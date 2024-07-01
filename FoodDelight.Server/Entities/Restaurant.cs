using FoodDelight.Server.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodDelight.Server.Entities
{
    public class Restaurant : BaseEntity
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }
        [MaxLength(250)]
        [Required]
        public string Location { get; set; }
        [Required]
        public RestautantType Type { get; set; }
        [Phone]
        [StringLength(15)]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}
