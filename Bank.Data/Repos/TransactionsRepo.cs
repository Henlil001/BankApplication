using Bank.Data.Interfaces;
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

        public void TransferMoney(TransactionsInput transactions)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AccountToTransferTo", transactions.AccountToTransferTo);
            parameters.Add("@AccountToTransferFrom", transactions.AccountToTransferFrom);
            parameters.Add("@Date", DateTime.Now);
            parameters.Add("@Type", transactions.Type);
            parameters.Add("@Amount", transactions.Amount);
            parameters.Add("@Balance", transactions.Balance); //Kolla om denna ska vara me sen när jag gör stored procedure
            parameters.Add("@Symbol", transactions.Symbol);
            parameters.Add("@Bank", transactions.Bank);
            parameters.Add("@Account", transactions.Account);

            using (IDbConnection db = _dbContext.GetConnection())
            {
                db.Execute("TransferMoney", parameters, commandType: CommandType.StoredProcedure);
            }

        }
    }
}
