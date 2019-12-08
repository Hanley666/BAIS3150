using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150WebAppDemo.Pages
{
    public class SignOnModel : PageModel
    {   [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {

        }
        public void OnPost()
        {
            Match match = Regex.Match(UserId, (@"/^[A-Za-z]{4}\d{4}$/"));

            if(UserId == null)
                ModelState.AddModelError("UserId", "User Id is Required.");

            if (UserId.Length < 8 || UserId.Length > 8)
                ModelState.AddModelError("UserId", "User Id must be 8 Characters Long.");

            if (!match.Success)
                ModelState.AddModelError("UserId", "User Id must contain 4 letters followed by 4 digits.");

            if (Password == null)
                ModelState.AddModelError("Password", "Password is Required.");

            if (Password.Length < 6)
                ModelState.AddModelError("Password", "Password Must Be at least 6 Characters Long.");
            
        }
    }
}