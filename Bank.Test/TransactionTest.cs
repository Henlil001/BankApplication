using AutoMapper;
using Bank.Core.Service;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Moq;

namespace Bank.Test
{
    public class TransactionTest
    {
        [Fact]
        public void TransferMoney_WhenSuccessful_ReturnsTrue()
        {
            bool expectReturn = true;

            var transactionRepo = new Mock<ITransactionsRepo>();
            var acountRepo = new Mock<IAccountsRepo>();
            var mapper = new Mock<IMapper>();
            var dispositionsRepo = new Mock<IDispositionsRepo>();

            var transactionInput = new TransactionsInput(1, 2, 1000.5m);
            var acountTo = new Accounts(1, 50);
            var accountFrom = new Accounts(2, 2000);

            acountRepo.Setup(repo => repo.GetAccount(accountFrom.AccountId)).Returns(accountFrom);
            acountRepo.Setup(repo => repo.GetAccount(acountTo.AccountId)).Returns(acountTo);
            
            var service = new TransactionsService(transactionRepo.Object, mapper.Object, acountRepo.Object, dispositionsRepo.Object);

            var result = service.TransferMoney(transactionInput);

            Assert.Equal(expectReturn, result);
        }

    }
}
