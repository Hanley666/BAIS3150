using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpiershanley1BAIS3150CodeSample.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cpiershanley1BAIS3150CodeSample.Pages
{
    public class CreateProgramModel : PageModel
    {
        private readonly BCS _requestDirector;

        public CreateProgramModel(BCS requestDirector)
        {
            _requestDirector = requestDirector;
        }
        bool Success;
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public void OnPost(string programCode, string description)
        {
            Message = null;

            try
            {
                Success = _requestDirector.CreateProgram(programCode, description);
                if (Success == true)
                    TempData["Success"] = "Program Created Successfully";
            }
            catch(Exception ex)
            {
                TempData["Failed"] = ex.Message;
            }

       
        }
    }
}