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
                string base64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAgAAAAIACAIAAAB7GkOtAAANF0lEQVR4nOzXC88W9H3GcWA3UdSVMQ/TUp1Whzh83OOsGmadXSNqjI5ip7VU8dDZuVm0jTKnZfVIO4f1kDLAtelSNrvhHHPxwJiaziJUK4dQCrSlo5as4Ih0dSAypbJXcSVLrs/nBVz/Ozfh+d6/wfpNm0cknXb0zOj+cVceF93/1xMOjO6f9vMzovvLNr0Q3b/31h3R/c/efFd0/w/v+VR0/4ejz47u7x+6NLp/yLqfRfdH/e+i6P4NF2f/fz0+96bo/oe+/GB0f1R0HYD/twQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQKmRV/5wYfSBPePfH91/9uND0f29V78vur/pvLei+1u2jo/u/+iv7orun/+7+6P7H7hyZ3R/wqjbovsTX7kxur/i1o9G9888/0+j+9cc+6Xo/ie/f3p0/+uTJkb3XQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQKnBKdN2RR+YdfVB0f0/v/bvovtn3v170f29r8yJ7s88ckl0/41VW6L711y/Pbo/5aWjo/ujFma//6lHjYnuD707Ibr/5C2HRfefXvFf0f2bH14T3X//hsej+y4AgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKDUyEe/9dPoA0e8c0t0/9qP3hfdH37ss9H9lctfje6vfv0fo/uD7dl/3xtH3R/dH3nyDdH9z9/zxej+nLWfie5P++NN0f1xk6LzI0Ys+ZXo/APnfSO6v39l9je6CwCglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKDUYnnlR9IHdky+M7t974rzo/ou7Do7uT7lrX3R/5Ukfju6vOeLvo/vPff606P7dd/x6dH+wYGx2f9vb0f2P7Lsyuj//dx6I7o9esCu6//FT3xvdH9oWnXcBALQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBt/c/97oAy9f91B0/+B9b0T3J550WXT/1cUTo/svPPHJ6P5ff252dP+xKxZH9284N/sb6HvjZkT3z7n9kej+j8cPRfcvOmRRdH/SieOi+4Olz0T3L/3BndF9FwBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUGrw0kkHRR9Y8eY72f2xo6P7F745Lbq/eOuHo/tPj9gW3V+68O3o/hP/eUR0f7BiV3T/R9dMjO6fseH46P4XH8nuPznrt6P7c4/bHN3fcfpV0f3DTvmf6L4LAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoNfjC/FujDxy6/BPR/TNuOSy6v2bT7Oj+8H0LovtXH//j6P6ky74Z3b9x9DXR/W//4pHo/tnfz34/X7v8quj+jGmLovvbR98Y3T/31N+K7q8/YXJ0/0s790f3XQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQKmRc956LPrAO3dNz+6f/JvR/cO/sjO6/8HpD0T3/2LLx6L7c3b/Q3T/9Btui+4/+emt0f1LR06J7g9mXxLdv+5n26P7v3TIWdH9yc+fHd1/9OLx0f0xZ02L7rsAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSg2fGrog+MH35ydH9c8+ZFd0/7/DXovuvHXtJdH/FL341uj/j8W9F9w/ffFZ0/58mTIvuP/xvW6L73/uTI6P7y1deEN1/ZO53ovtvn7Ivuv/fs5dE999Y/W503wUAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQaPHDiZdEHbp+U3V81dm50/ytTd0f3f7L059H9x3fOi+4/t+dvo/sfPDG7P/o9F0T3vzPvwOj+a/cvj+5vXzUxuj/uuezn37vt4ej+mLuviu5/bd2k6L4LAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoNRhx1JjoA5t2Pxnd3/Xyyuj+rFHzovsXz/1UdP+AZ/ZE9z8x9ZXo/nUbro3uP734l6P7T215Mbo/eePG6P7ii56N7j+x5o7o/rrzX4juP7hvYnT/9rXZv58uAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACg1MhDf3Jq9IETfjA/ur997SnR/Zt/f1F0f3jCx6L7Yz5wVXR//PB7ovvP3Xl7dP/5MQ9G92dsnRrdH35qTnR/3jEXRvffWnBOdH/DutXR/T+445jo/vJ7b4ruuwAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFKDzy17MfrAjh0HRPcP3rgwuv/UvUdE94f/7JLo/pc3r47urzrtL6P77/vpsuj+MVNej+7/2ohF0f31l8yM7g9Nnh/d37T85ej+1EMfju5P2f8f0f31T2Q/vwsAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACg1+Pq62dEHDrp8enR/2f07o/t7Zh4f3V/yoVui+3/zG7dG95fOvSK6P3znILp/3DGbo/uf/u53o/sHTjgour/k0aXR/Su+8dXo/qznL4vuHzV9Y3R/6anXRfddAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAqcGSQ/8o+sCy+Z+J7h/95kvR/QtfPza6/+2x10f3r39xbXT/2aGvRvcv3/sv0f37N9wT3V9w5gHR/du+8M/R/Y+Meyi6f9/CO6P7r15wU3T/yBlD0f2xD/17dN8FAFBKAABKCQBAKQEAKCUAAKUEAKCUAACUEgCAUgIAUEoAAEoJAEApAQAoJQAApQQAoJQAAJQSAIBSAgBQSgAASgkAQCkBACglAAClBACglAAAlBIAgFICAFBKAABKCQBAKQEAKCUAAKUEAKCUAACU+r8AAAD//7SkfRvIxGSmAAAAAElFTkSuQmCC\r\n";
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
                        ImagePath = "base64",
                        CreatedDate = DateTime.UtcNow
                    },
                    new MenuItem
                    {
                        MenuId = menus[0].Id,
                        Name = "Fries",
                        Description = "Crispy golden french fries.",
                        Price = 2.99m,
                        ImagePath = "base64",
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
