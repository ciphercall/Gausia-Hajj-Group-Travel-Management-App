using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;


namespace Travel.Controllers
{
    public class TicketController : AppController
    {
        TravelDbContext db = new TravelDbContext();
        // GET: /Ticket/

        public ActionResult Ticket_Purchase()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult Ticket_Purchase(PageModel model)
        {
            //var pdf = new PdfResult(aCnfJobModel, "GetJOBRegister_JobTypeReport");
            //return pdf;

            TempData["Ticket_Purchase"] = model;
            return RedirectToAction("Ticket_PurchaseReport");
        }
        public ActionResult Ticket_PurchaseReport()
        {
            PageModel model = (PageModel)TempData["Ticket_Purchase"];
            return View(model);
        }

        public ActionResult TicketWiseOp()
        {
            ViewData["Hightlight_valid_Travel_Report"] = "heighlight";
            return View();
        }

        [HttpPost]
        public ActionResult TicketWiseOp(PageModel model)
        {
            TempData["TicketWiseOp"] = model;
            return RedirectToAction("TicketWiseOpReport");
        }

        public ActionResult TicketWiseOpReport()
        {
            PageModel model = (PageModel) TempData["TicketWiseOp"];
            return View(model);
        }
    }
}
