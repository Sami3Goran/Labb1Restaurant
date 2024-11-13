using Labb1Restaurant.Auth;
using Labb1Restaurant.Data.Repos;
using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Account;
using Labb1Restaurant.Services.IServices;
using Microsoft.Extensions.Configuration;

namespace Labb1Restaurant.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepos;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepository accountRepo, IConfiguration configuration)
        {
            _accountRepos = accountRepo;
            _configuration = configuration;
        }
        public async Task<string> LoginAccountAsync(AccountLoginDto account)
        {
            var accountFinder = await _accountRepos.GetAccountByEmailAsync(account.Email);
            if (accountFinder == null)
            {
                throw new KeyNotFoundException($"Cant find user with email:{account.Email}");
            }

            //controll if email or pass is correct
            if (TokenHand.ScanPassAndMail(accountFinder, account.Password, accountFinder.PasswordHash))
            {
                throw new InvalidOperationException("Invalid email or password!");
            };

            //generate jwt token and returns it
            var token = TokenHand.GenerateJwtToken(accountFinder, _configuration);
            return token;
        }

        public async Task RegisterAccountAsync(RegisterUserDTO account)
        {
            var existingAccount = await _accountRepos.GetAccountByEmailAsync(account.Email);
            if (existingAccount != null)
            {
                throw new InvalidOperationException("This email is already in use!");
            }

            //hashing the password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(account.Password);
            var registerAccount = new Account
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                PasswordHash = passwordHash
            };
            await _accountRepos.RegisterAccountAsync(registerAccount);
        }
    }
}
