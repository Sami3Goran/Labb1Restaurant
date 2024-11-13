using Labb1Restaurant.Models.DTOs.Account;
using Labb1Restaurant.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb1Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDTO registerUser)
        {
            try
            {
                await _accountService.RegisterAccountAsync(registerUser);

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

            return Ok("Account has been Created!");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(AccountLoginDto loggedIn)
        {
            try
            {
                var token = await _accountService.LoginAccountAsync(loggedIn);
                return Ok(new { token });

            }
            catch (KeyNotFoundException ex)
            {

                return NotFound(ex.Message);

            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
