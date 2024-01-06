﻿using Bank.Domain.DTO;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interfaces
{
    public interface ILoginService
    {
        LoginToken LoginAdmin(Login login);
        LoginToken LoginCustomer(Login login);

        NewCustomer CreateLoginToExictingCustomer(Login login);

    }
}
