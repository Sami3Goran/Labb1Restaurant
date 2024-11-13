using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Customer;

namespace Labb1Restaurant.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerShortDTO>> GetAllCustomersAsync();
        Task<CustomerInfoAllDTO> GetCustomerByLastNameAsync(string lastName);
        Task<CustomerInfoAllDTO> GetCustomerByIdAsync(int customerId);

        Task AddCustomerAsync(CustomerDTO customer);

        Task UpdateCustomerAsync(int customerId, CustomerDTO customer);

        Task DeleteCustomerAsync(int customerId);
    }
}
