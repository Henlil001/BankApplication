using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Data.Interfaces
{
    public interface IAccountsRepo
    {
        List<Accounts> ShowAccounts(int customerId);
        int CreateNewAccount(CreateAccountDTO account, int customerId);
        Accounts? GetAccount(int AccountId);

    }
}
