using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Interfaces
{
    public interface IAccountsRepo
    {
        List<Accounts> ShowAccounts(int customerId);
        int CreateNewAccount(Accounts accounts, int customerId);

    }
}
