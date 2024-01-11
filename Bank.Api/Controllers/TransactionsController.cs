using Bank.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        public TransactionsController(ITransactionsService transactions)
        {
            _transactionsService = transactions;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult ShowTransactions(int accountId) 
        {
            try
            {
                //har kvar att mappa data till UI
                return Ok(_transactionsService.ShowTransactions(accountId));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, ShowTransactions");
                throw;
            }
        
        }
    }
}
