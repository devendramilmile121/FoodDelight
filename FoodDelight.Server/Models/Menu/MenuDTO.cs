using FoodDelight.Server.Enums;
using FoodDelight.Server.Models.MenuItem;

namespace FoodDelight.Server.Models.Menu
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public MenuType MenuType { get; set; }
        public string Name { get; set; }
        public List<MenuItemDTO> Items { get; set; }
    }
}
