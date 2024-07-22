using Matchfinder.Entities;
using Microsoft.EntityFrameworkCore;

namespace Matchfinder.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
