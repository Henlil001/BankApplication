using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.DTO
{
    public class NewCustomer
    {
        public int LoginId { get; set; }
        public int CustomerId { get; set; }
        public int accountId { get; set; }
        public bool CorrectInput { get; set; }

    }
}
