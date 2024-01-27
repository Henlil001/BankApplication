﻿using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;

namespace Bank.Core.Service
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepo _transactionsRepo;
        private readonly IAccountsRepo _accountsRepo;
        private readonly IMapper _mapper;
        public TransactionsService(ITransactionsRepo transactionsRepo, IMapper mapper, IAccountsRepo accountsRepo)
        {
            _transactionsRepo = transactionsRepo;
            _mapper = mapper;
            _accountsRepo = accountsRepo;
        }
        public List<TransactionsDTO> ShowTransactions(int accountId)
        {
            var transactions = _transactionsRepo.ShowTransactionsAsync(accountId).Result;

            var transactionsDTO = new List<TransactionsDTO>();

            foreach (var transaction in transactions)
            {
                var mapedTransactions = new TransactionsDTO
                {
                    Date = transaction.Date,
                    Operation = transaction.Operation,
                    Amount = transaction.Amount,
                    Balance = transaction.Balance,
                    Comment = transaction.Symbol

                };
                //var mappedTransactions = _mapper.Map<TransactionsDTO>(transaction);
                transactionsDTO.Add(mapedTransactions);
            }
            return transactionsDTO;
        }

        public bool TransferMoney(TransactionsInput transactions)
        {
            try
            {
                if (transactions.AccountToTransferFrom.ToString() is null ||
                    transactions.AccountToTransferTo.ToString() is null ||
                    transactions.Amount.ToString() is null)
                    return false;

                var accountFrom = _accountsRepo.GetAccount(transactions.AccountToTransferFrom);
                var accountTo = _accountsRepo.GetAccount(transactions.AccountToTransferTo);

                if (accountFrom == null ||accountTo == null || accountFrom.Balance < transactions.Amount) 
                    return false;

                _transactionsRepo.TransferMoney(transactions);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
