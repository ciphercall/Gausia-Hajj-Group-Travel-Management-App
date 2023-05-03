using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers.TravelReport
{
    public class PassengerlistController : AppController
    {
        //
        // GET: /Passengerlist/
        public ActionResult Index()
        {
            return View();
        }

         [HttpPost]
        public ActionResult Index(PageModel model)
        {

            TempData["Passengerlistlicensewise"] = model;
            return RedirectToAction("LicensewisePassenger");
        }


         public ActionResult LicensewisePassenger()
         {

             PageModel model = (PageModel)TempData["Passengerlistlicensewise"];
             return View(model);
         }


        public ActionResult PassengerListAgent()
         {
             return View();
         }

          [HttpPost]
        public ActionResult PassengerListAgent(PageModel model)
        {
            TempData["PassengerListAgentWise"] = model;
            return RedirectToAction("AgentWisePassenger");
        }

          public ActionResult AgentWisePassenger()
          {

              PageModel model = (PageModel)TempData["PassengerListAgentWise"];
              return View(model);
          }
	}
}