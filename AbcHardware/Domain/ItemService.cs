using AbcHardware.Models;
using AbcHardware.TechnicalServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AbcHardware.Domain
{
    public class ItemService
    {
        private readonly ItemManager _itemManager;
        public ItemService(IConfiguration config)
        {
            _itemManager = new ItemManager(config.GetConnectionString("School"));
        }

        public List<Item> GetItems()
        {
            return _itemManager.GetItems();
        }

        public Item GetItemsByItemCode(string itemCode)
        {
            return _itemManager.GetItemByItemCode(itemCode);
        }

        public Dictionary<string,string> CreateItem(Item item)
        {
            var errorMessages = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(item.ItemCode))
            {
                Regex regex = new Regex(@"^([A-Z][0-9]{5})$");
                Match match = regex.Match(item.ItemCode.Trim());
                if (!match.Success)
                    errorMessages.Add("ItemCode", "Item Code must begin with a Capital Letter followed by 5 Numbers.");
            }
            else
                errorMessages.Add("ItemCode", "Item Code is Required.");

            if (item.UnitPrice != null)
            {
                Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$");
                Match match = regex.Match(item.UnitPrice.ToString());

                if (!match.Success)
                    errorMessages.Add("UnitPrice", "Item Unit Price must be in proper currency format 100,000.00 ");
            }
            else
                errorMessages.Add("UnitPrice", "Item Unit Price is Required.");
            
            if (item.UnitPrice < 0)
                errorMessages.Add("UnitPrice", "Item Unit Price must be a positive Value.");
            if (string.IsNullOrEmpty(item.Description))
                errorMessages.Add("Description", "Item Description is Required.");                       
            if (item.QuantityOnHand < 0)
                errorMessages.Add("QuantityOnHand", "Quantity must be a positive value.");
            if(item.QuantityOnHand == null)
                errorMessages.Add("QuantityOnHand", "Quantity is Required.");
           
            if (errorMessages.Any())
            {
                return errorMessages;
            }
            else
            {
                _itemManager.CreateItem(item);
                return null;
            }          
        }

        public Dictionary<string,string> DeleteItem(string itemCode)
        {
            var errorMessages = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(itemCode))
                errorMessages.Add("ItemCode", "ItemCode is Required");
            if(errorMessages.Any())
                return errorMessages;
            else
            {
                _itemManager.DeleteItem(itemCode);
                return null;
            }
            
        }

        public Dictionary<string,string> UpdateItem(Item item)
        {
            var errorMessages = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(item.ItemCode))
            {
                Regex regex = new Regex(@"^([A-Z][0-9]{5})$");
                Match match = regex.Match(item.ItemCode.Trim());
                if (!match.Success)
                    errorMessages.Add("ItemCode", "Item Code must begin with a Capital Letter followed by 5 Numbers.");
            }
            else
                errorMessages.Add("ItemCode", "Item Code is Required.");

            if (item.UnitPrice != null)
            {
                Regex regex = new Regex("^\\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$");
                Match match = regex.Match(item.UnitPrice.ToString());

                if (!match.Success)
                    errorMessages.Add("UnitPrice", "Item Unit Price must be in proper currency format $100,000.00 ");
            }
            else
                errorMessages.Add("UnitPrice", "Item Unit Price is Required.");

            if (item.UnitPrice < 0)
                errorMessages.Add("UnitPrice", "Item Unit Price must be a positive Value.");
            if (String.IsNullOrEmpty(item.Description))
                errorMessages.Add("Description", "Item Description is Required.");
            if (item.QuantityOnHand < 0)
                errorMessages.Add("QuantityOnHand", "Quantity must be a positive value.");
            if (String.IsNullOrEmpty(item.QuantityOnHand.ToString()))
                errorMessages.Add("QuantityOnHand", "Quantity is Required.");

            if (errorMessages.Any())
            {
                return errorMessages;
            }
            else
            {
                _itemManager.UpdateItem(item);
                return null;
            }
        }

        public int CheckInventoryAmount(string itemCode)
        {
            return _itemManager.CheckInventoryAmount(itemCode);
        }
    }
}
