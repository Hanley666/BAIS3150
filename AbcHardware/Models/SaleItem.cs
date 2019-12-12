using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Models
{
    public class SaleItem
    {
        public string ItemCode { get; set; }

        public string Description { get; set; }

        public int QuantityOnHand { get; set; }

        public decimal UnitPrice { get; set; }

        public int? Quantity { get; set; }

        public string UnitPriceDisplay => $"{string.Format("{0:C}", (UnitPrice == 0 ? (decimal?)null : UnitPrice))}";
    }
}
