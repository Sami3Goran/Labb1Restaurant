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
        public async Task<ActionResult<IEnumerable<CustomerShortDTO>>> GetAllCustomers()
        {
            var customerList = await _customerService.GetAllCustomersAsync();

            if (customerList.IsNullOrEmpty())
            {
                return NotFound("There is no customers yet.");
            }

            return Ok(customerList);
        }

        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public async Task<ActionResult<CustomerInfoAllDTO>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound("Couldn't find the customer.");
            }

            return Ok(customer);
        }

        [HttpGet]
        [Route("GetCustomerByLastName/{lastName}")]
        public async Task<ActionResult<CustomerInfoAllDTO>> GetCustomerByLastName(string lastName)
        {
            var lastNameC = await _customerService.GetCustomerByLastNameAsync(lastName);
            if (lastNameC == null)
            {
                return NotFound(new { Error = $"Couldnt find customer with surname:{lastName}" });
            }
            return Ok(lastNameC);
        }

        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult> AddCustomer([FromBody]CustomerDTO customer)
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
        [Route("UpdateCustomer/{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody]CustomerDTO customer)
        {
            try
            {
                await _customerService.UpdateCustomerAsync(id, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Customer has been updated!");
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Customer has been deleted.");
        }
    }

}

