using Bank.Data.Interfaces;
using Bank.Domain.DTO;
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

        public NewCustomer CreateCustomer(Login login, Accounts accounts)
        {
            using(IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", login.UserName);
                parameters.Add("@Password", login.Password);
                parameters.Add("@Gender", login.Customer.Gender);
                parameters.Add("@Givename", login.Customer.GivenName);
                parameters.Add("@Surname", login.Customer.SurName);
                parameters.Add("@Streetaddress", login.Customer.StreetAddress);
                parameters.Add("@City", login.Customer.City);
                parameters.Add("@Zipcode", login.Customer.ZipCode);
                parameters.Add("@Country", login.Customer.Country);
                parameters.Add("@CountryCode", login.Customer.CountryCode);
                parameters.Add("@Birthday", login.Customer.Birthday);
                parameters.Add("@TelephoneCountryCode", login.Customer.TelephoneCountryCode);
                parameters.Add("@TelephoneNumber", login.Customer.TelephoneNumber);
                parameters.Add("@Emailaddress", login.Customer.EmailAddress);
                parameters.Add("@Frequency", accounts.Frequency);
                parameters.Add("@Created", DateTime.Now);
                parameters.Add("@Balance", accounts.Balance);
                parameters.Add("@AccountType", accounts.AccountTypes.TypeName);
                parameters.Add("@Description", accounts.AccountTypes.Description);
                parameters.Add("@Role", "Customer");
                parameters.Add("@Type", "OWNER");
                

                return db.Query<NewCustomer>("CreateCustomer", parameters, commandType: CommandType.StoredProcedure).Single();

                
            }
        }
    }
}
