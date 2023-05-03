using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Travel.DataAccess;
using Travel.Models.DTO;

namespace Travel.Controllers
{
    public class DashBoardController : AppController
    {
        //
        // GET: /ShowJob/
        TimeZoneInfo timeZoneInfo;
        TravelDbContext db = new TravelDbContext();

        public HttpCookie userCookie;
        public Int64 loggedcompid;
        public DashBoardController()
        {
            

            if (System.Web.HttpContext.Current.Session["loggedCompID"] != null)
            {
                loggedcompid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            }
            else
            {
                Index();
            }

            ViewData["HighLight_Menu_DashBoard"] = "High Light DashBoard";
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Today()
        {

            return View();
        }



        public ActionResult LastOneDay()
        {
            return View();
        }
        public ActionResult Last7Day()
        {
            return View();
        }
        public ActionResult LastOneMonth()
        {
            return View();
        }

        public ActionResult LastOneYear()
        {
            return View();
        }




        public JsonResult Topcategories(string d)
        {
            Int64 n = Convert.ToInt64(d);
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string frdate = dt.ToString("yyyy-MM-dd");
            string todate = "";

            if (n == 364)
            {
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                todate = firstDay.ToString("yyyy-MM-dd");
            }
            else
            {
                DateTime dt2 = TimeZoneInfo.ConvertTime(DateTime.Today.AddDays(-n), timeZoneInfo);
                todate = dt2.ToString("yyyy-MM-dd");
            }



            mydataservice objMD = new mydataservice();
            var chartsdata = objMD.TopcategoriesListdata(loggedcompid, todate, frdate); // calling method Listdata
            return Json(chartsdata, JsonRequestBehavior.AllowGet); // returning list from here.
        }



        public JsonResult TopItemsByQty(string d)
        {
            Int64 n = Convert.ToInt64(d);
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string frdate = dt.ToString("yyyy-MM-dd");
            string todate = "";

            if (n == 364)
            {
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                todate = firstDay.ToString("yyyy-MM-dd");
            }
            else
            {
                DateTime dt2 = TimeZoneInfo.ConvertTime(DateTime.Today.AddDays(-n), timeZoneInfo);
                todate = dt2.ToString("yyyy-MM-dd");
            }

            mydataservice objMD = new mydataservice();
            var chartsdata = objMD.TopItemsByQtyListdata(loggedcompid, todate, frdate); // calling method Listdata
            return Json(chartsdata, JsonRequestBehavior.AllowGet); // returning list from here.
        }








        public JsonResult TopItemsByValue(string d)
        {
            Int64 n = Convert.ToInt64(d);
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string frdate = dt.ToString("yyyy-MM-dd");
            string todate = "";

            if (n == 364)
            {
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                todate = firstDay.ToString("yyyy-MM-dd");
            }
            else
            {
                DateTime dt2 = TimeZoneInfo.ConvertTime(DateTime.Today.AddDays(-n), timeZoneInfo);
                todate = dt2.ToString("yyyy-MM-dd");
            }


            mydataservice objMD = new mydataservice();
            var chartsdata = objMD.TopItemsByValueListdata(loggedcompid, todate, frdate); // calling method Listdata
            return Json(chartsdata, JsonRequestBehavior.AllowGet); // returning list from here.
        }








        public JsonResult CollectionData(string d)
        {
            Int64 n = Convert.ToInt64(d);
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string frdate = dt.ToString("yyyy-MM-dd");
            string todate = "";

            if (n == 364)
            {
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                todate = firstDay.ToString("yyyy-MM-dd");
            }
            else
            {
                DateTime dt2 = TimeZoneInfo.ConvertTime(DateTime.Today.AddDays(-n), timeZoneInfo);
                todate = dt2.ToString("yyyy-MM-dd");
            }


            mydataservice objMD = new mydataservice();
            var chartsdata = objMD.CollectionDataListdata(loggedcompid, todate, frdate); // calling method Listdata
            return Json(chartsdata, JsonRequestBehavior.AllowGet); // returning list from here.
        }









        public JsonResult TimeWiseSellData(string d)
        {
            Int64 n = Convert.ToInt64(d);
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            string frdate = dt.ToString("yyyy-MM-dd");
            string todate = "";

            if (n == 364)
            {
                int year = DateTime.Now.Year;
                DateTime firstDay = new DateTime(year, 1, 1);
                todate = firstDay.ToString("yyyy-MM-dd");
            }
            else
            {
                DateTime dt2 = TimeZoneInfo.ConvertTime(DateTime.Today.AddDays(-n), timeZoneInfo);
                todate = dt2.ToString("yyyy-MM-dd");
            }

            mydataservice objMD = new mydataservice();
            var chartsdata = objMD.TimeWiseSellDataListdata(loggedcompid, todate, frdate); // calling method Listdata
            return Json(chartsdata, JsonRequestBehavior.AllowGet); // returning list from here.
        }


    

      
      
    }
}
