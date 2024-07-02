using System.ComponentModel.DataAnnotations;

namespace FoodDelight.Server.Models.MenuItem
{
    public class CreateMenuItemDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}
