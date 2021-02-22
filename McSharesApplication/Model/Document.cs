using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Model
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentReference { get; set; }

    }
}
