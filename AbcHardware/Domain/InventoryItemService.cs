using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Domain
{
    public class InventoryItemService
    {
        private readonly string _connectionString;
        public InventoryItemService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Home");
        }
    }
}
