﻿using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interfaces
{
    public interface IAccountsService
    {
        List<Accounts> ShowAccounts(int customerId);
        int CreateNewAccount(Accounts accounts, int customerId);
    }
}