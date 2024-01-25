using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Bank.Domain.UIInput;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

        public async Task<NewCustomerDTO> CreateCustomer(CreateNewCustomerInput customer)
        {
            return await Task.Run(() =>
            {
                using (IDbConnection db = _dbContext.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserName", customer.Username);
                    parameters.Add("@Password", customer.Password);
                    parameters.Add("@Role", "Customer");
                    parameters.Add("@Gender", customer.Gender);
                    parameters.Add("@GivenName", customer.GivenName);
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
                    parameters.Add("@Balance", 0, 00);
                    parameters.Add("@AccountType", 2);
                    parameters.Add("@Type", customer.TypeOWNERorDISPONENT);

                    // Lägg till utmatningsparametrar
                    parameters.Add("@CustomerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@LoginId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@AccountId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                    // Exekvera den lagrade proceduren
                    db.Execute("CreateCustomer", parameters, commandType: CommandType.StoredProcedure);


                    // Skapa och returnera en NewCustomer med de erhållna värdena
                    return new NewCustomerDTO
                    {
                        CustomerId = parameters.Get<int>("@CustomerId"),
                        LoginId = parameters.Get<int>("@LoginId"),
                        AccountId = parameters.Get<int>("@AccountId")
                    };
                }
            });


        }


    }


}


