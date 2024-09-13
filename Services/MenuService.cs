using Labb1Restaurant.Data.Repos;
using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Menu;
using Labb1Restaurant.Services.IServices;

namespace Labb1Restaurant.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepo;
        public MenuService(IMenuRepository menuRepos)
        {
            _menuRepo = menuRepos;
        }

        public async Task AddFoodAsync(MenuDTO menu)
        {
            if (menu.FoodPrice <= 0 || menu.FoodPrice == null)
            {
                throw new InvalidOperationException($"Price must be larger than 0!");
            }

            await _menuRepo.AddFoodAsync(new Menu
            {
                FoodName = menu.FoodName,
                FoodPrice = menu.FoodPrice,
                IsAvailable = menu.IsAvailable,
            });
        }

        public async Task DeleteDishAsync(int menuId)
        {
            var Food = await _menuRepo.GetDishByIdAsync(menuId);

            if (Food == null)
            {
                throw new Exception($"Food with ID: {menuId} not found");
            }

            await _menuRepo.DeleteDishAsync(menuId);
        }

        public async Task<IEnumerable<MenuInfoAllDTO>> GetAllMenusAsync()
        {
            var menuList = await _menuRepo.GetAllMenusAsync();
            return menuList.Select(m => new MenuInfoAllDTO
            {
                MenuId = m.MenuId,
                FoodName = m.FoodName,
                FoodPrice = m.FoodPrice,
                IsAvailable = m.IsAvailable,
            }).ToList();
        }

        public async Task<MenuInfoAllDTO> GetDishByIdAsync(int menuId)
        {
            var singleMenu = await _menuRepo.GetDishByIdAsync(menuId);
            if (singleMenu == null)
            {
                throw new KeyNotFoundException($"Menu item with Id.{menuId} was not found!");
            }
            return new MenuInfoAllDTO
            {
                MenuId = singleMenu.MenuId,
                FoodName = singleMenu.FoodName,
                FoodPrice = singleMenu.FoodPrice,
                IsAvailable = singleMenu.IsAvailable,
            };
        }

        public async Task UpdateMenuAsync(int menuId, MenuDTO menu)
        {
            var menuUp = await _menuRepo.GetDishByIdAsync(menuId);
            if (menuUp == null)
            {
                throw new ArgumentException("The menu does not exist.");
            }

            menuUp.FoodName = menu.FoodName;
            menuUp.FoodPrice = menu.FoodPrice;
            menuUp.IsAvailable = menu.IsAvailable;

            await _menuRepo.UpdateMenuAsync(menuUp);
        }
    }
}
