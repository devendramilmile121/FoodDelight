using FoodDelight.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelight.Server.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Restaurants.Any() && !_context.Menus.Any() && !_context.MenuItems.Any())
            {
                _context.Restaurants.AddRange(
                    new Restaurant
                    {
                        Id = 1,
                        Name = "The Good Food Place",
                        Location = "123 Main Street",
                        Phone = "555-1234",
                        Email = "info@goodfood.com",
                        Type = Enums.RestautantType.PureVeg,
                        CreatedDate = DateTime.UtcNow
                    },
                    new Restaurant
                    {
                        Id = 2,
                        Name = "Pizza Heaven",
                        Location = "456 Elm Street",
                        Phone = "555-5678",
                        Email = "contact@pizzaheaven.com",
                        Type = Enums.RestautantType.VegNonVeg,
                        CreatedDate = DateTime.UtcNow
                    }
                );

                _context.Menus.AddRange(
                    new Menu
                    {
                        Id = 1,
                        RestaurantId = 1,
                        Name = "Main Menu",
                        MenuType = Enums.MenuType.Veg,
                        CreatedDate = DateTime.UtcNow
                    },
                    new Menu
                    {
                        Id = 2,
                        RestaurantId = 2,
                        Name = "Pizza Menu",
                        MenuType = Enums.MenuType.Veg,
                        CreatedDate = DateTime.UtcNow
                    }
                );

                _context.MenuItems.AddRange(
                    new MenuItem
                    {
                        Id = 1,
                        MenuId = 1,
                        Name = "Burger",
                        Description = "Juicy grilled burger with lettuce, tomato, and cheese.",
                        Price = 8.99m,
                        ImagePath = "/uploads/default_burger.jpg",
                        CreatedDate = DateTime.UtcNow
                    },
                    new MenuItem
                    {
                        Id = 2,
                        MenuId = 1,
                        Name = "Fries",
                        Description = "Crispy golden french fries.",
                        Price = 2.99m,
                        ImagePath = "/uploads/default_fries.jpg",
                        CreatedDate = DateTime.UtcNow
                    },
                    new MenuItem
                    {
                        Id = 3,
                        MenuId = 2,
                        Name = "Pepperoni Pizza",
                        Description = "Classic pizza with pepperoni and mozzarella cheese.",
                        Price = 12.99m,
                        ImagePath = "/uploads/default_pepperoni_pizza.jpg",
                        CreatedDate = DateTime.UtcNow
                    },
                    new MenuItem
                    {
                        Id = 4,
                        MenuId = 2,
                        Name = "Margherita Pizza",
                        Description = "Pizza with fresh tomatoes, basil, and mozzarella cheese.",
                        Price = 10.99m,
                        ImagePath = "/uploads/default_margherita_pizza.jpg",
                        CreatedDate = DateTime.UtcNow
                    }
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
