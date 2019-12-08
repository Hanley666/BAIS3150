using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAIS3150WebAppDemo.Domain
{
    public class Student
    {
        private string _studentId; //Camel Case beginning in Underscore
        private string _firstName;
        public string StudentId //Pascal Case
        {
            get
            {
                return _studentId;
            }
            set
            {
                _studentId = value;
            }
        }
        public string FirstName //Expression-Bodied Accessors
        {
            get => _firstName; //Implemntation of proerty can be made up of only a single statement
            set => _firstName = value;
        }
        public string LastName { get; set; } //Auto-Implemented Property, no logic in get/set
        public string Email { get; set; }
        public string ProgramCode { get; set; }
        public Student()
        {

        }
    }
}
