using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbSampleApi.Entities;
using MongoDbSampleApi.Interfaces;

namespace MongoDbSampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyCustomerController : ControllerBase
    {
        private readonly ICustomer _customer;
        public MyCustomerController(ICustomer customer)
        {
            _customer = customer;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            var isExistingCustomer = await _customer.IsCustomerExistsAsync(customer.EmailId);
            if (isExistingCustomer)
            {
                return BadRequest("Customer Already Exist.");
            }
            else
            {
                var result = await _customer.CreateCustomerAsync(customer);
                return Ok(result);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCredential credential)
        {
            var result = await _customer.LoginAsync(credential);
            return Ok(result);
        }
    }
}
