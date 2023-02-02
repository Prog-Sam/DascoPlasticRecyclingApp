using Microsoft.EntityFrameworkCore;

namespace DascoPlasticRecyclingApp.Models
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<ContactType> ContactType { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<PlasticType> PlasticType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;database=dasco_db;uid=root;pwd=1234;");
        }
    }
}
