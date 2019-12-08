using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAIS3150WebAppDemo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150WebAppDemo.Pages
{
    public class CreateProgramModel : PageModel
    {
        BCS RequestDirector;
        bool Success;

        //[BindProperty]
        //public string ProgramCode { get; set; }
        //[BindProperty]
        //public string Description { get; set; }
        public string Message { get; set; }
        
        public void OnGet()
        {
            
        }
        public void OnPost(string programCode, string description)
        {
            //var programCode = Request.Form["ProgramCode"];
            //var description = Request.Form["Description"];
            Message = null;

            RequestDirector = new BCS();
            Success = RequestDirector.CreateProgram(programCode, description);
            if(Success == true)
            {
                Message = "Program Created Successfully";
            }
            else
            {
                Message = "Program Creation Failed";

            }
        }
    }
}