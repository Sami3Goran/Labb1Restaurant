using Labb1Restaurant.Models;

namespace Labb1Restaurant.Data.Repos.IRepos
{
    public interface IAccountRepository
    {
        Task RegisterAccountAsync(Account account);
        Task<Account> GetAccountByEmailAsync(string email);
    }
}
