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
        public int CreateNewAccount(Accounts accounts, int customerId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Frequensy", accounts.Frequency);
                parameters.Add("@Date", DateTime.Now);
                parameters.Add("@Balance", 0);
                parameters.Add("@AccountTypeName", accounts.AccountTypes.TypeName);
                parameters.Add("@Description", accounts.AccountTypes.Description);
                parameters.Add("@CustomerId", customerId);

                return db.QuerySingle<int>("NewAccount", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Accounts> ShowAccounts(int customerId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters Parameters = new DynamicParameters();
                Parameters.Add("@CustomerID", customerId);

                //return db.Query<Accounts>("ShowAccounts",Parameters,commandType:CommandType.StoredProcedure).ToList();

                var showAccounts = db.Query<Accounts, AccountTypes, Accounts>("ShowAccounts",
                    (accounts, accountTypes) =>
                    {
                        accounts.AccountTypes = accountTypes;
                        return accounts;
                    }, param: Parameters,
                    splitOn: "TypeName",
                    commandType: CommandType.StoredProcedure).ToList();
                return showAccounts;


            }
        }
        public Accounts? GetAccount(int accountId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AccountId", accountId);

            using (IDbConnection db = _dbContext.GetConnection())
            {
                var account = db.Query<Accounts, AccountTypes, Accounts>("GetAccount",
                    (accounts, accountType) =>
                {
                    accounts.AccountTypes = accountType;
                    return accounts;
                }, param: parameters, splitOn: "", commandType: CommandType.StoredProcedure).SingleOrDefault();
                return account;
            }
        }


    }
}
