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
            
                
            createNewCustomer.Password = BCrypt.Net.BCrypt.HashPassword(createNewCustomer.Password);

            var check = _loginRepo.CheckUsername(createNewCustomer.Username);

            if (check != null)
                return newCustomer;

            newCustomer = _customerRepo.CreateCustomer(createNewCustomer);
            newCustomer.CorrectInput = true;
            return newCustomer;
        }

        

        public List<Customer> GetAllCustomers()
        {
            return  _customerRepo.GetAllCustomers();
        }



    }
}
