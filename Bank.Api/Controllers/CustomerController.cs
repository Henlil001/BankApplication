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
        public IActionResult CreateCustomer(CreateNewCustomerInput createNewCustomer)
        {
            try
            {
                var (newCustomer, check) = _customerService.CreateCostumer(createNewCustomer).Result;

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
