using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Core.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string username, string password);

        (NewCustomerDTO, bool) CreateLoginToExictingCustomer(Login login);

    }
}
