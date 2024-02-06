using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Data.Interfaces
{
    public interface ILoginRepo
    {
        Task<Login?> CheckUsernameAsync(string username);
        NewCustomerDTO CreateLoginToExictingCustomer(Login login);
    }
}
