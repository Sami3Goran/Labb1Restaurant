using Labb1Restaurant.Data.Repos;
using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Customer;
using Labb1Restaurant.Services.IServices;

namespace Labb1Restaurant.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }


        public async Task AddCustomerAsync(CustomerDTO customer)
        {

            var newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
            };

            await _customerRepo.AddCustomerAsync(newCustomer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new ArgumentException($"This customer with ID:{customerId} does not exist, try another ID?");
            }

            await _customerRepo.DeleteCustomerAsync(customer);
        }

        public async Task<IEnumerable<CustomerShortDTO>> GetAllCustomersAsync()
        {
            var allCustomrs = await _customerRepo.GetAllCustomersAsync();

            return allCustomrs.Select(c => new CustomerShortDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email
            }).ToList();
        }

        public async Task<CustomerInfoAllDTO> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception($"This customer with ID:{customerId} does not exist, try another ID?");
            }

            return new CustomerInfoAllDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email
            };
        }

        public async Task<CustomerInfoAllDTO> GetCustomerByLastNameAsync(string lastName)
        {
            var thisCustomer = await _customerRepo.GetCustomerByLastNameAsync(lastName);
            if (thisCustomer == null)
            {
                throw new Exception($"No customer with the lastname: {lastName}");
            }

            return new CustomerInfoAllDTO
            {
                Id = thisCustomer.Id,
                FirstName = thisCustomer.FirstName,
                LastName = thisCustomer.LastName,
                PhoneNumber = thisCustomer.PhoneNumber,
                Email = thisCustomer.Email
            };
        }

        public async Task UpdateCustomerAsync(int customerId, CustomerDTO customer)
        {
            var customerUp = await _customerRepo.GetCustomerByIdAsync(customerId);

            if (customerUp == null)
            {
                throw new InvalidOperationException("This customer doesn't exist, try again please.");
            }

            customerUp.FirstName = customer.FirstName;
            customerUp.LastName = customer.LastName;
            customerUp.Email = customer.Email;
            customerUp.PhoneNumber = customer.PhoneNumber;

            await _customerRepo.UpdateCustomerAsync(customerUp);
        }

    }
}
