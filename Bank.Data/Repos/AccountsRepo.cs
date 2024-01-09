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
    public class AccountsRepo : IAccountsRepo
    {
        private readonly IBankDBContext _dbContext;
        public AccountsRepo(IBankDBContext bankDBContext)
        {
            _dbContext = bankDBContext;
        }
        public void CreateNewAccount(Accounts accounts, int customerId)
        {
            throw new NotImplementedException();
        }

        public List<Accounts> ShowAccounts(int customerId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters Parameters = new DynamicParameters();
                Parameters.Add("@CustomerID", customerId);

                return db.Query<Accounts>("ShowAccounts",Parameters,commandType:CommandType.StoredProcedure).ToList();
            }
        }
    }
}
