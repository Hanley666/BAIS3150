using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Models
{
    public class SaleItem
    {
        public string ItemCode { get; set; }

        public int SaleNumber { get; set; }

        public int Quantity { get; set; }

        public decimal ItemTotal { get; set; }
    }
}
