using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Interfaces
{
    public interface ICustomerRepo
    {
        List<Customer> GetAllCustomers();
        NewCustomerDTO CreateCustomer(CreateNewCustomerInput createNewCustomer);

    }
}
