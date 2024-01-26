using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Service
{
    public class LoansService : ILoansService
    {
        private readonly ILoansRepo _loansRepo;
        private readonly IAccountsRepo _accountsRepo;
        public LoansService(ILoansRepo loansRepo, IAccountsRepo accountsRepo)
        {
            _loansRepo = loansRepo;
            _accountsRepo = accountsRepo;

        }

        public int NewLoan(NewLoanDTO loan)
        {
            
            if (loan.AccountId.ToString().Length == 0|| loan.Amount.ToString().Length == 0) 
                return -1;
                 
            var account = _accountsRepo.GetAccount(loan.AccountId);
            if (account == null) return 0;

            int loanID = _loansRepo.NewLoan(loan);
            return loanID;
        }
    }
}
