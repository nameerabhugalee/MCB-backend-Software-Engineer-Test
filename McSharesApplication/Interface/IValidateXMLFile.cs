using McSharesApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McSharesApplication.Interface
{
    public interface IValidateXMLFile
    {
        public bool ValidateXMLFile(UploadXMLFile obj);
    }
}
