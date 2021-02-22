using McSharesApplication.Interface;
using McSharesApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Services
{
    public class ErrorLoggerService : IErrorLogger
    {
        private readonly CustomerDBContext _context;
        public ErrorLoggerService(CustomerDBContext dBContext)
        {
            _context = dBContext;
        }

        public void logError(DateTime date, string details)
        {
            try
            {
                ErrorLogger logger = new ErrorLogger();
                logger.time = date;
                logger.details = details;
                _context.logErrors.Add(logger);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}
