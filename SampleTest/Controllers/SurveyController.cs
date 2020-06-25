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

        public ActionResult DoSurvey()
        {
            return View();
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

                    string id=surveyData.AddSurvey(surveyDetails);

                    return Json(id);

                }
                catch (Exception ex)
                {
                   return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("Data not entered.");             
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