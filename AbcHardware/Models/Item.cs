﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Models
{
    public class Item
    {
        public string ItemCode { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal? UnitPrice { get; set; }

        public int? QuantityOnHand { get; set; }

        public bool Discontinued { get; set; }
    }
}
