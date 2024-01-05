using Bank.Core.Interfaces;
using Bank.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
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
        public IActionResult GetAllCustomer()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateCustomer(Login login, Accounts accounts)
        {
            try
            {
                var newCustomer = _customerService.CreateCostumer(login, accounts);

                if (newCustomer.CorrectInput is true)
                    return Ok(newCustomer);

                return BadRequest("Remember to fill in all information / or Invalied Username.");
            }
            catch (Exception)
            {
                return StatusCode(500,"Error. CreateCustomer");
                throw;
            }
        }
    }
}
