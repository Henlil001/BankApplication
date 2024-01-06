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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly ILoginRepo _loginRepo;

        public CustomerService(ICustomerRepo customerRepo, ILoginRepo loginRepo)
        {
            _customerRepo = customerRepo;
            _loginRepo = loginRepo;
        }


        public NewCustomer CreateCostumer(Domain.Entites.Login login, Accounts accounts)
        {
            var newCustomer = new NewCustomer();

            if (login.Customer is null ||
                login.Customer.Gender is null ||
                login.Customer.GivenName is null ||
                login.Customer.SurName is null ||
                login.Customer.StreetAddress is null ||
                login.Customer.City is null ||
                login.Customer.ZipCode is null ||
                login.Customer.Country is null ||
                login.Customer.CountryCode is null ||
                accounts is null ||
                login.Password.Length < 5 ||
                login.UserName is null)
                return newCustomer;


            login.Password = BCrypt.Net.BCrypt.HashPassword(login.Password);

            var check = _loginRepo.CheckUsername(login);

            if (check != null)
                return newCustomer;

            newCustomer = _customerRepo.CreateCustomer(login, accounts);
            newCustomer.CorrectInput = true;
            return newCustomer;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetAllCustomers();
        }

        
    }
}
