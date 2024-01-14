using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interfaces
{
    public interface ITransactionsService
    {
        List<TransactionsDTO> ShowTransactions(int accountId);
        bool TransferMoney(TransactionsInput transactions);
    }
}
