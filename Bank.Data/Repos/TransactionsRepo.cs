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
    public class TransactionsRepo : ITransactionsRepo
    {
        private readonly IBankDBContext _dbContext;
        public TransactionsRepo(IBankDBContext bankDBContext)
        {
            _dbContext = bankDBContext;
        }
        public List<Transactions> ShowTransactions(int accountId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountId", accountId);

                //return db.Query<Transactions>("ShowTransactions", parameters, commandType:CommandType.StoredProcedure).ToList();

                var showTransactions = db.Query<Transactions, Accounts, Transactions>("ShowTransactions",
                    (transactions, accounts) =>
                    {
                        transactions.Accounts = accounts;
                        return transactions;
                    }, param: parameters,
                    splitOn: "AccountId",
                    commandType: CommandType.StoredProcedure).ToList();
                return showTransactions;
            }
        }

        public void TransferMoney(Transactions transactions)
        {
            throw new NotImplementedException();
        }
    }
}
