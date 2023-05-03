using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers.TravelReport
{
    public class TicketReportController : AppController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PageModel model)
        {

            TempData["TicketReport"] = model;
            return RedirectToAction("TicketReportCarrier");
        }


        public ActionResult TicketReportCarrier()
        {

            PageModel model = (PageModel)TempData["TicketReport"];
            return View(model);
        }
	}
}