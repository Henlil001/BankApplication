using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Data.Interfaces
{
    public interface ITransactionsRepo
    {
        Task<List<Transactions>> ShowTransactionsAsync(int accountId);
        void TransferMoney(TransactionsInput transactions);
    }
}
