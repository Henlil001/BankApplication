using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();

        NewCustomerDTO CreateCostumer(CreateNewCustomerInput createNewCustomer);

        


    }
}
