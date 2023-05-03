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
    public class CarrierController : AppController
    {


        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public CarrierController()
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
                     where n.COMPID == model.carrier.COMPID && n.USERID == model.carrier.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.carrier.COMPID);
            aslLog.USERID = model.carrier.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.carrier.INSIPNO;
            aslLog.LOGLTUDE = model.carrier.INSLTUDE;
            aslLog.TABLEID = "Carrier";
            aslLog.LOGDATA =
                Convert.ToString("carrier id: " + model.carrier.CARRIERID + ",\nShort Id: " + model.carrier.CARRIERSID + ",\ncarrier no: " + model.carrier.CARRIERNO + ",\ncarrier name: " + model.carrier.CARRIERNM + ",\nRemarks: " + model.carrier.REMARKS + ".");
            aslLog.USERPC = model.carrier.USERPC;
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
                      where n.COMPID == model.carrier.COMPID && n.USERID == model.carrier.INSUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.carrier.COMPID);
            aslLog.USERID = model.carrier.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.carrier.UPDIPNO;
            aslLog.LOGLTUDE = model.carrier.UPDLTUDE;
            aslLog.TABLEID = "Carrier";
            aslLog.LOGDATA =
                Convert.ToString("carrier id: " + model.carrier.CARRIERID + ",\nShort Id: " + model.carrier.CARRIERSID + ",\ncarrier no: " + model.carrier.CARRIERNO + ",\ncarrier name: " + model.carrier.CARRIERNM + ",\nRemarks: " + model.carrier.REMARKS + ".");
            aslLog.USERPC = model.carrier.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Delete_Asl_LogData(TAMS_CARRIER aCarrier)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                 Convert.ToInt64(
                     (from n in db.AslLogDbSet
                      where n.COMPID == aCarrier.COMPID && n.USERID == aCarrier.INSUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(aCarrier.COMPID);
            aslLog.USERID = aCarrier.UPDUSERID;
            aslLog.LOGTYPE = "Delete";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = aCarrier.UPDIPNO;
            aslLog.LOGLTUDE = aCarrier.UPDLTUDE;
            aslLog.TABLEID = "Carrier";
            aslLog.LOGDATA =
                Convert.ToString("carrier id: " + aCarrier.CARRIERID + ",\nShort Id: " + aCarrier.CARRIERSID + ",\ncarrier no: " + aCarrier.CARRIERNO + ",\ncarrier name: " + aCarrier.CARRIERNM + ",\nRemarks: " + aCarrier.REMARKS + ".");
            aslLog.USERPC = aCarrier.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();

        // Delete ALL INFORMATION from CNF_EXPMST TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE(TAMS_CARRIER aCarrier)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == aCarrier.COMPID && n.USERID == aCarrier.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(aCarrier.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = aCarrier.UPDIPNO;
            AslDelete.DELLTUDE = aCarrier.UPDLTUDE;
            AslDelete.TABLEID = "TAMS_CARRIER";
            AslDelete.DELDATA = Convert.ToString("carrier id: " + aCarrier.CARRIERID + ",\nShort Id: " + aCarrier.CARRIERSID + ",\ncarrier no: " + aCarrier.CARRIERNO + ",\ncarrier name: " + aCarrier.CARRIERNM + ",\nRemarks: " + aCarrier.REMARKS + ".");
            AslDelete.USERPC = aCarrier.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }



        // GET: /Carrier/

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PageModel model, string command)
        {
            if (command == "Create")
            {
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                var maxvalue =Convert.ToInt64
                    ((from n in db.carrierDbSet where n.COMPID == model.carrier.COMPID select n.CARRIERID).Max());
                if (maxvalue == 0)
                {
                    model.carrier.USERPC = strHostName;
                    model.carrier.INSIPNO = ipAddress.ToString();
                    model.carrier.INSTIME = Convert.ToDateTime(td);

                    model.carrier.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.carrier.CARRIERID = Convert.ToInt64(Convert.ToString(model.carrier.COMPID) + "01");

                    Insert_Asl_LogData(model);
                    db.carrierDbSet.Add(model.carrier);
                    db.SaveChanges();

                    TempData["CarrierMessage"] = "Carrier Created ";
                }
                else
                {
                    model.carrier.USERPC = strHostName;
                    model.carrier.INSIPNO = ipAddress.ToString();
                    model.carrier.INSTIME = Convert.ToDateTime(td);

                    model.carrier.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.carrier.CARRIERID = maxvalue + 1;

                    Insert_Asl_LogData(model);
                    db.carrierDbSet.Add(model.carrier);
                    db.SaveChanges();

                    TempData["CarrierMessage"] = "Carrier Created ";
                }

            }

            return RedirectToAction("Create");
        }



        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PageModel model, string command)
        {
            if (command == "Update")
            {
                db.Entry(model.carrier).State = EntityState.Modified;
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                model.carrier.USERPC = strHostName;
                model.carrier.INSIPNO = ipAddress.ToString();
                model.carrier.INSTIME = Convert.ToDateTime(td);

                model.carrier.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                model.carrier.UPDIPNO = ipAddress.ToString();
                model.carrier.UPDTIME = Convert.ToDateTime(td);
                //asluserco.UPDTIME = DateTime.Parse(td.ToString(), dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //Update User ID save AslUSerco table attribute INSUSERID
                model.carrier.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                model.carrier.INSLTUDE = model.carrier.INSLTUDE;
                model.carrier.UPDLTUDE = model.carrier.INSLTUDE;

                model.carrier.Id = model.carrier.Id;
                model.carrier.CARRIERID = model.carrier.CARRIERID;
                model.carrier.CARRIERSID = model.carrier.CARRIERSID;
                model.carrier.CARRIERNO = model.carrier.CARRIERNO;
                model.carrier.CARRIERNM = model.carrier.CARRIERNM;


                model.carrier.REMARKS = model.carrier.REMARKS;

                Update_Asl_LogData(model);

                db.SaveChanges();
                TempData["CarrierUpdate"] = "'" + model.carrier.CARRIERSID + "' successfully updated!";

                return RedirectToAction("Update");
            }


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
            if (model.carrier.CARRIERSID != null)
            {
                TAMS_CARRIER aCarrier = db.carrierDbSet.Find(model.carrier.Id);


                //Get Ip ADDRESS,Time & user PC Name 
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aCarrier.USERPC = strHostName;
                aCarrier.UPDIPNO = ipAddress.ToString();
                aCarrier.UPDTIME = Convert.ToDateTime(td);

                //Delete User ID save AslUSerco table attribute INSUSERID
                aCarrier.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                //Get current LOGLTUDE data 
                model.carrier.INSLTUDE = model.carrier.INSLTUDE;
                aCarrier.UPDLTUDE = model.carrier.INSLTUDE;

                Delete_Asl_LogData(aCarrier);
                Delete_ASL_DELETE(aCarrier);

                db.carrierDbSet.Remove(aCarrier);
                db.SaveChanges();

                TempData["CarrierDelete"] = "'" + aCarrier.CARRIERSID + "' successfully Deleted";
                return RedirectToAction("Delete");
            }
            else
            {
                return View("Delete");
            }



        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Update_SelectShortID(string changedtxt)
        {

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            var selectdata = from n in db.carrierDbSet where n.COMPID == compid && n.CARRIERSID == changedtxt select n;

            string carrierno = "", carriername = "", remarks = "";

            Int64 carrierid = 0, primaryid = 0;

            foreach (var l in selectdata)
            {
                primaryid = Convert.ToInt64(l.Id);

                //string xx = Convert.ToString(l.CHEQUEDT);
                //DateTime date = DateTime.Parse(xx);
                //chequedate = date.ToString("dd-MMM-yyyy");

                carrierid = Convert.ToInt64(l.CARRIERID);
                carrierno = Convert.ToString(l.CARRIERNO);
                carriername = Convert.ToString(l.CARRIERNM);
                remarks = Convert.ToString(l.REMARKS);

            }

            var result = new { keyid = primaryid, id = carrierid, no = carrierno, name = carriername, Remarks = remarks };

            return Json(result, JsonRequestBehavior.AllowGet);
        }




    }
}
