using Bank.Domain.DTO;

namespace Bank.Core.Interfaces
{
    public interface IAccountsService
    {
        List<AccountsDTO> ShowAccounts(int customerId);
        int CreateNewAccount(CreateAccountDTO account, int customerId);
    }
}
