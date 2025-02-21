using MicroLexxika.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroLexxika.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
