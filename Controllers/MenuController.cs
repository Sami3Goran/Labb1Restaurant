using Labb1Restaurant.Models.DTOs.Menu;
using Labb1Restaurant.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

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

        [HttpGet]
        [Route("GetFoodById/{id}")]
        public async Task<ActionResult<MenuInfoAllDTO>> GetFoodById(int id)
        {
            var menu = await _menuService.GetFoodByIdAsync(id);

            if (menu == null)
            {
                return NotFound($"Menu item with Id {id} was not found."); // Returnera 404
            }

            return Ok(menu);
        }

        [HttpGet]
        [Route("GetAllAvailableFoodMenu")]
        public async Task<ActionResult<IEnumerable<MenuInfoAllDTO>>> GetAllAvailableFoodMenu()
        {
            var availableFoods = await _menuService.GetAllAvailableFoodMenuAsync();

            if (availableFoods.IsNullOrEmpty())
            {
                return NotFound("We didnt find any food...");
            }

            return Ok(availableFoods);
        }

        [HttpGet]
        [Route("GetAllPopularFoodMenu")]
        public async Task<ActionResult<IEnumerable<MenuInfoAllDTO>>> GetAllPopularFoodMenu()
        {
            var popularFoods = await _menuService.GetAllPopularFoodMenuAsync();

            if (popularFoods.IsNullOrEmpty())
            {
                return NotFound("We didnt find any popular food...");
            }

            return Ok(popularFoods);
        }

        [HttpPost]
        [Route("AddFood")]
        public async Task<IActionResult> AddFood([FromBody] MenuDTO menu)
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
        [Route("UpdateFood/{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody]MenuDTO menu)
        {
            try
            {
                await _menuService.UpdateMenuAsync(id, menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Food has been updated");
        }

        [HttpDelete]
        [Route("DeleteFood/{id}")]
        public async Task<ActionResult> DeleteFood(int id)
        {
            try
            {
                await _menuService.DeleteFoodAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Food Has been deleted");
        }
    }
}
