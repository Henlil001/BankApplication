using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
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
        public LoansService(ILoansRepo loansRepo)
        {
            _loansRepo = loansRepo;
        }

        public int NewLoan(Loans loans)
        {
            if (loans.Accounts.AccountId.ToString() is null||
                loans.Amount.ToString() is null||
                loans.Duration.ToString() is null||
                loans.Payments.ToString() is null||
                loans.Status is null)
                return 0;

            int loanId = _loansRepo.NewLoan(loans);
            return loanId;
        }
    }
}
