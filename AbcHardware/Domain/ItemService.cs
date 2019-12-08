using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.Domain
{
    public class ItemService
    {
        private readonly string _connectionString;
        public ItemService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Home");
        }
    }
}
