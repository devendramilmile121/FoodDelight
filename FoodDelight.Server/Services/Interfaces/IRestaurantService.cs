using FoodDelight.Server.Models.Restaurant;

namespace FoodDelight.Server.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<RestaurantDTO> Create(CreateRestaurant restaurant);
        Task<RestaurantDTO> Update(int Id, CreateRestaurant restaurant);
        Task<RestaurantDTO> Get(int Id);
        Task<bool> Delete(int Id);
        Task<List<RestaurantDTO>> GetAll();
    }
}
