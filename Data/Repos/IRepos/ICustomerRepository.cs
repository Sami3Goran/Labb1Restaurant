using Labb1Restaurant.Models;

namespace Labb1Restaurant.Data.Repos.IRepos
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByLastNameAsync(string lastName);

        Task<Customer> GetCustomerByIdAsync(int customerId);

        Task AddCustomerAsync(Customer customer);

        Task UpdateCustomerAsync(Customer customer);

        Task DeleteCustomerAsync(int customerId);
    }
}
