using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.DTO
{
    public class NewCustomerDTO
    {
        public int LoginId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public bool CorrectInput { get; set; }

    }
}
