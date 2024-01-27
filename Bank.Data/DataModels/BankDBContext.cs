using Bank.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace Bank.Data.DataModels
{
    public class BankDBContext : IBankDBContext
    {
        private readonly string? _DBContext;
        public BankDBContext(IConfiguration configuration)
        {
            _DBContext = configuration.GetConnectionString("BankDBString");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_DBContext);
        }
    }
}
