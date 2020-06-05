using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Microsoft.CSharp.RuntimeBinder;
using System.Web.Mvc;

namespace MobilityeyeTest
{
    [TestClass]
    public class UnitTest1
    {
        //Test for dynamic form Creation
        [TestMethod]
        public void Test_FormCreation()
        {
            string jsonvalue = "{\"form\":[{\"type\":\"text\",\"label\":\"What is your name?\"},{\"type\":\"radio\",\"label\":\"What is your gender?\",\"options\":[\"Male\",\"Female\"]},{\"type\":\"checkbox\",\"label\":\"Select all your hobbies\",\"options\":[\"Music\",\"Movies\",\"Sports\"]}]}";


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
                var ActualString = "<br/><label for=\"What is your name?\" >What is your name?</label><br/><input placeholder=\"Enter Your Answer\"  type=\"text\" id=\"What is your name?\" name=\"What is your name?\" /><br/><br/> <br/><label for=\"What is your gender?\" >What is your gender?</label><br/><label for=\"Male\" >Male</label><input type=\"radio\" id=\"Male\" value=\"Male\" name=\"What is your gender?\" /><label for=\"Female\" >Female</label><input type=\"radio\" id=\"Female\" value=\"Female\" name=\"What is your gender?\" /><br/> <br/><label for=\"Select all your hobbies\" >Select all your hobbies</label><br/><label for=\"Music\" >Music</label><input type=\"checkbox\" id=\"Music\" value=\"Music\" name=\"Select all your hobbies\" /><label for=\"Movies\" >Movies</label><input type=\"checkbox\" id=\"Movies\" value=\"Movies\" name=\"Select all your hobbies\" /><label for=\"Sports\" >Sports</label><input type=\"checkbox\" id=\"Sports\" value=\"Sports\" name=\"Select all your hobbies\" />";

            Assert.AreEqual(htmlstring, ActualString);

            }

        //Test for Submitted Data
        [TestMethod]
        public void Test_DataSubmission()
        {
            var Resulthtml = "";
            FormCollection form = new FormCollection();
            form.Add("What is your name?", "Test");
            form.Add("What is your gender?", "Male");
            form.Add("Select all your hobbies", "Movies");
            foreach (var key in form.AllKeys)
            {
                var value = form[key];
                Resulthtml += "<br/><label>" + key + "&nbsp;&nbsp;" + value + "</label>";

            }
            var ActualResult = "<br/><label>What is your name?&nbsp;&nbsp;Test</label><br/><label>What is your gender?&nbsp;&nbsp;Male</label><br/><label>Select all your hobbies&nbsp;&nbsp;Movies</label>";
            Assert.AreEqual(Resulthtml, ActualResult);
        }
    }
}
