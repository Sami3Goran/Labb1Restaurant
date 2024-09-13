using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Menu;

namespace Labb1Restaurant.Services.IServices
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuInfoAllDTO>> GetAllMenusAsync(); 
        Task<MenuInfoAllDTO> GetDishByIdAsync(int menuId); 

        Task AddFoodAsync(MenuDTO menu); 

        Task UpdateMenuAsync(int menuId, MenuDTO menu); 

        Task DeleteDishAsync(int menuId);
    }
}
