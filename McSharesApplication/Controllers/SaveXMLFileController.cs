using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using McSharesApplication.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using McSharesApplication.Interface;
using Microsoft.AspNetCore.Http;

namespace McSharesApplication.Controllers
{
    public class SaveXMLFileController :Controller
    {
        private readonly ISaveXMLFile _ISaveXMLFile;

        public SaveXMLFileController(ISaveXMLFile iSaveXMLFile)
        {
            _ISaveXMLFile = iSaveXMLFile;
        }

        [HttpPost]
        [Route("saveFile")]
        public IActionResult save([FromForm] UploadXMLFile obj)
        {
            try
            {
                return _ISaveXMLFile.save(obj) ? Ok("XML data successfully saved to sql server") : Ok("Data duplication: Cannot save data to sql server");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}");
            }
        }

    }
}
