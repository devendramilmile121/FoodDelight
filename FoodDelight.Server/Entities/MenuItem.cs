using System.ComponentModel.DataAnnotations;

namespace FoodDelight.Server.Entities
{
    public class MenuItem : BaseEntity
    {
        public int MenuId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public Menu Menu { get; set; }
    }
}
