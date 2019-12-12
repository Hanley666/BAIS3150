using System;
using System.Collections.Generic;
using System.Linq;
using AbcHardware.Domain;
using AbcHardware.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbcHardware.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customer/Create
        public ActionResult AddCustomer()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer(Customer customer)
        {
            try
            {
                var errorsList = _customerService.CreateCustomer(customer);
                if(errorsList == null)
                {
                    TempData["Success"] = "Customer Create Successful";
                    return Redirect(nameof(AddCustomer));
                }
                foreach(var error in errorsList)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }                              
            }
            catch
            {
                ModelState.AddModelError("CreateCustomer", "Create Customer Failed");
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult UpdateCustomer(string searchString, int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Customers = FilterCustomers(searchString);
                var customer = GetCustomerById(id.Value);
                return View(customer);
            }
            else
            {
                ViewBag.Customers = FilterCustomers(searchString);
                return View();
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCustomer(Customer model)
        {
            try
            {
                var errorsList = _customerService.UpdateCustomer(model);
                if(errorsList == null)
                {
                    ViewBag.Customers = _customerService.GetCustomers();
                    TempData["Success"] = "Customer Update Successful";
                    return View();
                }
                foreach (var error in errorsList)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateCustomer", "Update Customer Failed");
            }
            ViewBag.Customers = _customerService.GetCustomers();
            return View(model);
        }

        // GET: Customer/Delete/5
        public ActionResult DeleteCustomer(string searchString)
        {
            ViewBag.Customers = FilterCustomers(searchString);
            
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                var errorsList = _customerService.DeleteCustomer(id);
                if(errorsList == null)
                {
                    ViewBag.Customers = _customerService.GetCustomers();
                    TempData["Success"] = "Customer Delete Successful";
                    return View();
                }
                foreach (var error in errorsList)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }

            }
            catch
            {
                ModelState.AddModelError("DeleteCustomer", "Delete Customer Failed");
            }
            return View();
        }

        private Customer GetCustomerById(int customerId)
        {
            return _customerService.GetCustomerById(customerId);
        }

        private List<Customer> FilterCustomers(string searchString)
        {
            var customers = _customerService.GetCustomers();

            if(!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.FullName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return customers;
        }
    }
}