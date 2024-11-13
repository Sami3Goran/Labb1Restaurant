using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Data.Repos
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Labb1RestaurantContext _context;

        public AccountRepository(Labb1RestaurantContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountByEmailAsync(string email)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
            return account;
        }

        public async Task RegisterAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }
    }
}
