using Microsoft.EntityFrameworkCore;

namespace E_Commerse_API.Models
{
    public class ApiContext:DbContext
    {
        public DbSet<Product> products { get; set; }
        public DbSet<Catogry> Catogries { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //base.OnConfiguring(optionsBuilder);
            options.UseSqlServer(@"Data Source=.;Initial Catalog=EComerseDB;Integrated Security=True");
        }
    }
}
