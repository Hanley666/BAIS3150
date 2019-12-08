using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpiershanley1BAIS3150CodeSample.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cpiershanley1BAIS3150CodeSample.Pages
{
    public class FindProgramModel : PageModel
    {
        private readonly BCS _requestDirector;

        public FindProgramModel(BCS requestDirector)
        {
            _requestDirector = requestDirector;
        }

        NaitProgram searchedProgram;
        [BindProperty]
        public string SearchBox { get; set; }
        [BindProperty]
        public string ProgramCode { get; set; }
        [BindProperty]
        public string Description { get; set; }
        public string Message { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();

        public void OnGet()
        {

        }

        public void OnPost()
        {
            searchedProgram = _requestDirector.FindProgram(SearchBox);

            ProgramCode = searchedProgram.ProgramCode;
            Description = searchedProgram.Description;
            EnrolledStudents = searchedProgram.EnrolledStudent;

            if (searchedProgram == null)
                Message = "Program Does Not Exist";
        }
    }
}