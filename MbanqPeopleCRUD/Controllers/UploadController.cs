using System.Web.Mvc;
using System.IO;
using System.Web;
using MbanqPeopleCRUD.Models;
using System.Collections.Generic;
using MbanqPeopleCRUD.DAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;

namespace FileUpload.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload  
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                // only XML files will be accepted
                if (!file.FileName.EndsWith(".xml"))
                    throw new Exception("Only XML files are accepted");

                if (file.ContentLength > 0)
                {
                    // file upload
                    string _FileName = Path.GetFileName(file.FileName);

                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);

                    // people bulk insert
                    DataSet ds = new DataSet();
                    //ds.ReadXml(Server.MapPath(_path)); // doesn't work
                    ds.ReadXml(Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName));

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MbanqContext"].ConnectionString))
                    {
                        DataTable dt = ds.Tables["person"];

                        conn.Open();

                        using (SqlBulkCopy copy = new SqlBulkCopy(conn, SqlBulkCopyOptions.CheckConstraints, null))
                        {
                            copy.BatchSize = 5000;
                            copy.BulkCopyTimeout = 3000;
                            copy.DestinationTableName = "dbo.Person";
                            //copy.ColumnMappings.Add("id", "id");
                            copy.ColumnMappings.Add("TIN", "TIN");
                            copy.ColumnMappings.Add("Name", "Name");
                            copy.ColumnMappings.Add("Surname", "Surname");
                            copy.ColumnMappings.Add("Place", "Place");
                            copy.ColumnMappings.Add("Address", "Address");
                            copy.ColumnMappings.Add("Phone", "Phone");
                            copy.ColumnMappings.Add("Email", "Email");
                            copy.WriteToServer(dt);
                        }
                        conn.Close();
                    }
                }

                ViewBag.Message = "File Uploaded Successfully";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "File upload failed";
                return View();
            }
        }
    }
}