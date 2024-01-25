using Bank.Domain.DTO;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Interfaces
{
    public interface ILoginRepo
    {
        Login? GetLogin(string username, string password);
        Task<Login?> CheckUsernameAsync(string username);
        NewCustomerDTO CreateLoginToExictingCustomer(Login login);
    }
}
