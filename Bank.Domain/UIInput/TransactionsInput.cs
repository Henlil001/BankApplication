using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.UIInput
{
    public class TransactionsInput
    {
        public int AccountToTransferTo { get; set; }
        public int AccountToTransferFrom { get; set; }
        public decimal Amount { get; set; }

    }
}
