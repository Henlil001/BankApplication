using Bank.Core.Interfaces;
using Bank.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _loansService;
        public LoansController(ILoansService loansService)
        {
            _loansService = loansService;
        }
        [Route("Admin_Create_Loan")]
        [HttpPost]
        public IActionResult NewLoan(NewLoanDTO loan)
        {
            int newLoan = _loansService.NewLoan(loan);

            if (newLoan == -1)
                return BadRequest("Invalied data");
            if (newLoan == 0)
                return BadRequest("Account dosent exist.");

            return Ok(new {LoanID = newLoan});
        }
    }
}
