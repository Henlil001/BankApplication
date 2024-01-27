using Bank.Domain.DTO;

namespace Bank.Core.Interfaces
{
    public interface ILoansService
    {
        int NewLoan(NewLoanDTO loan);
    }
}
