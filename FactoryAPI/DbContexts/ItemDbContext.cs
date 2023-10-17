using FactoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryAPI.DbContexts
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
