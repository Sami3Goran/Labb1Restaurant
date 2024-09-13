using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Customer;
using Labb1Restaurant.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Labb1Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
           _customerService = customerService;
        }


        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerInfoAllDTO>>> GetAllCustomers()
        {
            var customerList = await _customerService.GetAllCustomersAsync();

            if (customerList.IsNullOrEmpty())
            {
                return NotFound("There is no customers yet.");
            }

            return Ok(customerList);
        }

        [HttpGet]
        [Route("GetCustomerById/{customerId}")]
        public async Task<ActionResult<CustomerInfoAllDTO>> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                return NotFound("Couldn't find the customer.");
            }

            return Ok(customer);
        }

        [HttpGet]
        [Route("GetCustomerByLastName/{lastName}")]
        public async Task<ActionResult<Customer>> GetCustomerByLastName(string lastName)
        {
            var LastNameC = await _customerService.GetCustomerByLastNameAsync(lastName);
            if (LastNameC == null)
            {
                return NotFound(new { Error = $"Couldnt find customer with surname:{lastName}" });
            }
            return Ok(LastNameC);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult> AddCustomer(CustomerDTO customer)
        {
            try
            {
                await _customerService.AddCustomerAsync(customer);
            }
            catch (Exception ex)
            {
                return Conflict(new { Error = ex.Message });
            }

            return Ok("Customer has been added");
        }

        [HttpPut]
        [Route("UpdateCustomer/{CustomerId}")]
        public async Task<ActionResult> UpdateCustomer(int customerId, CustomerDTO customer)
        {
            try
            {
                await _customerService.UpdateCustomerAsync(customerId, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("successfull customer Update");
        }

        [HttpDelete]
        [Route("/DeleteCustomer/{customerId}")]
        public async Task<IActionResult> DeleteCustomerAsync(int customerId)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(customerId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Customer has been deleted.");
        }
    }

}

