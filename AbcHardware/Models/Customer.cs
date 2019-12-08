using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AbcHardware.Models
{
    public class Customer
    {
        private string _postalCode;
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = FormatPostalCode(value);
            }
        }


        public string FullName
        {
            get { return FirstName + "" + LastName; }
        }

        private string FormatPostalCode(string postalCode)
        {
            Regex regex = new Regex(@"^(?![DFIOQU])(([ABCEGHJ-NPRSTVXY]\d[A-Z]) ?(\d[A-Z]\d))$");
            Match match = regex.Match(postalCode);

            if (match.Success)
                return $"{match.Groups[2].Value} {match.Groups[3].Value}";

            return null;
        }
    }
}
