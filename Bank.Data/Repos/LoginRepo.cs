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
    public class LoginRepo : ILoginRepo
    {
        private readonly IBankDBContext _DBContext;

        public LoginRepo(IBankDBContext dBContext)
        {
            _DBContext = dBContext;
        }
        public Login? GetLogin(Login login)
        {
            using (IDbConnection db = _DBContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", login.UserName);
                parameters.Add("@Password", login.Password);

                return db.QuerySingleOrDefault<Login>("CheckLogin", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public Login? CheckUsername(string username)
        {
            using (IDbConnection db = _DBContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", username);

                return db.QuerySingleOrDefault<Login>("CheckUserName", parameters, commandType: CommandType.StoredProcedure);

            }
        }
        public NewCustomerDTO CreateLoginToExictingCustomer(Login login)
        {
            using (IDbConnection db = _DBContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", login.UserName);
                parameters.Add("@Password", login.Password);
                parameters.Add("@Role", login.Role);
                parameters.Add("@Givenname", login.Customer.GivenName);
                parameters.Add("@Surname", login.Customer.SurName);
                parameters.Add("@StreetAddress", login.Customer.StreetAddress);
                parameters.Add("@Birthday", login.Customer.Birthday);

                parameters.Add("@CustomerId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@LoginId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Exekvera den lagrade proceduren
                db.Execute("CreateLoginToExistingCustomer", parameters, commandType: CommandType.StoredProcedure);

                // Hämta utmatningsvärdena
                //int customerId = parameters.Get<int>("@CustomerId");
                //int loginId = parameters.Get<int>("@LoginId");

                // Skapa och returnera en NewCustomer med de erhållna värdena
                return new NewCustomerDTO
                {
                    CustomerId = parameters.Get<int>("@CustomerId"),
                    LoginId = parameters.Get<int>("@LoginId")
                    
                };

                // return db.QuerySingle<NewCustomer>("CreateLoginToExictingCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
