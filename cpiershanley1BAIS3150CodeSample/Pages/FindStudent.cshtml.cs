using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpiershanley1BAIS3150CodeSample.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cpiershanley1BAIS3150CodeSample.Pages
{
    public class FindStudentModel : PageModel
    {
        private readonly BCS _requestDirector;

        public FindStudentModel(BCS requestDirector)
        {
            _requestDirector = requestDirector;
        }
        public string Message { get; set; }
        public Student searchedStudent { get; set; }
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

        public void OnGet()
        {

        }

        public void OnPost(string searchBox)
        {
            searchedStudent = new Student();
            searchedStudent = _requestDirector.FindStudent(searchBox);

            studentId = searchedStudent.StudentId;
            firstName = searchedStudent.FirstName;
            lastName = searchedStudent.LastName;
            email = searchedStudent.Email;
            programCode = searchedStudent.ProgramCode;


            if (searchedStudent == null)
                Message = "Student Does Not Exist";
        }
    }
}