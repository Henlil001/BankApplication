using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Domain.Entites
{
    public class Cards
    {
        public int CardId { get; set; }
        public Dispositions Dispositions { get; set; }
        public string Type { get; set; }
        public DateTime Issued { get; set; }
        public string CCType { get; set; }
        public string CCNumber { get; set; }
        public string CVV2 { get; set; }
        public int ExpM { get; set; }
        public int ExpY { get; set; }
    }
}
