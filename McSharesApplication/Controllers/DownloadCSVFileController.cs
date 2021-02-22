using McSharesApplication.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McSharesApplication.Controllers
{
    public class DownloadCSVFileController : Controller
    {
        private readonly CustomerDBContext _context;
        public DownloadCSVFileController(CustomerDBContext dbcontext)
        {
            _context = dbcontext;
        }

        [HttpPost]
        [Route("generatecsvfile")]
        public IActionResult GenerateCSVString()
        {
            var customers = _context.Customers.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("ID");
            sb.Append(",");
            sb.Append("Name");
            sb.Append(",");
            sb.Append("Type");
            sb.Append(",");
            sb.Append("NumberOfShares");
            sb.Append(",");
            sb.Append("SharePrice");
            sb.AppendLine();
            foreach (var customer in customers)
            {
                sb.Append(customer.CustomerId);
                sb.Append(",");
                sb.Append(customer.ContactName);
                sb.Append(",");
                sb.Append(customer.CustomerType);
                sb.Append(",");
                sb.Append(customer.NumShares);
                sb.Append(",");
                sb.Append(customer.SharePrice);
                sb.AppendLine();
            }
            return sb;
        }
    }
}
