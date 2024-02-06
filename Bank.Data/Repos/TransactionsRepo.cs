using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using Dapper;
using System.Data;

namespace Bank.Data.Repos
{
    public class TransactionsRepo : ITransactionsRepo
    {
        private readonly IBankDBContext _dbContext;
        public TransactionsRepo(IBankDBContext bankDBContext)
        {
            _dbContext = bankDBContext;
        }
       
        public async Task<List<Transactions>> ShowTransactionsAsync(int accountId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountId", accountId);

                var showTransactions = await db.QueryAsync<Transactions, Accounts, Transactions>(
                    "ShowTransactions",
                    (transactions, accounts) =>
                    {
                        transactions.Accounts = accounts;
                        return transactions;
                    },
                    param: parameters,
                    splitOn: "AccountId",
                    commandType: CommandType.StoredProcedure
                );

                return showTransactions.ToList();
            }
        }

        public void TransferMoney(TransactionsInput transactions)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AccountToTransferTo", transactions.AccountToTransferTo);
            parameters.Add("@AccountToTransferFrom", transactions.AccountToTransferFrom);
            parameters.Add("@Date", DateTime.Now);
            parameters.Add("@Amount", transactions.Amount);


            using (IDbConnection db = _dbContext.GetConnection())
            {
                db.Execute("TransferMoney", parameters, commandType: CommandType.StoredProcedure);
            }

        }
    }
}
