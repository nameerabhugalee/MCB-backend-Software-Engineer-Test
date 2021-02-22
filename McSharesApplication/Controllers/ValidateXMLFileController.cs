using McSharesApplication.Interface;
using McSharesApplication.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace McSharesApplication.Controllers
{
    public class ValidateXMLFileController :ControllerBase
    {
        private readonly IHostingEnvironment _IHostingEnvironment;
        private readonly IValidateXMLFile _IValidateXMLFile;

        public ValidateXMLFileController(IHostingEnvironment environment, IValidateXMLFile iValidateXMLFile)
        {
            _IHostingEnvironment = environment;
            _IValidateXMLFile = iValidateXMLFile;
        }

        [HttpPost]
        //[Authorize] uncomment for authentication
        [Route("ValidateXmlFile")]
        public IActionResult ValidateXmlFile([FromForm] UploadXMLFile obj)
        {
            return _IValidateXMLFile.ValidateXMLFile(obj) ? Ok("XML document validation failed!") : Ok("XML document has been validated successfully!");
        }
    }
}
