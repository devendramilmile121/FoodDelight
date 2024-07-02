using FoodDelight.Server.Models.Menu;
using FoodDelight.Server.Models.MenuItem;

namespace FoodDelight.Server.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task<MenuItemDTO> Create(int MenuId, CreateMenuItemDTO item);
        Task<MenuItemDTO> Update(int MenuId, int Id, CreateMenuItemDTO item);
        Task<MenuItemDTO> Get(int Id);
        Task<bool> Delete(int Id);
        Task<List<MenuItemDTO>> GetAll(int MenuId);
    }
}
