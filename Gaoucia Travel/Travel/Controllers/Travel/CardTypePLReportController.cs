using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers
{
    public class CardTypePLReportController : AppController
    {
        
        // GET: /CardTypePLReport/

        public ActionResult CardTypePLSingle()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }
        [HttpPost]
        public ActionResult CardTypePLSingle(PageModel model)
        {
            //var pdf = new PdfResult(aCnfJobModel, "GetJOBRegister_JobTypeReport");
            //return pdf;

            TempData["CardTypeSingle"] = model;
            return RedirectToAction("CardTypeSingleReport");
        }
        public ActionResult CardTypeSingleReport()
        {
            PageModel model = (PageModel)TempData["CardTypeSingle"];
            return View(model);
        }


        public ActionResult CardTypePLGroup()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult CardTypePLGroup(PageModel model)
        {
            //var pdf = new PdfResult(aCnfJobModel, "GetJOBRegister_JobTypeReport");
            //return pdf;

            TempData["CardTypeGroup"] = model;
            return RedirectToAction("CardTypeGroupReport");
        }
        public ActionResult CardTypeGroupReport()
        {
            PageModel model = (PageModel)TempData["CardTypeGroup"];
            return View(model);
        }
    }
}
