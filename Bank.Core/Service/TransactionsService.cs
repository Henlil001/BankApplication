using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Service
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepo _transactionsRepo;
        private readonly IMapper _mapper;
        public TransactionsService(ITransactionsRepo transactionsRepo, IMapper mapper)
        {
            _transactionsRepo = transactionsRepo;
            _mapper = mapper;
        }
        public List<TransactionsDTO> ShowTransactions(int accountId)
        {
            //har kvar att mappa data till UI
            var transactions = _transactionsRepo.ShowTransactions(accountId);

            var transactionsDTO = new List<TransactionsDTO>();

            foreach (var transaction in transactions)
            {
                var mappedTransactions = _mapper.Map<TransactionsDTO>(transaction);
                transactionsDTO.Add(mappedTransactions);
            }
            return transactionsDTO;
        }

        public bool TransferMoney(TransactionsInput transactions)
        {
            try
            {
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
