using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAIS3150WebAppDemo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150WebAppDemo.Pages
{
    public class ViewProgramModel : PageModel
    {
        BCS RequestDirector;
        [BindProperty]
        public List<NAITProgram> ProgramList { get; set; }
        public void OnGet()
        {
            RequestDirector = new BCS();
            ProgramList = RequestDirector.FindPrograms();
        }
    }
}