using AbcHardware.Models;
using AbcHardware.TechnicalServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Domain
{
    public class ABCPOS
    {
        private readonly Sales _SalesManager;
        public ABCPOS(IConfiguration config)
        {
            _SalesManager = new Sales(config.GetConnectionString("Home"));
        }

        public int ProcessSale(ABCSalePoco sale)
        {
            ABCSale newSale = new ABCSale();

            newSale.CustomerId = sale.CustomerId;
            newSale.SaleDate = DateTime.Now;
            newSale.SalePerson = sale.SalePerson;
            newSale.SaleItems = sale.SaleItems.Where(i => !string.IsNullOrEmpty(i.ItemCode)).ToList();
            
            foreach(var item in newSale.SaleItems)
            {
                if(string.IsNullOrEmpty(item.ItemCode))
                {
                    newSale.SubTotal = 0;
                    newSale.SubTotal += (item.UnitPrice * item.Quantity.Value);
                }                            
            }

            newSale.Gst = (newSale.SubTotal * (decimal)0.05);
            newSale.SaleTotal = (newSale.SubTotal + newSale.Gst);

            return _SalesManager.ProcessSale(newSale);

        }
    }
}
