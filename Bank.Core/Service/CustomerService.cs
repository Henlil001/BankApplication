using Bank.Core.Interfaces;
using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
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


        public NewCustomerDTO CreateCostumer(CreateNewCustomerInput createNewCustomer)
        {

            var newCustomer = new NewCustomerDTO();

            var check = _loginRepo.CheckUsername(createNewCustomer.Username);

            if (check != null ||
                createNewCustomer.Username is null || createNewCustomer.Password is null ||
                createNewCustomer.Password.Length < 5 ||
                createNewCustomer.Gender is null || createNewCustomer.GivenName is null ||
                createNewCustomer.SurName is null || createNewCustomer.StreetAddress is null ||
                createNewCustomer.City is null || createNewCustomer.ZipCode is null ||
                createNewCustomer.Country is null || createNewCustomer.CountryCode is null ||
                createNewCustomer.Birthday is null || createNewCustomer.Frequency is null ||
                createNewCustomer.AccountTypeName is null || createNewCustomer.AccountTypeDescription is null)
                return newCustomer;

            createNewCustomer.Password = BCrypt.Net.BCrypt.HashPassword(createNewCustomer.Password);

            newCustomer = _customerRepo.CreateCustomer(createNewCustomer);

            if(newCustomer.CustomerId.ToString().Length == 0|| newCustomer.AccountId.ToString().Length == 0|| newCustomer.LoginId.ToString().Length ==0)
                return newCustomer;

            newCustomer.CorrectInput = true;
            return newCustomer;
        }



        public List<Customer> GetAllCustomers()
        {
            string password = BCrypt.Net.BCrypt.HashPassword("henrik123");


            return _customerRepo.GetAllCustomers();
        }



    }
}
