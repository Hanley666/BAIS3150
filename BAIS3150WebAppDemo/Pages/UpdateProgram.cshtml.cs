using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAIS3150WebAppDemo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150WebAppDemo.Pages
{
    public class UpdateProgramModel : PageModel
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
        public void OnPost(string programCode, string description)
        {
            NAITProgram UpdatedProgram = new NAITProgram()
            {
                ProgramCode = programCode,
                Description = description
            };

            RequestDirector = new BCS();
            Success = RequestDirector.UpdateProgramDescription(UpdatedProgram);
            if (Success == true)
            {
                Message = "Program Description Updated Successfully";
                ProgramCode = null;
                Description = null;
            }    
            else
                Message = "Prgram Update Failed";
        }
    }
}