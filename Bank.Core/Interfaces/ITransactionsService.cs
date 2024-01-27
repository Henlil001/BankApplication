using Bank.Domain.DTO;

namespace Bank.Core.Interfaces
{
    public interface ITransactionsService
    {
        List<TransactionsDTO> ShowTransactions(int accountId);
        bool TransferMoney(TransactionsInput transactions);
    }
}
