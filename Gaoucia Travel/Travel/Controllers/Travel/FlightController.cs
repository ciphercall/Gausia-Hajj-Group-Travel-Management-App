using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Travel.Models.Travel;

namespace Travel.Controllers.Travel
{
    public class FlightController : AppController
    {
          //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;





        public FlightController()
        {
            ViewData["Hightlight_valid_Travel_Form"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }


        public ASL_LOG aslLog = new ASL_LOG();

        public void Insert_Asl_LogData(PageModel model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == model.flight.COMPID && n.USERID == model.flight.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.flight.COMPID);
            aslLog.USERID = model.flight.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.flight.INSIPNO;
            aslLog.LOGLTUDE = model.flight.INSLTUDE;
            aslLog.TABLEID = "Flight";
            aslLog.LOGDATA =
                Convert.ToString("Card No: " + model.passenger.CARDNO + ",\nPassenger name: " + model.passenger.PSGRNM + ",\nFlight No: " + model.flight.FLIGHTNO + ",\nFlight Date: " + model.flight.FLIGHTDT + ",\nCarrier sid: " + model.flight.CARRIERSID + ".");
            aslLog.USERPC = model.flight.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Update_Asl_LogData(PageModel model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                 Convert.ToInt64(
                     (from n in db.AslLogDbSet
                      where n.COMPID == model.flight.COMPID && n.USERID == model.flight.INSUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.flight.COMPID);
            aslLog.USERID = model.flight.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.flight.UPDIPNO;
            aslLog.LOGLTUDE = model.flight.UPDLTUDE;
            aslLog.TABLEID = "Flight";
            aslLog.LOGDATA =
                 Convert.ToString("Card No: " + model.passenger.CARDNO + ",\nPassenger name: " + model.passenger.PSGRNM + ",\nFlight No: " + model.flight.FLIGHTNO + ",\nFlight Date: " + model.flight.FLIGHTDT + ",\nCarrier sid: " + model.flight.CARRIERSID + ".");
            aslLog.USERPC = model.flight.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Delete_Asl_LogData(TAMS_FLIGHT aFlight)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                 Convert.ToInt64(
                     (from n in db.AslLogDbSet
                      where n.COMPID == aFlight.COMPID && n.USERID == aFlight.INSUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aFlight.COMPID);
            aslLog.USERID = aFlight.UPDUSERID;
            aslLog.LOGTYPE = "Delete";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aFlight.UPDIPNO;
            aslLog.LOGLTUDE = aFlight.UPDLTUDE;
            aslLog.TABLEID = "Flight";
            aslLog.LOGDATA =
                Convert.ToString("Card ID: " + aFlight.CARDID + ",\nFlight No: " + aFlight.FLIGHTNO + ",\nFlight Date: " +aFlight.FLIGHTDT + ",\nCarrier sid: " + aFlight.CARRIERSID + ".");
            aslLog.USERPC = aFlight.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();

      
        public void Delete_ASL_DELETE(TAMS_FLIGHT aFlight)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == aFlight.COMPID && n.USERID == aFlight.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(aFlight.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = aFlight.UPDIPNO;
            AslDelete.DELLTUDE = aFlight.UPDLTUDE;
            AslDelete.TABLEID = "TAMS_FLIGHT";
            AslDelete.DELDATA = Convert.ToString("Card ID: " + aFlight.CARDID + ",\nFlight No: " + aFlight.FLIGHTNO + ",\nFlight Date: " + aFlight.FLIGHTDT + ",\nCarrier sid: " + aFlight.CARRIERSID + ".");
            AslDelete.USERPC = aFlight.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }



        // GET: /Flight/

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PageModel model)
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];


            string xx = Convert.ToString(td);
            DateTime ss = DateTime.Parse(xx);
            string today = ss.ToString("dd-MM-yyyy");

            string yy = today.Substring(6, 4)+today.Substring(3,2);

            model.flight.USERPC = strHostName;
            model.flight.INSIPNO = ipAddress.ToString();
            model.flight.INSTIME = Convert.ToDateTime(td);

            model.flight.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            var result = (from n in db.passengerDbSet
                where n.COMPID == model.flight.COMPID && n.CARDNO == model.passenger.CARDNO
                select new {n.CARDYY, n.CARDID}).ToList();
            foreach (var item in result)
            {
                model.flight.CARDID = Convert.ToInt64(item.CARDID);
                model.flight.CARDYY = Convert.ToInt64(item.CARDYY);
            }
            Int64 flight_sl = 0;
            try
            {
                 flight_sl = Convert.ToInt64((from n in db.flightDbSet where n.COMPID == model.flight.COMPID select n.FLIGHTSL).Max());
            }
            catch(Exception e) 
            {
                flight_sl = 0;
            }
            if(flight_sl==0)
            {
                string ff = yy + "0001";
                model.flight.FLIGHTSL = Convert.ToInt64(ff);
            }
            else
            {
                string ff = Convert.ToString(flight_sl).Substring(0, 6);
                if(yy==ff)
                {
                    ff = Convert.ToString(flight_sl + 1);
                    model.flight.FLIGHTSL = Convert.ToInt64(ff);
                }
                else
                {
                    ff = yy + "0001"; 
                    model.flight.FLIGHTSL = Convert.ToInt64(ff);
                }
                
            }
            Insert_Asl_LogData(model);
            db.flightDbSet.Add(model.flight);
            db.SaveChanges();

            TempData["FlightMessage"] = "Flight Information Created for Card No: "+model.passenger.CARDNO+" - "+model.passenger.PSGRNM;



            return RedirectToAction("Create");
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PageModel model)
        {
            db.Entry(model.flight).State = EntityState.Modified;
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            model.flight.USERPC = strHostName;
            model.flight.INSIPNO = ipAddress.ToString();
            model.flight.INSTIME = Convert.ToDateTime(td);

            model.flight.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


            model.flight.UPDIPNO = ipAddress.ToString();
            model.flight.UPDTIME = Convert.ToDateTime(td);
         
            model.flight.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            model.flight.INSLTUDE = model.flight.INSLTUDE;
            model.flight.UPDLTUDE = model.flight.INSLTUDE;



            var result = (from n in db.passengerDbSet
                          where n.COMPID == model.flight.COMPID && n.CARDNO == model.passenger.CARDNO
                          select new { n.CARDYY, n.CARDID }).ToList();
            foreach (var item in result)
            {
                model.flight.CARDID = Convert.ToInt64(item.CARDID);
                model.flight.CARDYY = Convert.ToInt64(item.CARDYY);
            }

            model.flight.Id = model.flight.Id;
            model.flight.FLIGHTDT = model.flight.FLIGHTDT;
            model.flight.CARRIERSID = model.flight.CARRIERSID;
            model.flight.RETURNDT = model.flight.RETURNDT;
            model.flight.CLASS = model.flight.CLASS;


            model.flight.ROUTE = model.flight.ROUTE;
            model.flight.TIMEDEP = model.flight.TIMEDEP;
            model.flight.TIMEARR = model.flight.TIMEARR;
            model.flight.STATUS = model.flight.STATUS;
            model.flight.PNRNO = model.flight.PNRNO;
            model.flight.CONFIRMBY = model.flight.CONFIRMBY;
            model.flight.TKTTLIMIT = model.flight.TKTTLIMIT;


           Update_Asl_LogData(model);

            db.SaveChanges();
            TempData["FlightMessage"] = "Flight Information Updated for Card No: " + model.passenger.CARDNO + " - " + model.passenger.PSGRNM;
            return RedirectToAction("Update");
        }

        public ActionResult Delete()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PageModel model)
        {
            
                TAMS_FLIGHT aflight = db.flightDbSet.Find(model.flight.Id);


                //Get Ip ADDRESS,Time & user PC Name 
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aflight.USERPC = strHostName;
                aflight.UPDIPNO = ipAddress.ToString();
                aflight.UPDTIME = Convert.ToDateTime(td);

                //Delete User ID save AslUSerco table attribute INSUSERID
                aflight.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                //Get current LOGLTUDE data 
                model.flight.INSLTUDE = model.flight.INSLTUDE;
                aflight.UPDLTUDE = model.flight.INSLTUDE;

                Delete_Asl_LogData(aflight);
                Delete_ASL_DELETE(aflight);

                db.flightDbSet.Remove(aflight);
                db.SaveChanges();

                TempData["FlightMessage"] = "Flight Information Deleted for Card No: " + model.passenger.CARDNO + " - " + model.passenger.PSGRNM;
                return RedirectToAction("Delete");
           



        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CardNo_Changed(Int64 changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
          
            string PSGRNM = "";
            string changedtxt2 = Convert.ToString(changedtxt);

           
            var rt = (from n in db.passengerDbSet where n.COMPID == comid && n.CARDNO == changedtxt2 select n).ToList();
         
            foreach (var x in rt)
            {
               
                PSGRNM = x.PSGRNM;
               

               
            }


            var result = new
            {
               
                passenger = PSGRNM,
              


            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        //FlightNo load
        public JsonResult FlightNoLoad(string type)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> flightno = new List<SelectListItem>();
            string company = Convert.ToString(comid);
            string CARDID = company + "10202" + type;
            Int64 cardid = Convert.ToInt64(CARDID);
           
                var ans = from n in db.flightDbSet where n.COMPID == comid && n.CARDID==cardid select new { n.FLIGHTNO };
                foreach (var x in ans)
                {
                    flightno.Add(new SelectListItem { Text = x.FLIGHTNO, Value = x.FLIGHTNO });
                }






                return Json(new SelectList(flightno, "Value", "Text"));

        }




        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Update_SelectFlightno(string changedtxt, string changedtxt2)
        {

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            string company = Convert.ToString(compid);
            string CARDID = company + "10202" + changedtxt;
            Int64 cardid = Convert.ToInt64(CARDID);

            var selectdata = from n in db.flightDbSet where n.COMPID == compid && n.CARDID == cardid && n.FLIGHTNO == changedtxt2 select new { n.Id, n.CARRIERSID, n.FLIGHTDT, n.RETURNDT, n.CLASS, n.ROUTE, n.TIMEDEP, n.TIMEARR, n.STATUS, n.PNRNO, n.CONFIRMBY,n.TKTTLIMIT };

            string carriersid = "", flightdt = "", returndt = "", classs = "", root = "", timedep = "", timearr = "", status = "", pnrno = "", confirmby = "",tktime="";

            Int64 primaryid = 0;

            foreach (var l in selectdata)
            {
                primaryid = Convert.ToInt64(l.Id);


                carriersid = Convert.ToString(l.CARRIERSID);

                if (Convert.ToString(l.FLIGHTDT) != "")
                {
                    string aa = Convert.ToString(l.FLIGHTDT);
                    DateTime bb = DateTime.Parse(aa);
                    flightdt = bb.ToString("dd-MMM-yyyy");
                }



                if (Convert.ToString(l.RETURNDT) != "")
                {
                    string aa = Convert.ToString(l.RETURNDT);
                    DateTime bb = DateTime.Parse(aa);
                    returndt = bb.ToString("dd-MMM-yyyy");
                  
                }
               

                classs = Convert.ToString(l.CLASS);
                root = Convert.ToString(l.ROUTE);

                timedep = Convert.ToString(l.TIMEDEP);
                timearr = Convert.ToString(l.TIMEARR);
                status = Convert.ToString(l.STATUS);
                pnrno = Convert.ToString(l.PNRNO);
                confirmby = Convert.ToString(l.CONFIRMBY);


                if (Convert.ToString(l.TKTTLIMIT) != "")
                {
                    string aa = Convert.ToString(l.TKTTLIMIT);
                    DateTime bb = DateTime.Parse(aa);
                    tktime = bb.ToString("dd-MMM-yyyy");
                  
                }
               
            }


            var result = new { keyid = primaryid, Carriersid = carriersid, FlightDt = flightdt, ReturnDt = returndt, Class = classs, Root = root, TimeDep = timedep, TimeArr = timearr, Status = status, PNRPO = pnrno, ConfirmBy = confirmby, Tktime = tktime };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
