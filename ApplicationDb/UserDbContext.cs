using Microsoft.EntityFrameworkCore;
using signalr_be.Models;

namespace signalr_be.ApplicationDb
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
