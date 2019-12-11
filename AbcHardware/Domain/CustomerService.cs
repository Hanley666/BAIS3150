using AbcHardware.Models;
using AbcHardware.TechnicalServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbcHardware.Domain
{
    public class CustomerService
    {
        private readonly CustomerManager _customerManager;

        public CustomerService(IConfiguration config)
        {
            _customerManager = new CustomerManager(config.GetConnectionString("Home"));
        }

        public List<Customer> GetCustomers()
        {
            return _customerManager.GetCustomers();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customerManager.GetCustomerById(customerId);
        }

        public Dictionary<string, string> CreateCustomer(Customer customer)
        {
            var errorMessages = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(customer.PostalCode))
            {
                Regex regex = new Regex(@"^(?![DFIOQU])(([ABCEGHJ-NPRSTVXY]\d[A-Z]) ?(\d[A-Z]\d))$");
                Match match = regex.Match(customer.PostalCode);

                if (match.Success)
                    customer.PostalCode = $"{match.Groups[2].Value} {match.Groups[3].Value}";
                else
                   errorMessages.Add("PostalCode", "Postal Code must be a valid Canadian Postal Code in the format A1A 1A1");
            }
            else
                errorMessages.Add("PostalCode", "Postal Code Is Required"); 

            if (String.IsNullOrEmpty(customer.FirstName))
                errorMessages.Add("FirstName", "First Name Is Required");
            if (String.IsNullOrEmpty(customer.LastName))
                errorMessages.Add("LastName", "Last Name Is Required");
            if (String.IsNullOrEmpty(customer.Address))
                errorMessages.Add("Address", "Address Is Required");
            if (String.IsNullOrEmpty(customer.City))
                errorMessages.Add("City", "City Is Required");
            if (String.IsNullOrEmpty(customer.Province))
                errorMessages.Add("Province", "Province Is Required");


            if (errorMessages.Any())
                return errorMessages;
            else
            {
                _customerManager.CreateCustomer(customer);
                return null;
            }                      
        }

        public Dictionary<string,string> DeleteCustomer(int customerId)
        {
            var errorMessage = new Dictionary<string, string>();

            if (customerId == 0)
                errorMessage.Add("Customer Id", "Customer Id is Required");
            if(_customerManager.GetSaleNumberByCustomerId(customerId) == true)
                errorMessage.Add("Sale Exists", "Customer is attached to a Sale and cannot be Deleted ");

            if (errorMessage.Any())
                return errorMessage;
            else
            {
                _customerManager.DeleteCustomer(customerId);
                return null;
            }         
        }

        public Dictionary<string,string> UpdateCustomer(Customer customer)
        {            
            var errorMessages = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(customer.PostalCode))
            {
                Regex regex = new Regex(@"^(?![DFIOQU])(([ABCEGHJ-NPRSTVXY]\d[A-Z]) ?(\d[A-Z]\d))$");
                Match match = regex.Match(customer.PostalCode);

                if (match.Success)
                    customer.PostalCode = $"{match.Groups[2].Value} {match.Groups[3].Value}";
                else
                    errorMessages.Add("PostalCode", "Postal Code must be a valid Canadian Postal Code in the format A1A 1A1");
            }
            else
                errorMessages.Add("PostalCode", "Postal Code Is Required");

            if (String.IsNullOrEmpty(customer.FirstName))
                errorMessages.Add("FirstName","Customer First Name is Required");
            if (String.IsNullOrEmpty(customer.LastName))
                errorMessages.Add("LastName","Customer Last Name is Required");
            if (String.IsNullOrEmpty(customer.Address))
                errorMessages.Add("Address","Customer Address is Required");
            if (String.IsNullOrEmpty(customer.City))
                errorMessages.Add("City","City is Required");
            if (String.IsNullOrEmpty(customer.Province))
                errorMessages.Add("Province","Province is Required");
            

            if (errorMessages.Any())
                return errorMessages;
            else
            {
                _customerManager.UpdateCustomer(customer);
                return null;
            }           
        }

        
    }
}
