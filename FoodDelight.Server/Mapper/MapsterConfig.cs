using FoodDelight.Server.Entities;
using FoodDelight.Server.Models.Menu;
using FoodDelight.Server.Models.MenuItem;
using FoodDelight.Server.Models.Restaurant;
using Mapster;

namespace FoodDelight.Server.Mapper
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<Restaurant, RestaurantDTO>.NewConfig()
                .Map(dest => dest.Menus, src => src.Menus.Adapt<List<MenuDTO>>())
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.Description, src => src.Description);

            TypeAdapterConfig<Restaurant, CreateRestaurant>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Phone, src => src.Phone)
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.Location, src => src.Location)
                .Map(dest => dest.Description, src => src.Description);
            
            TypeAdapterConfig<Menu, MenuDTO>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.MenuType, src => src.MenuType)
                .Map(dest => dest.RestaurantId, src => src.RestaurantId)
                .Map(dest => dest.Items, src => src.MenuItems.Adapt<List<MenuItemDTO>>());
        }
    }
}
