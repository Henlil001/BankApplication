using Bank.Domain.DTO;
using Bank.Domain.Entites;

namespace Bank.Data.Interfaces
{
    public interface ILoginRepo
    {
        Login? GetLogin(string username, string password);
        Task<Login?> CheckUsernameAsync(string username);
        NewCustomerDTO CreateLoginToExictingCustomer(Login login);
    }
}
