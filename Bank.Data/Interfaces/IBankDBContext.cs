using Microsoft.Data.SqlClient;

namespace Bank.Data.Interfaces
{
    public interface IBankDBContext
    {
        SqlConnection GetConnection();
    }
}
