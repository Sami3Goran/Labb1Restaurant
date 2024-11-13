using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Menu;

namespace Labb1Restaurant.Services.IServices
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuInfoAllDTO>> GetAllMenusAsync(); 
        Task<MenuInfoAllDTO> GetFoodByIdAsync(int menuId);
        Task<IEnumerable<MenuInfoAllDTO>> GetAllPopularFoodMenuAsync();
        Task<IEnumerable<MenuInfoAllDTO>> GetAllAvailableFoodMenuAsync();
        Task AddFoodAsync(MenuDTO menu); 

        Task UpdateMenuAsync(int menuId, MenuDTO menu); 

        Task DeleteFoodAsync(int menuId);
    }
}
