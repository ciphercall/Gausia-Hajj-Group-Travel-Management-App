using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Travel.Models.Travel;

namespace Travel.Controllers.Travel
{
    public class LicenseController : AppController
    {
       //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public LicenseController()
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
                     where n.COMPID == model.license.COMPID && n.USERID == model.license.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.license.COMPID);
            aslLog.USERID = model.license.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.license.INSIPNO;
            aslLog.LOGLTUDE = model.license.INSLTUDE;
            aslLog.TABLEID = "License";
            aslLog.LOGDATA =
                Convert.ToString("License id: " + model.license.LICENSEID + ",\nLicense No: " + model.license.LICENSENO + ",\nLicense Name: " + model.license.LICENSENM  + ",\nRemarks: " + model.license.Remarks + ".");
            aslLog.USERPC = model.license.USERPC;
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
                      where n.COMPID == model.license.COMPID && n.USERID == model.license.INSUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.license.COMPID);
            aslLog.USERID = model.license.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.license.UPDIPNO;
            aslLog.LOGLTUDE = model.license.UPDLTUDE;
            aslLog.TABLEID = "license";
            aslLog.LOGDATA =
                Convert.ToString("license id: " + model.license.LICENSEID + ",\nLicense No: " + model.license.LICENSENO + ",\nlicense Name: " + model.license.LICENSENM + ",\nRemarks: " + model.license.Remarks + ".");
            aslLog.USERPC = model.license.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Delete_Asl_LogData(TAMS_LICENSE alicense)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                 Convert.ToInt64(
                     (from n in db.AslLogDbSet
                      where n.COMPID == alicense.COMPID && n.USERID == alicense.INSUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(alicense.COMPID);
            aslLog.USERID = alicense.UPDUSERID;
            aslLog.LOGTYPE = "Delete";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = alicense.UPDIPNO;
            aslLog.LOGLTUDE = alicense.UPDLTUDE;
            aslLog.TABLEID = "license";
            aslLog.LOGDATA =
                Convert.ToString("license id: " + alicense.LICENSEID + ",\nLicense No: " + alicense.LICENSENO + ",\nlicense Name: " + alicense.LICENSENM + ",\nRemarks: " + alicense.Remarks + ".");
            aslLog.USERPC = alicense.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();

        // Delete ALL INFORMATION from CNF_EXPMST TO ASL_DELETE Database Table.
        public void Delete_ASL_DELETE(TAMS_LICENSE aLicense)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == aLicense.COMPID && n.USERID == aLicense.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(aLicense.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = aLicense.UPDIPNO;
            AslDelete.DELLTUDE = aLicense.UPDLTUDE;
            AslDelete.TABLEID = "TAMS_LICENSE";
            AslDelete.DELDATA = Convert.ToString("license id: " + aLicense.LICENSEID + ",\nLicense No: " + aLicense.LICENSENO + ",\nlicense Name: " + aLicense.LICENSENM + ",\nRemarks: " + aLicense.Remarks + ".");
            AslDelete.USERPC = aLicense.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }


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

                var maxvalue = Convert.ToInt64
                    ((from n in db.licenseDbSet where n.COMPID == model.license.COMPID select n.LICENSEID).Max());
                if (maxvalue == 0)
                {
                    model.license.USERPC = strHostName;
                    model.license.INSIPNO = ipAddress.ToString();
                    model.license.INSTIME = Convert.ToDateTime(td);

                    model.license.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.license.LICENSEID = Convert.ToInt64(Convert.ToString(model.license.COMPID) + "01");

                    Insert_Asl_LogData(model);
                    db.licenseDbSet.Add(model.license);
                    db.SaveChanges();

                    TempData["licenseMessage"] = "license Created ";
                }
                else
                {
                    model.license.USERPC = strHostName;
                    model.license.INSIPNO = ipAddress.ToString();
                    model.license.INSTIME = Convert.ToDateTime(td);

                    model.license.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.license.LICENSEID = maxvalue + 1;

                    Insert_Asl_LogData(model);
                    db.licenseDbSet.Add(model.license);
                    db.SaveChanges();

                    TempData["licenseMessage"] = "license Created ";
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
                db.Entry(model.license).State = EntityState.Modified;
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                model.license.USERPC = strHostName;
                model.license.INSIPNO = ipAddress.ToString();
                model.license.INSTIME = Convert.ToDateTime(td);

                model.license.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                model.license.UPDIPNO = ipAddress.ToString();
                model.license.UPDTIME = Convert.ToDateTime(td);
                //asluserco.UPDTIME = DateTime.Parse(td.ToString(), dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                //Update User ID save AslUSerco table attribute INSUSERID
                model.license.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                model.license.INSLTUDE = model.license.INSLTUDE;
                model.license.UPDLTUDE = model.license.INSLTUDE;

                model.license.Id = model.license.Id;
                model.license.LICENSEID = model.license.LICENSEID;
                model.license.LICENSENO = model.license.LICENSENO;

                model.license.LICENSENM = model.license.LICENSENM;


                model.license.Remarks = model.license.Remarks;

                Update_Asl_LogData(model);

                db.SaveChanges();
                TempData["licenseUpdate"] = "'" + model.license.LICENSENO + "' successfully updated!";

                return RedirectToAction("Update");
            }


            return RedirectToAction("Update");
        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Update_SelectShortID(string changedtxt)
        {

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            var selectdata = from n in db.licenseDbSet where n.COMPID == compid && n.LICENSENO == changedtxt select n;

            string carrierno = "", licensename = "", remarks = "";

            Int64 licenseid = 0, primaryid = 0;

            foreach (var l in selectdata)
            {
                primaryid = Convert.ToInt64(l.Id);

               

                licenseid = Convert.ToInt64(l.LICENSEID);
               
                licensename = Convert.ToString(l.LICENSENM);
                remarks = Convert.ToString(l.Remarks);

            }

            var result = new { keyid = primaryid, id = licenseid, name = licensename, Remarks = remarks };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete()
         {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PageModel model)
        {
            if (model.license.LICENSEID != null)
            {
                TAMS_LICENSE alicense = db.licenseDbSet.Find(model.license.Id);


                //Get Ip ADDRESS,Time & user PC Name 
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                alicense.USERPC = strHostName;
                alicense.UPDIPNO = ipAddress.ToString();
                alicense.UPDTIME = Convert.ToDateTime(td);

                //Delete User ID save AslUSerco table attribute INSUSERID
                alicense.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                //Get current LOGLTUDE data 
                model.license.INSLTUDE = model.license.INSLTUDE;
                alicense.UPDLTUDE = model.license.INSLTUDE;

                Delete_Asl_LogData(alicense);
                Delete_ASL_DELETE(alicense);

                db.licenseDbSet.Remove(alicense);
                db.SaveChanges();

                TempData["licenseDelete"] = "'" + alicense.LICENSENO + "' successfully Deleted";
                return RedirectToAction("Delete");
            }
            else
            {
                return View("Delete");
            }



        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Update_SelectLicenseID(string changedtxt)
        {
            Int64 Licenseid = Convert.ToInt64(changedtxt);
            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            var selectdata = from n in db.licenseDbSet where n.COMPID == compid && n.LICENSEID == Licenseid select n;

            string licenseno = "", licensename = "", remarks = "";

            Int64 licenseid = 0, primaryid = 0;

            foreach (var l in selectdata)
            {
                primaryid = Convert.ToInt64(l.Id);

                //string xx = Convert.ToString(l.CHEQUEDT);
                //DateTime date = DateTime.Parse(xx);
                //chequedate = date.ToString("dd-MMM-yyyy");

                licenseid = Convert.ToInt64(l.LICENSEID);
                licenseno = Convert.ToString(l.LICENSENO);
                licensename = Convert.ToString(l.LICENSENM);
                remarks = Convert.ToString(l.Remarks);

            }

            var result = new { keyid = primaryid, id = licenseid, no = licenseno, name = licensename, Remarks = remarks };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


	}
}