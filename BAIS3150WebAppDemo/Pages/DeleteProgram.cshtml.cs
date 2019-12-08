using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAIS3150WebAppDemo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150WebAppDemo.Pages
{
    public class DeleteProgramModel : PageModel
    {
        BCS RequestDirector;
        bool Success;
        [BindProperty]
        public string ProgramCode { get; set; }
        [BindProperty]
        public string Description { get; set; }
        public string Message { get; set; }
        public void OnGet(string programCode)
        {
            RequestDirector = new BCS();
            NAITProgram SelectedProgram = new NAITProgram()
            {
                ProgramCode = programCode,
                Description = RequestDirector.FindProgram(programCode).Description
            };

            ProgramCode = SelectedProgram.ProgramCode;
            Description = SelectedProgram.Description;
        }
        public void OnPost()
        {
            NAITProgram ActiveStudents = new NAITProgram();
            RequestDirector = new BCS();
            ActiveStudents.EnrolledStudent = RequestDirector.FindStudentsByProgram(ProgramCode);
            if (ActiveStudents.EnrolledStudent.Count < 1)
            {
                Success = RequestDirector.DeleteProgram(ProgramCode);
                if (Success == true)
                {
                    Message = "Program " + ProgramCode + " Deleted Successfully";
                    ProgramCode = null;
                    Description = null;
                }
                else
                    Message = "Program Delete Failed";
            }
            else
                Message = "There are Currently Students in " + ProgramCode + " Unable to Delete.";
        }
    }
}