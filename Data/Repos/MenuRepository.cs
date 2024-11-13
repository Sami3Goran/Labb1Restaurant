using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Data.Repos
{
    public class MenuRepository : IMenuRepository
    {
        private readonly Labb1RestaurantContext _context;

        public MenuRepository(Labb1RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddFoodAsync(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFoodAsync(Menu menu)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllAvailableFoodMenuAsync()
        {
            var availableFood = await _context.Menus.Where(food => food.IsAvailable).ToListAsync();
            return availableFood;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            var menuList = await _context.Menus.ToListAsync();
            return menuList;
        }

        public async Task<IEnumerable<Menu>> GetAllPopularFoodMenuAsync()
        {
            var popularFood = await _context.Menus.Where(food => food.IsPopular).ToListAsync();
            return popularFood;
        }

        public async Task<Menu> GetFoodByIdAsync(int menuId)
        {
            var menu = await _context.Menus.FindAsync(menuId);
            return menu;
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }
    }
}
