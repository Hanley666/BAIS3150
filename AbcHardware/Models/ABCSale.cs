﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Models
{
    public class ABCSale
    {
        public int CustomerId { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Gst { get; set; }

        public decimal? SaleTotal { get; set; }

        public string SalePerson { get; set; }

        public List<SaleItem> SaleItems { get; set; }

    }
}
