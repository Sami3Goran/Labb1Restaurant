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

        public async Task DeleteDishAsync(int menuId)
        {
            var dish = await _context.Menus.FindAsync();

            if (dish != null)
            {
                _context.Menus.Remove(dish);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            var menuList = await _context.Menus.ToListAsync();
            return menuList;
        }

        public async Task<Menu> GetDishByIdAsync(int menuId)
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
