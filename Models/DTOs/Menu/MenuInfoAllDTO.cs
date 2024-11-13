using System.ComponentModel.DataAnnotations;

namespace Labb1Restaurant.Models.DTOs.Menu
{
    public class MenuInfoAllDTO
    {
        public int Id { get; set; }

        public string FoodName { get; set; }

        public string FoodInfo { get; set; }

        public double FoodPrice { get; set; }

        public bool IsPopular { get; set; }

        public bool IsAvailable { get; set; }
    }
}
