using E_Commerse_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace E_Commerse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static UserInfo user=new UserInfo(); 
        [HttpPost("Register")]
        public async Task<ActionResult<UserInfo>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            user.Username = request.Username;
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            return Ok(user);
            
        }
        [HttpPost("Login")]
        private async Task<ActionResult<string>> Login(UserDto request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest();
            }
            else
            {
                return ("My Crazy Token");
            }
        }

        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }


    }
}
