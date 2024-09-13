using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Customer;

namespace Labb1Restaurant.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerInfoAllDTO>> GetAllCustomersAsync();
        Task<CustomerInfoAllDTO> GetCustomerByLastNameAsync(string lastName);
        Task<CustomerInfoAllDTO> GetCustomerByIdAsync(int customerId);

        Task AddCustomerAsync(CustomerDTO customerAdd);

        Task UpdateCustomerAsync(int customerId, CustomerDTO customerNew);

        Task DeleteCustomerAsync(int customerId);
    }
}
