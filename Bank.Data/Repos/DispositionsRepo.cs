using Bank.Data.Interfaces;
using Bank.Domain.Entites;
using Dapper;
using System.Data;

namespace Bank.Data.Repos
{
    public class DispositionsRepo : IDispositionsRepo
    {
        private readonly IBankDBContext _dbContext;
        public DispositionsRepo(IBankDBContext bankDBContext)
        {
            _dbContext = bankDBContext;
        }
        List<Domain.Entites.Dispositions> IDispositionsRepo.GetDispositions(int userId)
        {
            using (IDbConnection db = _dbContext.GetConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", userId);

                return db.Query<Dispositions>("GetDispositions", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
