using FoodDelight.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelight.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Menus)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId);

            modelBuilder.Entity<Menu>()
                .HasMany(m => m.MenuItems)
                .WithOne(mi => mi.Menu)
                .HasForeignKey(mi => mi.MenuId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
