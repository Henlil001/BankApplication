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
        [InlineData(3, 0)] //Fel inparameter, inget kontoSkapas
        [InlineData(4, 0)] //Fel inparameter, inget kontoSkapas
        [InlineData(5, 0)] //Fel inparameter, inget kontoSkapas
        [InlineData(6, 0)] //Fel inparameter, inget kontoSkapas
        [InlineData(7, 0)] //Fel inparameter, inget kontoSkapas
        [InlineData(1, 2)] //Här skapas ett nytt privatkonto
        [InlineData(2, 2)] //Här skapas ett nytt sparkonto
        public void Test_CreateNewAccount__AccountType_Most_Be_1_Or_2_To_Create_New_Account(int accountType, int expectedReturn)
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
