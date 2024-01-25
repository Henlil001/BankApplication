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
        public Login? GetLogin(string username, string password)
        {
            using (IDbConnection db = _DBContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", username);
                parameters.Add("@Password", password);

                var login = db.Query<Login, Customer, Login>("CheckLogin", (login, customer) =>
                {
                    login.Customer = customer;
                    return login;
                }, param: parameters, splitOn: "CustomerId", commandType: CommandType.StoredProcedure).SingleOrDefault();
                return login;
            }
        }

        public async Task<Login?> CheckUsername(string username)
        {
            return await Task.Run(() =>
            {
                using (IDbConnection db = _DBContext.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserName", username);

                    var login = db.Query<Login, Customer, Login>("CheckUserName",
                        (login, customer) =>
                        {
                            login.Customer = customer;
                            return login;
                        }, param: parameters, splitOn: "CustomerId", commandType: CommandType.StoredProcedure).SingleOrDefault();
                    return login;
                }
            });
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
