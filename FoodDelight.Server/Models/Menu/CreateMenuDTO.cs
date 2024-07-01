using FoodDelight.Server.Enums;

namespace FoodDelight.Server.Models.Menu
{
    public class CreateMenuDTO
    {
        public int RestaurantId { get; set; }
        public MenuType MenuType { get; set; }
        public string Name { get; set; }
    }
}
