using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.DTO
{
    public class CreateAccountDTO
    {
        public string Frequency { get; set; }
        public int AccountType1ForStandardPrivat2ForSavings { get; set; }
    }
}
