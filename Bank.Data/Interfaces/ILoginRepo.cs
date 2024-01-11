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
        Login? GetLogin(Login login);
        Login? CheckUsername(string username);
        NewCustomerDTO CreateLoginToExictingCustomer(Login login);
    }
}
