using McSharesApplication.Interface;
using McSharesApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Services
{
    public class UploadXMLFileService : IUploadXMLFile
    {
        private readonly CustomerDBContext _context;
        private readonly IErrorLogger _iErrorLogger;
        public UploadXMLFileService(CustomerDBContext dBContext, IErrorLogger iErrorLogger)
        {
            _context = dBContext;
            _iErrorLogger = iErrorLogger;
        }

        public List<Customer> getAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public CustomerData getByCustomerID(string customer_id)
        {
            var customer = _context.Customers.Find(customer_id);
            CustomerData data = new CustomerData();
            if (customer != null)
            {
                data.CustomerId = customer.CustomerId;
                data.CustomerName = customer.ContactName;
                data.CustomerType = customer.CustomerType;
                data.DateIncorp = customer.DateIncorp;
                data.NumShares = customer.NumShares;
                data.SharePrice = customer.SharePrice;
                data.Balance = customer.SharePrice * customer.NumShares;
            }
            else
            {
                //logging error
                _iErrorLogger.logError(DateTime.Now, "Customer does not exist!");
                return null;
            }
            return data;
        }
        public NewShares numberOfShares(string customer_id, int numShares)
        {
            NewShares newShareModel = new NewShares();

            var customer = _context.Customers.Find(customer_id);
            if (customer != null)
            {
                    customer.NumShares = numShares;
                    _context.SaveChanges();
                    newShareModel.updatedBalance = customer.NumShares * customer.SharePrice;
                 newShareModel.isSuccess = true;
                    return newShareModel;
            }
            newShareModel.isSuccess = false;
            return newShareModel;
        }

        public bool updateContactNumber(string customer_id, string contactNum)
        {
            var customer = _context.Customers.Find(customer_id);
            if (customer != null)
            {
                customer.ContactNumber = contactNum;
                _context.SaveChanges();
                return true;
            }
            _iErrorLogger.logError(DateTime.Now, "Cannot update Contact Number");
            return false;
        }
        public bool deleteCustomer(string customer_id)
        {
            var customer = _context.Customers.Find(customer_id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return true;
            }
            _iErrorLogger.logError(DateTime.Now, "Unable to delete customer");
            return false;
        }

        public IQueryable<Customer> filterByName(string searchTerm)
        {
            string[] parts = searchTerm.ToLower().Split();
            string p1 = parts.Length < 1 ? "" : parts[0];
            string p2 = parts.Length < 2 ? "" : parts[1];

            var result = _context.Customers.Where(n =>
                (n.ContactName.ToLower().Contains(p1)) || ((n.ContactName.ToLower().Contains(p2))));
            return result;
        }
    }
}
