using FoodDelight.Server.Enums;
using FoodDelight.Server.Models.Menu;

namespace FoodDelight.Server.Models.Restaurant
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public RestautantType Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<MenuDTO> Menus { get; set; }
    }
}
