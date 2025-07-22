using Microsoft.EntityFrameworkCore;
using TShop.Domain.Entities;

namespace TShop.Infrustructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
    }
}

