using Labb1Restaurant.Models;

namespace Labb1Restaurant.Data.Repos.IRepos
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<Menu> GetDishByIdAsync(int menuId);

        Task AddFoodAsync(Menu menu);

        Task UpdateMenuAsync(Menu menu);

        Task DeleteDishAsync(int menuId);
    }
}
