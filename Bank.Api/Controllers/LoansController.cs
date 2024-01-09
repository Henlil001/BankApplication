using Bank.Core.Interfaces;
using Bank.Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [HttpPost]
        public IActionResult NewLoan(Loans loan)
        {
            int loanId = _loansService.NewLoan(loan);

            if (loanId == 0)
                return BadRequest("Invalied ");

            return Ok(loanId);
        }
    }
}
