using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<Users> users { get; set; }
    }
}
