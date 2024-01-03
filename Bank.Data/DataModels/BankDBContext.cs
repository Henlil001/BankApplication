using Bank.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Data.DataModels
{
    public class BankDBContext : IBankDBContext
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=SOL0202\\SQLEXPRESS;Initial Catalog=BankAppData;Integrated Security=true;trustservercertificate=true");
        }
    }
}
