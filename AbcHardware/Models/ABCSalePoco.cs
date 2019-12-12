using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Models
{
    public class ABCSalePoco
    {
        public int CustomerId { get; set; }

        public string SalePerson { get; set; }

        public List<SaleItem> SaleItems { get; set; }

        public ABCSalePoco()
        {
            SaleItems = new List<SaleItem>();
        }
    }
}
