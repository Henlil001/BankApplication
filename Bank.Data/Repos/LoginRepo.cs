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
        public Login? CheckUsername(Login login)
        {
            using (IDbConnection db = _DBContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", login.UserName);

                return db.QuerySingleOrDefault<Login>("CheckUserName", parameters, commandType: CommandType.StoredProcedure);

    }
        }
    }
}
