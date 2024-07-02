using FoodDelight.Server.Models.Menu;
using FoodDelight.Server.Models.Restaurant;

namespace FoodDelight.Server.Services.Interfaces
{
    public interface IMenuService
    {
        Task<MenuDTO> Create(int RestaurantId, CreateMenuDTO menu);
        Task<MenuDTO> Update(int RestaurantId, int Id, CreateMenuDTO menu);
        Task<MenuDTO> Get(int Id);
        Task<bool> Delete(int Id);
        Task<List<MenuDTO>> GetAll(int RestaurantId);
    }
}
