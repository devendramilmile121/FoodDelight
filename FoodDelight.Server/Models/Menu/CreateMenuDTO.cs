using FoodDelight.Server.Enums;

namespace FoodDelight.Server.Models.Menu
{
    public class CreateMenuDTO
    {
        public MenuType MenuType { get; set; }
        public string Name { get; set; }
    }
}
