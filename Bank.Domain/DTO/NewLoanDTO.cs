using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.DTO
{
    public class NewLoanDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
