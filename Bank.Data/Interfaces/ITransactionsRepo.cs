using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Interfaces
{
    public interface ITransactionsRepo
    {
        List<Transactions> ShowTransactions(int accountId);
        void TransferMoney(Transactions transactions);
    }
}
