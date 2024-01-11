using Bank.Core.Interfaces;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Service
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsService _transactionsService;
        public TransactionsService(ITransactionsService transactions)
        {
            _transactionsService = transactions;
        }
        public List<Transactions> ShowTransactions(int accountId)
        {
            //har kvar att mappa data till UI
            return _transactionsService.ShowTransactions(accountId);
        }

        public bool TransferMoney(Transactions transactions)
        {
            throw new NotImplementedException();
        }
    }
}
