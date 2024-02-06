using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Dapper;
using System.Data;

namespace Bank.Data.Repos
{
    public class LoginRepo : ILoginRepo
    {
        private readonly IBankDBContext _DBContext;

        public LoginRepo(IBankDBContext dBContext)
        {
            _DBContext = dBContext;
        }

        public async Task<Login?> CheckUsernameAsync(string username)
        {

            using (IDbConnection db = _DBContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", username);

                var login = await db.QueryAsync<Login, Customer, Login>("CheckUserName",
                    (login, customer) =>
                    {
                        login.Customer = customer;
                        return login;
                    }, param: parameters, splitOn: "CustomerId", commandType: CommandType.StoredProcedure);
                return login.SingleOrDefault();
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

                db.Execute("CreateLoginToExistingCustomer", parameters, commandType: CommandType.StoredProcedure);

                // Skapa och returnera en NewCustomer med de erhållna värdena
                return new NewCustomerDTO
                {
                    CustomerId = parameters.Get<int>("@CustomerId"),
                    LoginId = parameters.Get<int>("@LoginId")

                };


            }
        }
    }
}
