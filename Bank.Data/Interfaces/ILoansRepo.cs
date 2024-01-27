using Bank.Domain.DTO;

namespace Bank.Data.Interfaces
{
    public interface ILoansRepo
    {
        int NewLoan(NewLoanDTO loan);
    }
}
