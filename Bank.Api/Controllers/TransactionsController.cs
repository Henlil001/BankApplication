using Bank.Core.Interfaces;
using Bank.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
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
        [Route("Show_Transactions_For_A_CustomerAccount")]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult ShowTransactions(int accountId)
        {
            try
            {
                return Ok(_transactionsService.ShowTransactionsAsync(accountId).Result); ;
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, ShowTransactions");
                throw;
            }
        }
        [Route("Transfer_Money")]
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult TransferMoney(TransactionsInput transaction)
        {
           bool check = _transactionsService.TransferMoney(transaction);
            if (check) return Ok("Transfer successfull.");
            else return StatusCode(500, "Transfer Failed");
        }
    }
}
