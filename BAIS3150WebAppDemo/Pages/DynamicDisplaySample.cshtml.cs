using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using BAIS3150WebAppDemo.Domain;

namespace BAIS3150WebAppDemo.Pages
{
    public class DynamicDisplaySampleModel : PageModel
    {
        //private List<SampleClass> _sampleObjectCollection = new List<SampleClass>();
        //public List<SampleClass> SampleObjectCollection
        //{
        //    get
        //    {
        //        return _sampleObjectCollection;
        //    }
        //}

        //public DynamicDisplaySampleModel() //Constructor
        //{
        //    _sampleObjectCollection = new List<SampleClass>();
        //}
        public List<SampleClass> SampleObjectCollection { get; } = new List<SampleClass>();
        public void OnGet() //initial
        {
            SampleClass SampleObject;

            SampleObject = new SampleClass()
            {
                FirstProperty = "1",
                SecondProperty = "One",
            };
            SampleObjectCollection.Add(SampleObject);

            SampleObject = new SampleClass()
            {
                FirstProperty = "2",
                SecondProperty = "Two",
            };
            SampleObjectCollection.Add(SampleObject);

            SampleObject = new SampleClass()
            {
                FirstProperty = "3",
                SecondProperty = "Three",
            };
            SampleObjectCollection.Add(SampleObject);
        }
    }
}