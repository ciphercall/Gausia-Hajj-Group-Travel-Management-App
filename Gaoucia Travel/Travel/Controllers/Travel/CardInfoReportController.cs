using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers
{
    public class CardInfoReportController : AppController
    {
        //
        // GET: /CardInfoReport/

        public ActionResult CardInfoYear()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult CardInfoYear(PageModel model)
        {
            

            TempData["CardInfoYear"] = model;
            return RedirectToAction("CardInfoYearReport");
        }
        public ActionResult CardInfoYearReport()
        {
            PageModel model = (PageModel)TempData["CardInfoYear"];
            return View(model);
        }



        public ActionResult CardInfoSerial()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult CardInfoSerial(PageModel model)
        {
           

            TempData["CardInfoSerial"] = model;
            return RedirectToAction("CardInfoSerialReport");
        }
        public ActionResult CardInfoSerialReport()
        {
            PageModel model = (PageModel)TempData["CardInfoSerial"];
            return View(model);
        }



        public ActionResult CardInfoCardType()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult CardInfoCardType(PageModel model)
        {
            TempData["CardInfoCardType"] = model;
            return RedirectToAction("CardInfoCardTypeReport");
        }
        public ActionResult CardInfoCardTypeReport()
        {
            PageModel model = (PageModel)TempData["CardInfoCardType"];
            return View(model);
        }

        public ActionResult CardInfo()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult CardInfo(PageModel model)
        {
            TempData["CardInfo"] = model;
            return RedirectToAction("CardInfoReport");
        }
        public ActionResult CardInfoReport()
        {
            PageModel model = (PageModel)TempData["CardInfo"];
            return View(model);
        }


    }
}
