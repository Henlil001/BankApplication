using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.DTO
{
    public class AccountsDTO
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}
