using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cpiershanley1BAIS3150CodeSample.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace cpiershanley1BAIS3150CodeSample.Pages
{
    public class GetDatabaseUserModel : PageModel
    {
        private readonly BCS _requestDirector;

        public GetDatabaseUserModel(BCS requestDirector)
        {
            _requestDirector = requestDirector;
        }
        public DatabaseUser CurrentDatabaseUser { get; set; }
        public void OnGet()
        {
            CurrentDatabaseUser = _requestDirector.GetDatabaseUser();
        }
    }
}