using Labb1Restaurant.Models;

namespace Labb1Restaurant.Data.Repos.IRepos
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<Menu> GetFoodByIdAsync(int menuId);

        Task<IEnumerable<Menu>> GetAllPopularFoodMenuAsync();
        Task<IEnumerable<Menu>> GetAllAvailableFoodMenuAsync();

        Task AddFoodAsync(Menu menu);

        Task UpdateMenuAsync(Menu menu);

        Task DeleteFoodAsync(Menu menu);
    }
}
