using E_Commerse_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace E_Commerse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ApiContext apiContext;
        public User user = new User();
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            apiContext = new ApiContext();
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            user.Username = request.Username;
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            user.Name = request.Name;
            user.Enable = true;
            user.Deleted = false;
            apiContext.users.Add(user);
            apiContext.SaveChanges();
            return Ok(user);
            
        }
        [HttpGet("GetAllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return apiContext.users.ToList();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var userDb = apiContext.users.FirstOrDefault(e => e.Username == request.Username);
            if (userDb == null)
            {
                return BadRequest();
            }
            else
            {
                if (!verifyPasswordHash(request.Password, userDb.PasswordHash, userDb.PasswordSalt))
                {
                    return BadRequest("Wrong Password");
                }
                else
                {
                    string Token = CreateToken(userDb);
                    return Ok(Token);
                }
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1),signingCredentials:cred);
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        private bool verifyPasswordHash(string Password,byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return ComputedHash.SequenceEqual(PasswordHash);
            }
        }
        

    }
}
