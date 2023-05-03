using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Travel.Models;
using Travel.Models.ASL;

namespace Travel.Controllers.ASL
{
    public class Asl_BranchController : AppController
    {
        public ActionResult Create()
        {


            //var dt = (Asl_Branch)TempData["Branch"];

           

            return View();
        }

        //[AcceptVerbs("POST")]
        //[ActionName("Create")]
        //public ActionResult CreatePost(Asl_Branch model)
        //{
        //    AslUserco aslUserco = new AslUserco();

        //    aslUserco.COMPID = model.COMPID;
        //    aslUserco.BRANCHID = model.BRANCHID;
           
        //    aslUserco.ADDRESS = model.ADDRESS;
        //    aslUserco.MOBNO = model.CONTACTNO;
        //    aslUserco.EMAILID = model.EMAILID;
        //    aslUserco.LOGINPW = "asl" + "123%";
        //    TempData["User"] = aslUserco;

        //    return RedirectToAction("Create", "AslUserCO", TempData["User"]);
        //}





    }
}