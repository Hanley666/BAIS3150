using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cpiershanley1BAIS3150CodeSample.Domain
{
    public class NaitProgram
    {
        [Required]
        public string ProgramCode { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<Student> EnrolledStudent { get; set; }

        public NaitProgram()
        {

        }
    }
}
