using FoodDelight.Server.Data;
using FoodDelight.Server.Entities;
using FoodDelight.Server.Models.Menu;
using FoodDelight.Server.Models.MenuItem;
using FoodDelight.Server.Services.Interfaces;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace FoodDelight.Server.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuItemService> _logger;

        public MenuItemService(ApplicationDbContext context, IMenuService menuService,
            IMapper mapper, ILogger<MenuItemService> logger)
        {
            _context = context;
            _menuService = menuService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MenuItemDTO> Create(int MenuId, CreateMenuItemDTO item)
        {
            try
            {
                _logger.LogInformation($"Creating Menu Item for Menu {MenuId}.");
                var res = await _menuService.Get(MenuId);
                if (res == null)
                {
                    _logger.LogInformation($"Menu with ID {MenuId} not found.");
                    throw new KeyNotFoundException($"Menu with ID {MenuId} not found.");
                }

                MenuItem newMenu = _mapper.Map<MenuItem>(item);
                newMenu.MenuId = MenuId;
                _context.Add(newMenu);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created Menu Item for Menu {MenuId}.");
                return await Get(newMenu.Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to Create MenuItem with {MenuId}.", ex);
                throw;
            }
        }

        public async Task<bool> Delete(int Id)
        {
            try
            {
                _logger.LogInformation($"Deleting MenuItem with {Id}.");
                MenuItem item = await _context.MenuItems.Where(x => x.Id == Id)
                            .FirstOrDefaultAsync();

                if (item != null)
                {
                    _context.Remove(item);
                    int result = await _context.SaveChangesAsync();
                    _logger.LogInformation($"Menu Item Deleted Successfully with {Id}.");
                    return result > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to Deleted MenuItem with {Id}.", ex);
                throw;
            }
        }

        public async Task<MenuItemDTO> Get(int Id)
        {
            try
            {
                _logger.LogInformation($"Getting Menu Item with {Id}.");
                MenuItem menu = await _context.MenuItems.Where(x => x.Id == Id)
                        .FirstOrDefaultAsync();
                MenuItemDTO menuDTO = _mapper.Map<MenuItemDTO>(menu);
                _logger.LogInformation($"Menu Item retrive Successfullys with {Id}.");
                return menuDTO;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to retrive menu item with {Id}.", ex);
                throw;
            }
        }

        public async Task<List<MenuItemDTO>> GetAll(int MenuId)
        {
            try
            {
                _logger.LogInformation($"Getting all Menus Item with {MenuId}.");
                List<MenuItem> menus = await _context.MenuItems.Where(x => x.MenuId == MenuId)
                    .ToListAsync();

                List<MenuItemDTO> menuDTO = _mapper.Map<List<MenuItemDTO>>(menus);
                _logger.LogInformation($"Found {menus.Count} Menu Items with {MenuId}.");
                return menuDTO;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to retrive all menus items with {MenuId}.", ex);
                throw;
            }
        }

        public async Task<MenuItemDTO> Update(int MenuId, int Id, CreateMenuItemDTO item)
        {
            try
            {
                _logger.LogInformation($"Updating menu item with {Id} and {MenuId}");
                var res = await _menuService.Get(MenuId);
                if (res == null)
                {
                    _logger.LogInformation($"Menu item with ID {MenuId} not found.");
                    throw new KeyNotFoundException($"Menu item with ID {MenuId} not found.");
                }

                var oldMenu = await _context.MenuItems.FirstOrDefaultAsync(x => x.Id == Id);
                _mapper.Map(item, oldMenu);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Successfully updated menu item with {Id} and {MenuId}");
                return await Get(Id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Unable to update menu item with {Id}", ex);
                throw;
            }
        }
    }
}
