using System.ComponentModel.DataAnnotations;

namespace E_Commerse_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        //public Role role { get; set; }
        public bool Enable { get; set; }
        public bool Deleted { get; set; }
    }
}
