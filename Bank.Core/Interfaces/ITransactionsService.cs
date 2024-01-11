using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interfaces
{
    public interface ITransactionsService
    {
        List<Transactions> ShowTransactions(int accountId);
        bool TransferMoney(Transactions transactions);
    }
}
