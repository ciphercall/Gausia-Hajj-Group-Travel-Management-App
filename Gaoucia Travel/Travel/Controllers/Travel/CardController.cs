using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Travel.Models;

namespace Travel.Controllers
{
    public class CardController : AppController
    {
        //
        // GET: /Card/

        public ActionResult CardWiseTS()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult CardWiseTS(PageModel model)
        {
            TempData["CardWiseTS"] = model;
            return RedirectToAction("CardWiseTSReport");
        }

        public ActionResult CardWiseTSReport()
        {
            PageModel model = (PageModel) TempData["CardWiseTS"];
            return View(model);
        }
        public ActionResult Op_PL_Statement()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult Op_PL_Statement(PageModel model)
        {
            TempData["Op_PL_Statement"] = model;
            return RedirectToAction("Op_PL_StatementReport");
        }

        public ActionResult Op_PL_StatementReport()
        {
            PageModel model = (PageModel) TempData["Op_PL_Statement"];
            return View(model);
        }


        public ActionResult Op_PL_StatementAll()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }


        [HttpPost]
        public ActionResult Op_PL_StatementAll(PageModel model)
        {
            TempData["Op_PL_StatementAll"] = model;
            return RedirectToAction("Op_PL_StatementAllReport");
        }

        public ActionResult Op_PL_StatementAllReport()
        {
            PageModel model = (PageModel)TempData["Op_PL_StatementAll"];
            return View(model);
        }
    }
}
