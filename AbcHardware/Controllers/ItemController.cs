using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcHardware.Domain;
using AbcHardware.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbcHardware.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: Item/Create
        public ActionResult AddItem()
        {
            return View();
        }
    
        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(Item item)
        {
            try
            {
                var errorsList = _itemService.CreateItem(item);

                if (errorsList == null)
                {
                    TempData["Success"] = "Item Create Successful";
                    return View();
                }
                foreach (var error in errorsList)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            catch
            {
                ModelState.AddModelError("CreateItem", "Create Item Failed");
            }
            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult UpdateItem(string searchString, string itemCode)
        {
            if (itemCode != null)
            {
                //ViewBag.Items = FilterItems(searchString);
                var item = GetItemByItemCode(itemCode);
                return View(item);
            }
            else
            {
                ViewBag.Items = FilterItems(searchString);
                return View();
            }
        }

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateItem(Item model)
        {
            try
            {
                var errorsList = _itemService.UpdateItem(model);
                if (errorsList == null)
                {
                    ViewBag.Items = _itemService.GetItems();
                    TempData["Success"] = "Item Update Successful";
                    return View();
                }
                foreach (var error in errorsList)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateItem", "Update Item Failed");
            }
            ViewBag.Items = _itemService.GetItems();
            return View(model);
        }

        // GET: Item/Delete/5
        public ActionResult DeleteItem(string searchString = null)
        {
            FilterDiscontinuedItems(searchString);

            return View();
        }

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItemPost(string itemCode)
        {
            try
            {
                var errorsList = _itemService.DeleteItem(itemCode);
                if (errorsList == null)
                {
                    FilterDiscontinuedItems();
                    TempData["Success"] = "Item Delete Successful";
                    return View("DeleteItem");
                }
                foreach (var error in errorsList)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeleteItem", "Delete Item Failed");
            }

            return View("DeleteItem");
        }

        private void FilterDiscontinuedItems(string searchString = null)
        {
            var items = FilterItems(searchString);
            items = items.Where(i => i.Discontinued == false).ToList();
            ViewBag.Items = items;
        }

        private Item GetItemByItemCode(string itemCode)
        {
            return _itemService.GetItemsByItemCode(itemCode);
        }

        private List<Item> FilterItems(string searchString)
        {
            var items = _itemService.GetItems();

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Description.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) ||
                                         i.ItemCode.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return items;
        }
    }
}