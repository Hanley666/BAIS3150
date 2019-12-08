using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.TechnicalServices
{
    public class Sales
    {
        private readonly string _connectionString;

        public Sales(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
