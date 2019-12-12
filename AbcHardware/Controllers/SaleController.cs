using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcHardware.Domain;
using AbcHardware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbcHardware.Controllers
{
    public class SaleController : Controller
    {
        private readonly CustomerService _customersService;
        private readonly ItemService _itemsService;
        private readonly ABCPOS _salesService;

        public SaleController(CustomerService customersService, ItemService itemsService, ABCPOS salesService)
        {
            _customersService = customersService;
            _itemsService = itemsService;
            _salesService = salesService;
        }

        // GET: Sale/Create
        public ActionResult ProcessSale()
        {
            var customers = _customersService.GetCustomers();
            ViewBag.Customers = GetCustomers(customers);
            ViewBag.Items = GetAvailableItems();

            var model = new ABCSalePoco();
            model.SaleItems.Add(new SaleItem());

            return View(model);
        }

        // POST: Sale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSale(ABCSalePoco model)
        {
            try
            {
                ModelState.Clear();

                if (model.CustomerId == 0)
                    ModelState.AddModelError("CustomerId", "Customer is Required");
                if (model.SaleItems.Count() == 0)
                    ModelState.AddModelError("SaleItem", "Items must be added to sale");
                if (string.IsNullOrEmpty(model.SalePerson))
                    ModelState.AddModelError("SalesPerson", "Sales Person Name Required");
                
                foreach (var item in model.SaleItems)
                {
                    if (item.ItemCode != null)
                    {
                        if (_itemsService.CheckInventoryAmount(item.ItemCode) == 0 || _itemsService.CheckInventoryAmount(item.ItemCode) < item.Quantity)
                            ModelState.AddModelError("Quantity", "Quantity must be less than stock");
                        if (item.Quantity == 0 || item.Quantity < 0)
                            ModelState.AddModelError("Quantity", "Quantity must be a Positive Value");
                    }
                        
                }

                if (ModelState.ErrorCount == 0)
                {
                    var saleNumber = _salesService.ProcessSale(model);

                    if (saleNumber != 0)
                    {
                        TempData["Success"] = $"Sale {saleNumber} Create Successful";

                        model = new ABCSalePoco();
                        model.SaleItems.Add(new SaleItem());
                    }
                    
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("CreateSale", "Create Item Failed");
            }

            var customers = _customersService.GetCustomers();
            ViewBag.Customers = GetCustomers(customers);
            ViewBag.Items = GetAvailableItems();

            return View(model);
        }

        private SelectList GetCustomers(List<Customer> customers)
        {
            SelectList customerSelectList = new SelectList(customers, "CustomerId", "FullName");
            return customerSelectList;
        }
        private List<Item> GetAvailableItems()
        {
            return _itemsService.GetItems()
                .Where(i => !i.Discontinued)
                .ToList();
        }
    }
}