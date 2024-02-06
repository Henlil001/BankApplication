using Bank.Domain.DTO;

namespace Bank.Core.Interfaces
{
    public interface ITransactionsService
    {
        Task<(List<TransactionsDTO>, bool)> ShowTransactionsAsync(int accountId, int id);
        bool TransferMoney(TransactionsInput transactions);
    }
}
