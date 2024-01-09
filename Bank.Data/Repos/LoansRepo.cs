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
    public class LoansRepo : ILoansRepo
    {
        private readonly IBankDBContext _dbContext;
        public LoansRepo(IBankDBContext bankDB)
        {
            _dbContext = bankDB;
        }
        public int NewLoan(Loans loan)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountId", loan.Accounts.AccountId);
                parameters.Add("@Date", DateTime.Now);
                parameters.Add("@Amount", loan.Amount);
                parameters.Add("@Duration", loan.Duration);
                parameters.Add("@Payments", loan.Payments);
                parameters.Add("@Status", loan.Status);

                return db.QuerySingle<int>("NewLoan", parameters, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
