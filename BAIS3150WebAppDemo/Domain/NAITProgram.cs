using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAIS3150WebAppDemo.Domain
{
    public class NAITProgram
    {
        public string ProgramCode { get; set; }
        public string Description { get; set; }
        public List<Student> EnrolledStudent { get; set; }
        public NAITProgram()
        {


        }
    }
}
