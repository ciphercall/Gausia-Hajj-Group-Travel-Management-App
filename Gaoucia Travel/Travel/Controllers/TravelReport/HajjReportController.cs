using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers.TravelReport
{
    public class HajjReportController : AppController
    {
        //
        // GET: /HajjReport/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PageModel model)
        {

            TempData["HajjReport"] = model;
            return RedirectToAction("HajjReportLicenseWise");
        }


        public ActionResult HajjReportLicenseWise()
        {

            PageModel model = (PageModel)TempData["HajjReport"];
            return View(model);
        }
	}
}