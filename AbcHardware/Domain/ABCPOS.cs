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
    }
}
