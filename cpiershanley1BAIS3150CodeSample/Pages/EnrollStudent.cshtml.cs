using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpiershanley1BAIS3150CodeSample.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cpiershanley1BAIS3150CodeSample.Pages
{
    public class EnrollStudentModel : PageModel
    {
        private readonly BCS _requestDirector;

        public EnrollStudentModel(BCS requestDirector)
        {
            _requestDirector = requestDirector;
        }
        bool Success;
        public string Message { get; set; }
        [BindProperty]
        public List<NaitProgram> ProgramList { get; set; }
        public void OnGet()
        {
            
            ProgramList = _requestDirector.FindPrograms();
        }
        public void OnPost(string studentId, string firstName, string lastName, string email, string programList)
        {
            Student NewStudent = new Student()
            {
                StudentId = studentId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                ProgramCode = programList
            };
        
            Success = _requestDirector.EnrollStudent(NewStudent);
            if (Success == true)
                Message = "Student Created Successfully";
            else
                Message = "Student Create Failed";
        }
    }
}