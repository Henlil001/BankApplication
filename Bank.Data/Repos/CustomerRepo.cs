using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
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

        public NewCustomerDTO CreateCustomer(CreateNewCustomerInput customer)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", customer.Username);
                parameters.Add("@Password", customer.Password);
                parameters.Add("@Gender", customer.Gender);
                parameters.Add("@Givename", customer.GivenName);
                parameters.Add("@Surname", customer.SurName);
                parameters.Add("@Streetaddress", customer.StreetAddress);
                parameters.Add("@City", customer.City);
                parameters.Add("@Zipcode", customer.ZipCode);
                parameters.Add("@Country", customer.Country);
                parameters.Add("@CountryCode", customer.CountryCode);
                parameters.Add("@Birthday", customer.Birthday);
                parameters.Add("@TelephoneCountryCode", customer.TelephoneCountryCode);
                parameters.Add("@TelephoneNumber", customer.TelephoneNumber);
                parameters.Add("@Emailaddress", customer.EmailAddress);
                parameters.Add("@Frequency", customer.Frequency);
                parameters.Add("@Created", DateTime.Now);
                parameters.Add("@Balance", 0);
                parameters.Add("@AccountType", customer.AccountTypeName);
                parameters.Add("@Description", customer.AccountTypeDescription);
                parameters.Add("@Role", "Customer");
                parameters.Add("@Type", "OWNER");

                // Lägg till utmatningsparametrar
                parameters.Add("@CustomerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@LoginId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@AccountId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Exekvera den lagrade proceduren
                db.Execute("CreateCustomer", parameters, commandType: CommandType.StoredProcedure);

                // Hämta utmatningsvärdena
                //int customerId = parameters.Get<int>("@CustomerId");
                //int loginId = parameters.Get<int>("@LoginId");
                //int accountId = parameters.Get<int>("@AccountId");

                // Skapa och returnera en NewCustomer med de erhållna värdena
                return new NewCustomerDTO
                {
                    CustomerId = parameters.Get<int>("@CustomerId"),
                    LoginId = parameters.Get<int>("@LoginId"),
                    AccountId = parameters.Get<int>("@AccountId")
                };

                // Eventuella andra attribut från NewCustomer-klassen
            }

            //return db.Query<NewCustomer>("CreateCustomer", parameters, commandType: CommandType.StoredProcedure).Single();


        }

        
    }
}

