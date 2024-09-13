using Labb1Restaurant.Models.DTOs.Menu;
using Labb1Restaurant.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb1Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        [Route("GetAllMenu")]
        public async Task<ActionResult<IEnumerable<MenuInfoAllDTO>>> GetAllMenus()
        {
            var menuList = await _menuService.GetAllMenusAsync();
            if (menuList == null || !menuList.Any())
            {
                return NotFound(new { Error = "Did not found the menus" });
            }
            return Ok(menuList);
        }

        //Get /api/Menus/GetDishById/{id}
        [HttpGet]
        [Route("GetDishById/{menuId}")]
        public async Task<ActionResult<MenuInfoAllDTO>> GetDishById(int menuId)
        {
            var menu = await _menuService.GetDishByIdAsync(menuId);

            if (menu == null)
            {
                return NotFound("Didn't find the food with that ID.");
            }

            return Ok(menu);
        }

        [HttpPost]
        [Route("AddFood")]
        public async Task<IActionResult> AddFood(MenuDTO menu)
        {
            try
            {
                await _menuService.AddFoodAsync(menu);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Food added to the menu");
        }

        [HttpPut]
        [Route("/UpdateFood{menuId}")]
        public async Task<IActionResult> UpdateMenu(int menuId, MenuDTO menu)
        {
            try
            {
                await _menuService.UpdateMenuAsync(menuId, menu);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Food updated");
        }

        [HttpDelete]
        [Route("DeleteFood/{menuId}")]
        public async Task<ActionResult> DeleteDish(int menuId)
        {
            try
            {
                await _menuService.DeleteDishAsync(menuId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Food Has been deleted");
        }
    }
}
