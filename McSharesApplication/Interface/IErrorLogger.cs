using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Interface
{
    public interface IErrorLogger
    {
        public void logError(DateTime date, string details);
    }
}
