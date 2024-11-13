using Labb1Restaurant.Models.DTOs.Account;

namespace Labb1Restaurant.Services.IServices
{
    public interface IAccountService
    {
        Task RegisterAccountAsync(RegisterUserDTO account);

        Task<string> LoginAccountAsync(AccountLoginDto account);
    }
}
