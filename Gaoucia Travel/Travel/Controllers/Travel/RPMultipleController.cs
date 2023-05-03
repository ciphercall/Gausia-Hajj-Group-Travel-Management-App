using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers.Travel
{
    public class RPMultipleController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public RPMultipleController()
        {
            ViewData["Hightlight_valid_Travel_Form"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }
        // GET: /RPMultiple/
        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getMonth(DateTime changedtxt)//with Trans No Generation
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(9, 2);
            string getMonth = converttoString.Substring(3, 3);
            string Month = getMonth + "-" + getYear;



            string converttoString1 = Convert.ToString(changedtxt.ToString("dd-MM-yyyy"));
            string getyear = converttoString1.Substring(8, 2);
            string getmonth = converttoString1.Substring(3, 2);
            string halftransno = getyear + getmonth;

            var query = from n in db.RPDRCRDbSet where n.COMPID == comid select new { n.TRANSNO };

            Int64 maxvalue = 0, Trans = 0;


            List<SelectListItem> Transno = new List<SelectListItem>();

            foreach (var x in query)
            {

                string temp = Convert.ToString(x.TRANSNO);
                string substring = temp.Substring(0, 4);
                if (substring == halftransno)
                {
                    Transno.Add(new SelectListItem { Text = x.TRANSNO.ToString(), Value = x.TRANSNO.ToString() });

                }

            }
            string transno = "";
            if (Transno.Count == 0)
            {

                transno = halftransno + "000001";
            }
            else
            {
                maxvalue = Transno.Max(t => Convert.ToInt64(t.Text));
                Int64 temp = maxvalue + 1;
                transno = Convert.ToString(temp);

            }

            var result = new { month = Month, TransNo = transno };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Update()
        {
            return View();
        }


	}
}