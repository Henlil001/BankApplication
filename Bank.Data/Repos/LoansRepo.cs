using Bank.Data.Interfaces;
using Bank.Domain.DTO;
using Dapper;
using System.Data;

namespace Bank.Data.Repos
{
    public class LoansRepo : ILoansRepo
    {
        private readonly IBankDBContext _dbContext;
        public LoansRepo(IBankDBContext bankDB)
        {
            _dbContext = bankDB;
        }
        public int NewLoan(NewLoanDTO loan)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AccountId", loan.AccountId);
                parameters.Add("@Date", DateTime.Now);
                parameters.Add("@Amount", loan.Amount);
                parameters.Add("@Duration", 60);
                parameters.Add("@Payments", 0, 00);
                parameters.Add("@Status", "Running - Client in debt");

                return db.Query<int>("NewLoan", parameters, commandType: CommandType.StoredProcedure).Single();



            }
        }
    }
}
