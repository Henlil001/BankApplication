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

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCustomer(CreateNewCustomer createNewCustomer)
        {
            try
            {
                var newCustomer = _customerService.CreateCostumer(createNewCustomer);

                if (newCustomer.CorrectInput is true)
                    return Ok(newCustomer);

                return BadRequest("Remember to fill in all information / or Invalied Username.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error. CreateCustomer");
                throw;
            }
        }

        //[HttpGet]
        //[Authorize(Roles = "Customer")]
        //public IActionResult ShowAccounts()
        //{
        //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        //    int id = int.Parse(userIdClaim.Value);

        //    return Ok();
        //}


    }
}
