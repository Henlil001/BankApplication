﻿using Bank.Core.Interfaces;
using Bank.Domain.UIInput;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

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
                return Ok(_transactionsService.ShowTransactions(accountId));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error, ShowTransactions");
                throw;
            }
        }

        [HttpPost]
        public IActionResult TransferMoney(TransactionsInput transaction)
        {
           bool check = _transactionsService.TransferMoney(transaction);
            if (check) return Ok("Transfer successfull.");
            else return StatusCode(500, "Transfer Failed");
        }
    }
}
