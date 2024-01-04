using Bank.Domain.DTO;
using Bank.Domain.Entites;
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
        NewCustomer CreateCustomer(Domain.Entites.Login login, Accounts accounts);
    }
}
