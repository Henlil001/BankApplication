﻿using AutoMapper;
using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
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
        private readonly IMapper _mapper;
        public AccountsService(IAccountsRepo accountsRepo, IMapper mapper)
        {
            _accountsRepo = accountsRepo;
            _mapper = mapper;
        }
        public List<AccountsDTO> ShowAccounts(int customerId)
        {
            var accounts = _accountsRepo.ShowAccounts(customerId);

            var accountsDTO = new List<AccountsDTO>();

         

            foreach (var account in accounts)
            {

                var mapedaAccounts = new AccountsDTO
                {
                    AccountId = account.AccountId,
                    AccountType = account.AccountTypes?.TypeName,
                    Balance = account.Balance,
                    // Lägg till andra egenskaper här...
                };
                //var mapedaAccounts = _mapper.Map<AccountsDTO>(account);
                accountsDTO.Add(mapedaAccounts);
            }
            return accountsDTO;
        }
        public int CreateNewAccount(CreateAccountDTO account, int customerId)
        {
            if (account.Frequency is null && !(account.AccountType1ForStandardPrivat2ForSavings==1 || account.AccountType1ForStandardPrivat2ForSavings == 2))
                return 0;
            
            return _accountsRepo.CreateNewAccount(account, customerId);
        }


    }
}
