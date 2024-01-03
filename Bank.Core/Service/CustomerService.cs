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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public List<Customer> GetAllCustomers()
        {
           return _customerRepo.GetAllCustomers();
        }
    }
}
