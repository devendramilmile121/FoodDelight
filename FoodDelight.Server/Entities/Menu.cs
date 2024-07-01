using FoodDelight.Server.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodDelight.Server.Entities
{
    public class Menu : BaseEntity
    {
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public MenuType MenuType { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
