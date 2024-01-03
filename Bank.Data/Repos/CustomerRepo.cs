using Bank.Data.Interfaces;
using Bank.Domain.Entites;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly IBankDBContext _dbContext;
        public CustomerRepo(IBankDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        
        public List<Customer> GetAllCustomers()
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                string sql = "select * from Customers";
                return db.Query<Customer>(sql).ToList();
            }
        }
    }
}
