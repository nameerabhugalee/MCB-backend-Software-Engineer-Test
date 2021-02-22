using McSharesApplication.Interface;
using McSharesApplication.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace McSharesApplication.Services
{
    public class SaveXMLFileService : ISaveXMLFile
    {
        private readonly CustomerDBContext _context;
        private readonly IValidateXMLFile _iValidate;
        private readonly IErrorLogger _ierrorlogger;
        public SaveXMLFileService(CustomerDBContext dBContext, IErrorLogger ierrorlogger,  IValidateXMLFile iValidate)
        {
            _context = dBContext;
            _iValidate = iValidate;
            _ierrorlogger = ierrorlogger;
        }

        public bool save(UploadXMLFile obj)
        {
            Boolean isNotValid = _iValidate.ValidateXMLFile(obj);

            if (!isNotValid)
            {
                //Load xml file
                XDocument xmlDoc = XDocument.Load(obj.files.OpenReadStream());
                var xmlDocument = xmlDoc.ToXmlDocument();

                Document document = new Document();

                document.DocumentReference = xmlDocument.GetElementsByTagName("Doc_Ref")[0].InnerText;
                document.DocumentDate = Convert.ToDateTime(xmlDocument.GetElementsByTagName("Doc_Date")[0].InnerText);

                //if refDoc exists
                var isExist = checkIfExistDocRef(document.DocumentReference);

                if (!isExist)
                {
                    _context.Documents.Add(document);

                    for (int i = 0; i < xmlDocument.GetElementsByTagName("DataItem_Customer").Count; i++)
                    {
                        Customer customer = new Customer();

                        customer.CustomerId = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[0].InnerXml;
                        customer.CustomerType = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[1].InnerXml;

                        if (xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[2].InnerXml != "")
                        {
                            customer.DateOfBirth = Convert.ToDateTime(xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[2].InnerXml);
                        }

                        if (xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[3].InnerXml != "")
                        {
                            customer.DateIncorp = Convert.ToDateTime(xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[3].InnerXml);
                        }

                        if (xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[4].InnerXml != "")
                        {
                            customer.RegNumber = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[4].InnerXml;
                        }

                        customer.AddressLine1 = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[5].ChildNodes[0].InnerText;
                        customer.AddressLine2 = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[5].ChildNodes[1].InnerText;
                        customer.TownCity = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[5].ChildNodes[2].InnerText;
                        customer.Country = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[5].ChildNodes[3].InnerText;

                        customer.ContactName = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[6].ChildNodes[0].InnerText;

                        if (xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[6].ChildNodes[1].InnerText != "")
                        {
                            customer.ContactName = xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[6].ChildNodes[1].InnerText;
                        }

                        customer.NumShares = Int32.Parse(xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[7].ChildNodes[0].InnerText);
                        customer.SharePrice = Double.Parse(xmlDocument.GetElementsByTagName("DataItem_Customer")[i].ChildNodes[7].ChildNodes[1].InnerText);

                        customer.requestDocumentId = document.DocumentId;

                        _context.Customers.Add(customer);
                        _context.SaveChanges();

                    }
                    return true;
                }

                return false;
            }


            else
            {
                return false;

            }

            bool checkIfExistDocRef(string docRef)
            {
                var isExist = _context.Documents.Where(u => u.DocumentReference == docRef).ToList();
                return isExist.Any() ? true : false;
            }


        }
    }
    }
