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
        private readonly ILogger<MenuService> _logger;
        public MenuService(IMenuRepository menuRepos, ILogger<MenuService> logger)
        {
            _menuRepo = menuRepos;
            _logger = logger;
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
                FoodInfo = menu.FoodInfo,
                IsPopular = menu.IsPopular,
                IsAvailable = menu.IsAvailable
            });
        }

        public async Task DeleteFoodAsync(int menuId)
        {
            var food = await _menuRepo.GetFoodByIdAsync(menuId);

            if (food != null)
            {
                await _menuRepo.DeleteFoodAsync(food);
            }

            throw new Exception($"Food with ID: {menuId} not found");

        }

        public async Task<IEnumerable<MenuInfoAllDTO>> GetAllAvailableFoodMenuAsync()
        {
            var availableFoods = await _menuRepo.GetAllAvailableFoodMenuAsync();
            return availableFoods.Select(f => new MenuInfoAllDTO
            {
                Id = f.Id,
                FoodName = f.FoodName,
                FoodPrice = f.FoodPrice,
                FoodInfo = f.FoodInfo,
                IsPopular = f.IsPopular,
                IsAvailable = f.IsAvailable
            }).ToList();
        }

        public async Task<IEnumerable<MenuInfoAllDTO>> GetAllMenusAsync()
        {
            var menuList = await _menuRepo.GetAllMenusAsync();
            return menuList.Select(m => new MenuInfoAllDTO
            {
                Id = m.Id,
                FoodName = m.FoodName,
                FoodInfo = m.FoodInfo,
                FoodPrice = m.FoodPrice,
                IsPopular = m.IsPopular,
                IsAvailable = m.IsAvailable
            }).ToList();
        }

        public async Task<IEnumerable<MenuInfoAllDTO>> GetAllPopularFoodMenuAsync()
        {
            var popularFoods = await _menuRepo.GetAllPopularFoodMenuAsync();
            return popularFoods.Select(f => new MenuInfoAllDTO
            {
                Id = f.Id,
                FoodName = f.FoodName,
                FoodInfo = f.FoodInfo,
                FoodPrice = f.FoodPrice,
                IsPopular = f.IsPopular,
                IsAvailable = f.IsAvailable
            }).ToList();
        }

        public async Task<MenuInfoAllDTO> GetFoodByIdAsync(int menuId)
        {
            var singleMenu = await _menuRepo.GetFoodByIdAsync(menuId);

            if (singleMenu == null)
            {
                // Logga ett varningsmeddelande om det behövs
                _logger.LogWarning($"Menu item with Id {menuId} not found");
                return null; // Returnera null istället för att kasta ett undantag
            }

            return new MenuInfoAllDTO
            {
                Id = singleMenu.Id,
                FoodName = singleMenu.FoodName,
                FoodInfo = singleMenu.FoodInfo,
                FoodPrice = singleMenu.FoodPrice,
                IsPopular = singleMenu.IsPopular,
                IsAvailable = singleMenu.IsAvailable
            };
        }

        public async Task UpdateMenuAsync(int menuId, MenuDTO menu)
        {
            var menuUp = await _menuRepo.GetFoodByIdAsync(menuId);
            if (menuUp == null)
            {
                throw new InvalidOperationException("The menu does not exist.");
            }

            menuUp.FoodName = menu.FoodName;
            menuUp.FoodInfo = menu.FoodInfo;
            menuUp.FoodPrice = menu.FoodPrice;
            menuUp.IsPopular = menu.IsPopular;
            menuUp.IsAvailable = menu.IsAvailable;

            await _menuRepo.UpdateMenuAsync(menuUp);
        }
    }
}
