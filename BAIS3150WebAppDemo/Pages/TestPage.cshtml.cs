using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150WebAppDemo.Pages
{
    public class TestPageModel : PageModel
    {
        [BindProperty]
        public string Field { get; set; }
        public string Message { get; set; }

        public void OnGet() //Initial
        {
            Message = "***OnGet***";
        }
        //public void OnPost()
        //{
        //    Message = "*** OnPost ***";
        //    Field = Request.Form["Field"];
        //}
        public void OnPostFirstAction()
        {
            Message = "*** OnPostFirstAction ***";
        }

        public void OnPostSecondAction()
        {
            Message = "*** OnPostSecondAction ***";
        }
    }
}