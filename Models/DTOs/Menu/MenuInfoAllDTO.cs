namespace Labb1Restaurant.Models.DTOs.Menu
{
    public class MenuInfoAllDTO
    {
        public int MenuId { get; set; }
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public bool IsAvailable { get; set; }
    }
}
