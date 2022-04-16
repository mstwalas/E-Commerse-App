namespace E_Commerse_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role role { get; set; }
        public bool Enable { get; set; }
        public bool Deleted { get; set; }
    }
}
