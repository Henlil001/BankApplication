using Bank.Domain.Entites;
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
        void CreateNewAccount(Accounts accounts, int customerId);
        
    }
}
