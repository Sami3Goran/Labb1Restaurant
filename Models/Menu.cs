using System.ComponentModel.DataAnnotations;

namespace Labb1Restaurant.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public string FoodInfo { get; set; }

        [Required]
        public double FoodPrice { get; set; }

        public bool IsPopular { get; set; }

        public bool IsAvailable { get; set; }
    }
}
