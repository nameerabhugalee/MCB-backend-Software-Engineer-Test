using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Model
{
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }

        [ForeignKey("DocumentID")]
        public int requestDocumentId { get; set; }
        public string CustomerType { get; set; }
        public DateTime? DateIncorp { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? RegNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string TownCity { get; set; }
        public string Country { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public int NumShares { get; set; }
        public double SharePrice { get; set; }

    }
}
