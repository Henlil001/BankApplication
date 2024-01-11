using Bank.Core.Interfaces;
using Bank.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult ShowAccounts()
        {
            try
            {
                //har kvar att mappa data till UI
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
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult CreateNewAccount(Accounts accounts)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                int customerId = int.Parse(userIdClaim.Value);

                int id = _accountsService.CreateNewAccount(accounts, customerId);

                if (id == 0)
                    return BadRequest("Invalied Data");

                return Ok(id);
                
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, CreateNewAccount");
                throw;
            }

            
        }
    }
}
