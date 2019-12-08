using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAIS3150WebAppDemo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150WebAppDemo.Pages
{
    public class CreateStudentModel : PageModel
    {
        BCS RequestDirector;
        bool Success;
        public string Message { get; set; }
        [BindProperty]
        public List<NAITProgram> ProgramList { get; set; }
        public void OnGet()
        {
            RequestDirector = new BCS();
            ProgramList = RequestDirector.FindPrograms();
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

            RequestDirector = new BCS();
            Success = RequestDirector.EnrollStudent(NewStudent);
            if (Success == true)
                Message = "Student Created Successfully";
            else
                Message = "Student Create Failed";
        }
    }
}