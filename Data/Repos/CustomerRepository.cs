using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Data.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Labb1RestaurantContext _context;

        public CustomerRepository(Labb1RestaurantContext context)
        {
            _context = context;
        }


        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customerList = await _context.Customers.ToListAsync();
            return customerList;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var theCustomer = await _context.Customers.FindAsync(customerId);
            return theCustomer;
        }

        public async Task<Customer> GetCustomerByLastNameAsync(string lastName)
        {
            var customerName = await _context.Customers.FirstOrDefaultAsync(p => p.LastName == lastName);
            return customerName;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
