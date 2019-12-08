using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BAIS3150WebAppDemo.Domain;
using BAIS3150WebAppDemo.Technical_Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;

namespace BAIS3150WebAppDemo.Pages
{
    public class DemonstrationCategoriesModel : PageModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Category> CategoryList { get; set; }
        public void OnGet()
        {
            CategoriesController CategoryManager = new CategoriesController();
            CategoryList = CategoryManager.GetNorthwindCategories();

           


        }

       
    }
}