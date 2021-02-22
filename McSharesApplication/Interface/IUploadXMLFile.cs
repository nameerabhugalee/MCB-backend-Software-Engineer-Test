using McSharesApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Interface
{
    public interface IUploadXMLFile
    {
        public List<Customer> getAllCustomers();
        public CustomerData getByCustomerID(string customer_id);
        public NewShares numberOfShares(string customer_id, int numShares);
        public bool updateContactNumber(string customer_id, string contactNum);
        public bool deleteCustomer(string customer_id);
        public IQueryable<Customer> filterByName(string searchTerm);
    }
}
