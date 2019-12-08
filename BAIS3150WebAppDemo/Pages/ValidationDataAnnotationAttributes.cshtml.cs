using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace BAIS3150WebAppDemo.Pages
{
    public class ValidationDataAnnotationAttributesModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        [Required, MaxLength(5), MinLength(1)]
        public string Field { get; set; }
        public void OnGet()
        {
            Message = "*** OnGet ***";
        }
        public void OnPost()
        {
            if (ModelState.IsValid)
                Message = "*** OnPost *** - Valid";
            else
                Message = "*** OnPost *** - Invalid";
        }

    }
}