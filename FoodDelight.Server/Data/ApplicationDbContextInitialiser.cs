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
                var restaurants = new List<Restaurant> {
                    new Restaurant
                    {
                        Name = "The Good Food Place",
                        Location = "123 Main Street",
                        Phone = "555-1234",
                        Email = "info@goodfood.com",
                        Type = Enums.RestautantType.PureVeg,
                        CreatedDate = DateTime.UtcNow
                    },
                    new Restaurant
                    {
                        Name = "Pizza Heaven",
                        Location = "456 Elm Street",
                        Phone = "555-5678",
                        Email = "contact@pizzaheaven.com",
                        Type = Enums.RestautantType.VegNonVeg,
                        CreatedDate = DateTime.UtcNow
                    }
                };

                _context.Restaurants.AddRange(restaurants);
                await _context.SaveChangesAsync();

                var menus = new List<Menu>
                {
                    new Menu
                    {
                        RestaurantId = restaurants[0].Id,
                        Name = "Main Menu",
                        MenuType = Enums.MenuType.Veg,
                        CreatedDate = DateTime.UtcNow
                    },
                    new Menu
                    {
                        RestaurantId = restaurants[1].Id,
                        Name = "Pizza Menu",
                        MenuType = Enums.MenuType.Veg,
                        CreatedDate = DateTime.UtcNow
                    }
                };
                _context.Menus.AddRange(menus);
                await _context.SaveChangesAsync();

                var menuItems = new List<MenuItem>
                {
                    new MenuItem
                    {
                        MenuId = menus[0].Id,
                        Name = "Burger",
                        Description = "Juicy grilled burger with lettuce, tomato, and cheese.",
                        Price = 8.99m,
                        ImagePath = "/uploads/default_burger.jpg",
                        CreatedDate = DateTime.UtcNow
                    },
                    new MenuItem
                    {
                        MenuId = menus[0].Id,
                        Name = "Fries",
                        Description = "Crispy golden french fries.",
                        Price = 2.99m,
                        ImagePath = "/uploads/default_fries.jpg",
                        CreatedDate = DateTime.UtcNow
                    },
                    new MenuItem
                    {
                        MenuId = menus[1].Id,
                        Name = "Pepperoni Pizza",
                        Description = "Classic pizza with pepperoni and mozzarella cheese.",
                        Price = 12.99m,
                        ImagePath = "/uploads/default_pepperoni_pizza.jpg",
                        CreatedDate = DateTime.UtcNow
                    },
                    new MenuItem
                    {
                        MenuId = menus[1].Id,
                        Name = "Margherita Pizza",
                        Description = "Pizza with fresh tomatoes, basil, and mozzarella cheese.",
                        Price = 10.99m,
                        ImagePath = "/uploads/default_margherita_pizza.jpg",
                        CreatedDate = DateTime.UtcNow
                    }
                };
                _context.MenuItems.AddRange(menuItems);
                await _context.SaveChangesAsync();
            }
        }
    }
}
