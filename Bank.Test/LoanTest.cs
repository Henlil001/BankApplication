using Bank.Core.Service;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Moq;

namespace Bank.Test
{
    public class LoanTest
    {
        [Fact]
        public void NewLoan_Return_New_LoanID_When_Seccsefull()
        {
            int expectedreturn = 45;

            var loanRepo = new Mock<ILoansRepo>();
            var acountRepo = new Mock<IAccountsRepo>();

            var loanInput = new NewLoanDTO { AccountId = 1, Amount = 1000 };
            var checkAccount = new Accounts(1, 50);

            acountRepo.Setup(repo => repo.GetAccount(loanInput.AccountId)).Returns(checkAccount);
            loanRepo.Setup(repo => repo.NewLoan(loanInput)).Returns(45);

            var service = new LoansService(loanRepo.Object, acountRepo.Object);

            var result = service.NewLoan(loanInput);

            Assert.Equal(expectedreturn, result);

        }
    }
}
