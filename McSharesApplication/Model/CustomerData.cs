using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Model
{
    public class CustomerData
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateIncorp { get; set; }
        public string CustomerType { get; set; }
        public int NumShares { get; set; }
        public double SharePrice { get; set; }
        public double Balance { get; set; }
    }
}
