using Bank.Domain.DTO;

namespace Bank.Core.Interfaces
{
    public interface ITransactionsService
    {
       Task<List<TransactionsDTO>> ShowTransactionsAsync(int accountId);
        bool TransferMoney(TransactionsInput transactions);
    }
}
