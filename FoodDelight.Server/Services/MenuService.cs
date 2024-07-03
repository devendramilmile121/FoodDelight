using FoodDelight.Server.Data;
using FoodDelight.Server.Entities;
using FoodDelight.Server.Models.Menu;
using FoodDelight.Server.Models.Restaurant;
using FoodDelight.Server.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace FoodDelight.Server.Services
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuService> _logger;

        public MenuService(ApplicationDbContext context, IRestaurantService restaurantService,
            IMapper mapper,
            ILogger<MenuService> logger)
        {
            _context = context;
            _restaurantService = restaurantService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MenuDTO> Create(int RestaurantId, CreateMenuDTO menu)
        {
            try
            {
                _logger.LogInformation($"Creating Menu for Restaurant {RestaurantId}.");
                var res = await _restaurantService.Get(RestaurantId);
                if (res == null) {
                    _logger.LogInformation($"Restaurant with ID {RestaurantId} not found.");
                    throw new KeyNotFoundException($"Restaurant with ID {RestaurantId} not found.");
                }

                Menu newMenu = _mapper.Map<Menu>(menu);
                newMenu.RestaurantId = RestaurantId;
                _context.Add(newMenu);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created Menu for Restaurant {RestaurantId}.");
                return await Get(newMenu.Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to Menu with {RestaurantId}.", ex);
                throw;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                _logger.LogInformation($"Deleting Menu with {Id}.");
                Menu menu = await _context.Menus.Where(x => x.Id == Id)
                    .Include(inc => inc.MenuItems)
                    .FirstOrDefaultAsync();

                if (menu != null)
                {
                    _context.Remove(menu);
                    int result = await _context.SaveChangesAsync();
                    _logger.LogInformation($"Menu Deleted Successfully with {Id}.");
                    return result > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to Deleted Menu with {Id}.", ex);
                throw;
            }
        }

        public async Task<MenuDTO> Get(int Id)
        {
            try
            {
                _logger.LogInformation($"Getting Menu with {Id}.");
                Menu menu = await _context.Menus.Where(x => x.Id == Id)
                        .Include(inc => inc.MenuItems)
                        .FirstOrDefaultAsync();
                MenuDTO menuDTO = _mapper.Map<MenuDTO>(menu);
                _logger.LogInformation($"Menu retrive Successfullys with {Id}.");
                return menuDTO;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to retrive menu with {Id}.", ex);
                throw;
            }
        }

        public async Task<List<MenuDTO>> GetAll(int RestaurantId)
        {
            try
            {
                _logger.LogInformation($"Getting all Menus with {RestaurantId}.");
                List<Menu> menus = await _context.Menus.Where(x => x.RestaurantId == RestaurantId)
                    .Include(inc => inc.MenuItems)
                    .ToListAsync();

                List<MenuDTO> menuDTO = _mapper.Map<List<MenuDTO>>(menus);
                _logger.LogInformation($"Found {menus.Count} Menus with {RestaurantId}.");
                return menuDTO;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to retrive all menus with {RestaurantId}.", ex);
                throw;
            }
        }

        public async Task<MenuDTO> Update(int RestaurantId, int Id, CreateMenuDTO menu)
        {
            try
            {
                _logger.LogInformation($"Updating menu with {Id} and {RestaurantId}");
                var res = await _restaurantService.Get(RestaurantId);
                if (res == null)
                {
                    _logger.LogInformation($"Restaurant with ID {RestaurantId} not found.");
                    throw new KeyNotFoundException($"Restaurant with ID {RestaurantId} not found.");
                }

                var oldMenu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == Id &&
                                x.RestaurantId == RestaurantId);
                _mapper.Map(menu, oldMenu);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully updated menu with {Id} and {RestaurantId}");
                return await Get(Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Unable to update menu with {Id}", ex);
                throw;
            }
        }
    }
}
