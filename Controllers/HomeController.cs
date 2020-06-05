using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilityEye.Controllers
{

    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }

        //Creating form from Json data
        [HttpPost]
        public ActionResult Form1(Models.InputModel im) 
        {
            ViewBag.input = im.Inputjson;
            string jsonvalue = im.Inputjson;

            dynamic dynJson = JsonConvert.DeserializeObject(jsonvalue);
            var htmlstring = "";
            foreach (var item in dynJson["form"])
            {
                //code for input types other than checkbox and radio buttons
                if (item.options == null)
                {
                    htmlstring += "<br/><label for=\"" + item.label + "\" >" + item.label + "</label><br/><input placeholder=\"" + "Enter Your Answer" + "\"  type=\"" + item.type + "\" id=\"" + item.label + "\" name=\"" + item.label + "\" /><br/>";
                }
                //code for input types checkbox and radio buttons 
                else
                {
                    htmlstring += "<br/> <br/><label for=\"" + item.label + "\" >" + item.label + "</label><br/>";
                    
                    foreach (var option in item.options)
                    {
                        htmlstring += "<label for=\"" + option + "\" >" + option + "</label><input type=\"" + item.type + "\" id=\"" + option + "\" value=\"" + option + "\" name=\"" + item.label + "\" />";
                    }
                }
            }
            ViewBag.HtmlStr = htmlstring;

            return View("form");
        }

        //Saving data submitted to dynamic form
        [HttpPost]
        public ActionResult Form2(FormCollection form)  
        {
            var Resulthtml = "";
            TempData["alertMessage"] = "<script>alert('Form Submitted Succesfully');</script>";

                    foreach (var key in form.AllKeys)
                    {
                        var value = form[key];
                        Resulthtml += "<br/><label>" + key + "&nbsp;&nbsp;" + value + "</label>";
                       
                    }
                            
                ViewBag.Resulthtml = Resulthtml;
                return View("result");
            

        }
    }
}




 