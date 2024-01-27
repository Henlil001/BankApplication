using AutoMapper;
using Bank.Core.Service;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Moq;

namespace Bank.Test
{
    public class AccountsTest
    {
        [Theory]
        [InlineData(3, 0)]
        [InlineData(4, 0)]
        [InlineData(5, 0)]
        [InlineData(6, 0)]
        [InlineData(7, 0)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        public void Test_Create_New_Account__(int accountType, int expectedReturn)
        {
            var accountRepo = new Mock<IAccountsRepo>();
            var mapper = new Mock<IMapper>();

            var userInput = new CreateAccountDTO { Frequency = "Monthly", AccountType1ForStandardPrivat2ForSavings = accountType, };

            accountRepo.Setup(repo => repo.CreateNewAccount(userInput, 1)).Returns(2);

            var service = new AccountsService(accountRepo.Object, mapper.Object);

            var result = service.CreateNewAccount(userInput, 1);

            Assert.Equal(expectedReturn, result);
        }
    }
}
