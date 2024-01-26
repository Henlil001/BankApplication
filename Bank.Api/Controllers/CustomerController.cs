using Bank.Core.Interfaces;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Route("Admin_Create_New_Customer")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCustomerAsync(CreateNewCustomerInput createNewCustomer)
        {
            try
            {
                var tuple = _customerService.CreateCostumer(createNewCustomer);
                var newCustomer = tuple.Item1;
                var check = tuple.Item2;

                if (check)
                    return Ok(newCustomer);

                return BadRequest("Remember to fill in all information / or Invalied Username.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error. CreateCustomer");
                throw;
            }
        }


    }
}
