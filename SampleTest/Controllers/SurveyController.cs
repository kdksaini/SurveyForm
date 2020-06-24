using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using System.IO;

namespace SampleTest.Controllers
{
    public class SurveyController : Controller
    {
        [HttpGet]
        [ActionName("DoSurvey")]
        public ActionResult DoSurvey_Get()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles2()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public ActionResult UploadData()
        {
            
            if (Request.Files.Count > 0)
             {
               try
                {
                    
                        SurveyData surveyData = new SurveyData();
                        SurveyDetails surveyDetails = new SurveyDetails();
 
                        HttpPostedFileBase file = Request.Files[0];

                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
 
                        var fname2 = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname2);

                    surveyDetails.Name = Request.Form.Get("Name");
                    surveyDetails.Age = Convert.ToInt16(Request.Form.Get("Age"));
                    surveyDetails.Gender = Request.Form.Get("Gender");
                    surveyDetails.Email = Request.Form.Get("Email");
                    surveyDetails.City = Request.Form.Get("City");
                    surveyDetails.Resume = fname;
                    surveyDetails.Education = Request.Form.Get("Education");
                    ///return this.Json(fname);
                    ///
                    surveyData.AddSurvey(surveyDetails);

                    return Json("Data Saved Successfully!");
                    //return RedirectToAction("FormSubmission");
                }
                catch (Exception ex)
                {
                   return Json("Error occurred. Error details: " + ex.Message);
                   // return "Error occurred. Error details: " + ex.Message;
                }
            }
            else
            {
                return Json("Data not entered.");
                //return "No files selected.";
            }
        }


        public ActionResult SurveyDetail(int id)
        {
            SurveyData surveyData = new SurveyData();
            SurveyDetails details = surveyData.Details.Single(survey => survey.ID == id);
            return View(details);
        }
    }
}