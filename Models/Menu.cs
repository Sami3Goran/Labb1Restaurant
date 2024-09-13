using System.ComponentModel.DataAnnotations;

namespace Labb1Restaurant.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public double FoodPrice { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
