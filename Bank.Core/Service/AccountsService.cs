using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Service
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepo _accountsRepo;
        public AccountsService(IAccountsRepo accountsRepo)
        {
            _accountsRepo = accountsRepo;
        }
        public List<Accounts> ShowAccounts(int customerId)
        {
            //har kvar att mappa data till UI
            return _accountsRepo.ShowAccounts(customerId);
        }
        public int CreateNewAccount(Accounts accounts, int customerId)
        {
            if (accounts is null)
                return 0;
            return _accountsRepo.CreateNewAccount(accounts, customerId);
        }


    }
}
