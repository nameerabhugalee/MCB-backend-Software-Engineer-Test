
using Microsoft.AspNetCore.Hosting;
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
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using McSharesApplication.Interface;

namespace McSharesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadXMLFileController : ControllerBase
    {
        private readonly IUploadXMLFile _IUpload;
        public UploadXMLFileController(IUploadXMLFile _iupload)
        {
            _IUpload = _iupload;
        }

        private static IWebHostEnvironment _webHostEnvironment;

        public UploadXMLFileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<string> Upload([FromForm] UploadXMLFile obj)
        {
            if (obj.files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath);
                    }
                    using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + obj.files.FileName))
                    {
                        obj.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return obj.files.FileName;

                    }
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
            else
            {
                return "Upload Failed!";
            }
        }

        [HttpGet]
        [Route("getAllCustomers")]
        public ActionResult<IEnumerable<string>> getAllCustomers()
        {
            try
            {
                var list = _IUpload.getAllCustomers();
                if (list.Any()) { return Ok(list); }
                else { return NotFound("No data"); }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}");
            }
        }

        [HttpGet]
        [Route("{customer_id}")]
        public ActionResult<CustomerData> getByCustomerID(string customer_id)
        {
            try
            {
                var customer = _IUpload.getByCustomerID(customer_id);
                if (customer != null)
                {
                    return Ok(customer);
                }
                else
                {
                    return NotFound("Customer not found!");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}");
            }
        }

        [HttpGet]
        [Route("FilterByName")]
        public ActionResult<IQueryable<Customer>> FilterByName(string searchTerm)
        {
            try
            {
                var customer = _IUpload.filterByName(searchTerm);
                if (customer != null) { 
                    return Ok(customer.ToList()); 
                }
                else { 
                    return NotFound("Not found."); 
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}");
            }
        }

        [HttpPut]
        [Route("numberOfShares")]
        public ActionResult Put(string custId, [FromBody] int numShares)
        {
            try
            {
                var returnModel = _IUpload.numberOfShares(custId, numShares);
                if (returnModel.isSuccess) { 
                    return Ok("New balance: " + returnModel.updatedBalance); 
                }
                else { 
                    return NotFound("Error!"); 
                }
            }
            catch (Exception e) { return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}"); }
        }
        [HttpPut]
        [Route("updateContactNumber")]
        public ActionResult Put(string custId, string contactNumber)
        {
            try
            {
                var isSuccess = _IUpload.updateContactNumber(custId, contactNumber);
                if (isSuccess) {
                    return Ok("Contact number is successfully updated"); 
                        }
                else { 
                    return NotFound("Error!"); 
                }
            }
            catch (Exception e) { return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}"); }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                var isSuccess = _IUpload.deleteCustomer(id);
                if (isSuccess) { 
                    return Ok(" Deleted!"); 
                }
                else { 
                    return NotFound("Error!"); 
                }
            }
            catch (Exception e) { return StatusCode(StatusCodes.Status500InternalServerError, $"{e.Message}"); }
        }
    }
}
