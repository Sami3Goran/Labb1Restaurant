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


        public async Task AddCustomerAsync(CustomerDTO customerAdd)
        {

            var newCustomer = new Customer
            {
                FirstName = customerAdd.FirstName,
                LastName = customerAdd.LastName,
                PhoneNumber = customerAdd.PhoneNumber,
                Email = customerAdd.Email,
            };

            await _customerRepo.AddCustomerAsync(newCustomer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(customerId);
            if (customer == null) throw new ArgumentException("Customer not found.");

            await _customerRepo.DeleteCustomerAsync(customerId);
        }

        public async Task<IEnumerable<CustomerInfoAllDTO>> GetAllCustomersAsync()
        {
            var allCustomrs = await _customerRepo.GetAllCustomersAsync();

            return allCustomrs.Select(c => new CustomerInfoAllDTO
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email
            }).ToList();
        }

        public async Task<CustomerInfoAllDTO> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(customerId);
            if (customer == null) 
            {
                throw new Exception($"Customer Does not exist.");
            }

            return new CustomerInfoAllDTO
            {
                CustomerId = customer.CustomerId,
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
                throw new Exception($"Customer not found.");
            }

            return new CustomerInfoAllDTO
            {
               CustomerId = thisCustomer.CustomerId,
                FirstName = thisCustomer.FirstName,
                LastName = thisCustomer.LastName,
                PhoneNumber = thisCustomer.PhoneNumber,
                Email = thisCustomer.Email
            };
        }

        public async Task UpdateCustomerAsync(int customerId, CustomerDTO customerNew)
        {
            var updateCustomer = await _customerRepo.GetCustomerByIdAsync(customerId);
            if (updateCustomer == null)
            {
                throw new InvalidOperationException("Customer Not Found.");
            }

            updateCustomer.FirstName = customerNew.FirstName;
            updateCustomer.LastName = customerNew.LastName;
            updateCustomer.Email = customerNew.Email;
            updateCustomer.PhoneNumber = customerNew.PhoneNumber;

            await _customerRepo.UpdateCustomerAsync(updateCustomer);
        }
    }
}
