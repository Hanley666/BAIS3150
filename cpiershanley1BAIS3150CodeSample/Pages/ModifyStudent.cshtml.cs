using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpiershanley1BAIS3150CodeSample.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cpiershanley1BAIS3150CodeSample.Pages
{
    public class ModifyStudentModel : PageModel
    {
        private readonly BCS _requestDirector;

        public ModifyStudentModel(BCS requestDirector)
        {
            _requestDirector = requestDirector;
        }

        bool Success;
        public string Message { get; set; }
        public Student searchedStudent { get; set; }
        public Student ModifiedStudent { get; set; }
        [BindProperty]
        public string studentId { get; set; }
        [BindProperty]
        public string firstName { get; set; }
        [BindProperty]
        public string lastName { get; set; }
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string programCode { get; set; }
        [BindProperty]
        public List<NaitProgram> ProgramList { get; set; }

        public void OnGet()
        {

            ProgramList = _requestDirector.FindPrograms();
        }

        public void OnPostSearchStudent(string searchBox)
        {

            ProgramList = _requestDirector.FindPrograms();

            searchedStudent = new Student();
            searchedStudent = _requestDirector.FindStudent(searchBox);

            studentId = searchedStudent.StudentId;
            firstName = searchedStudent.FirstName;
            lastName = searchedStudent.LastName;
            email = searchedStudent.Email;
            programCode =  searchedStudent.ProgramCode;


            if (searchedStudent == null)
                Message = "Student Does Not Exist";
        }

        public void OnPostModifyStudent(string studentId, string firstName, string lastName, string email, string programList)
        {
            ModifiedStudent = new Student();

            ModifiedStudent.StudentId = studentId;
            ModifiedStudent.FirstName = firstName;
            ModifiedStudent.LastName = lastName;
            ModifiedStudent.Email = email;
            ModifiedStudent.ProgramCode = programList;

            Success = _requestDirector.ModifyStudent(ModifiedStudent);
            if (Success == true)
                Message = "Student Update Successful";
            else
                Message = "Student Update failed";
        }
    }
}