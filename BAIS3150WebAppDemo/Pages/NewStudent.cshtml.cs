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
    public class NewStudentModel : PageModel
    {
        [BindProperty, Required]
        public string FirstName { get; set; }
        [BindProperty,Required]
        public string LastName { get; set; }
        [BindProperty,Required]
        public string Email { get; set; }
        [BindProperty, Required, MinLength(8), MaxLength(8)]
        public string UserId { get; set; }
        public string ProgramDropDownList { get; set; }
        [BindProperty,Required, MinLength(6)]
        public string Password { get; set; }
        [BindProperty,Required]
        public string PasswordConfirm { get; set; }
        public void OnGet()
        {

        }
        public void OnPost()
        {
            Match EmailMatch = Regex.Match(Email ?? "", $"^w +@[a - zA - Z_] +?.[a-zA-Z]{2,3}$");
            if(!EmailMatch.Success)
                ModelState.AddModelError("Email", "Email Must be Email Format.");

            Match UserIdMatch = Regex.Match(UserId ?? "", (@"/^[A-Za-z]{4}\d{4}$/"));
            if(!UserIdMatch.Success)
                ModelState.AddModelError("UserId", "User Id must contain 4 letters followed by 4 digits.");
            
            if(Password != PasswordConfirm)
                ModelState.AddModelError("PasswordConfirm", "Confirm Password Must Match Password");

            if(ProgramDropDownList != "BAIST" || ProgramDropDownList != "BUS" || ProgramDropDownList != "PHT")
                ModelState.AddModelError("ProgramDropDownList", "Program Must be Selected");
        }
    }
}