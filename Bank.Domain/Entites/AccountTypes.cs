using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Entites
{
    public class AccountTypes
    {
        public int AccountTypeId { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    }
}
