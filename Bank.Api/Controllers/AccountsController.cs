using Bank.Core.Interfaces;
using Bank.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }
        [Route("Show_Inloged_Customer_Accounts")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult ShowAccounts()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                int customerId = int.Parse(userIdClaim.Value);

                return Ok(_accountsService.ShowAccounts(customerId));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, ShowAccounts");
                throw;
            }
        }
        [Route("Create_New_Account_For_Inloged_Customer")]
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult CreateNewAccount(CreateAccountDTO account)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                int customerId = int.Parse(userIdClaim.Value);

                int id = _accountsService.CreateNewAccount(account, customerId);

                if (id == 0)
                    return BadRequest("remember to fill in Frequensy and AccountType 1 or 2. ");

                return Ok(new {NewAccountId = id });

            }
            catch (Exception)
            {
                return StatusCode(500, "Error, CreateNewAccount");
                throw;
            }


        }
    }
}
