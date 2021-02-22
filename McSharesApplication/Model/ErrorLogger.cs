using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Model
{
    public class ErrorLogger
    {
        public int errorID { get; set; }
        public DateTime time { get; set; }
        public string details { get; set; }
    }
}
