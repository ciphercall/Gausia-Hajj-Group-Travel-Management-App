using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
//using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Travel.Models;
using Travel.Models.Travel;
using Travel.Models.Account;
using System.IO;

namespace Travel.Controllers.Travel
{
    public class PassengerController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public PassengerController()
        {
            ViewData["Hightlight_valid_Travel_Form"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }



        public ASL_LOG aslLog = new ASL_LOG();

        public void Insert_Asl_LogData(TAMS_PASSENGER model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == model.COMPID && n.USERID == model.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = model.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.INSIPNO;
            aslLog.LOGLTUDE = model.INSLTUDE;
            aslLog.TABLEID = "TAMS_PASSENGER";
            aslLog.LOGDATA =
                Convert.ToString("CARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Update_Asl_LogData(TAMS_PASSENGER model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                 Convert.ToInt64(
                     (from n in db.AslLogDbSet
                      where n.COMPID == model.COMPID && n.USERID == model.UPDUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = model.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.UPDIPNO;
            aslLog.LOGLTUDE = model.UPDLTUDE;
            aslLog.TABLEID = "TAMS_PASSENGER";
            aslLog.LOGDATA =
                Convert.ToString("CARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Update_Asl_LogData2(TAMS_PASSENGER model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                 Convert.ToInt64(
                     (from n in db.AslLogDbSet
                      where n.COMPID == model.COMPID && n.USERID == model.UPDUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = model.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.UPDIPNO;
            aslLog.LOGLTUDE = model.UPDLTUDE;
            aslLog.TABLEID = "TAMS_PASSENGER";
            aslLog.LOGDATA =
                Convert.ToString("Passenger Name: " + model.PSGRNM + ",\nCARD NO: " + model.CARDNO + ",\nPermanent add: " + model.PERADDR + ",\nDistrict: " + model.PERDIST + ",\nPost Office: " + model.PERPO + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Delete_Asl_LogData(TAMS_PASSENGER model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));


            Int64 maxSerialNo =
                Convert.ToInt64(
                    (from n in db.AslLogDbSet
                     where n.COMPID == model.COMPID && n.USERID == model.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = model.INSUSERID;
            aslLog.LOGTYPE = "Delete";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.INSIPNO;
            aslLog.LOGLTUDE = model.INSLTUDE;
            aslLog.TABLEID = "TAMS_PASSENGER";
            aslLog.LOGDATA =
                Convert.ToString("CARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();


        public void Delete_ASL_DELETE(TAMS_PASSENGER model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslDeleteDbSet where n.COMPID == model.COMPID && n.USERID == model.UPDUSERID select n.DELSLNO).Max());
            if (maxSerialNo == 0)
            {
                AslDelete.DELSLNO = Convert.ToInt64("1");
            }
            else
            {
                AslDelete.DELSLNO = maxSerialNo + 1;
            }

            AslDelete.COMPID = Convert.ToInt64(model.COMPID);
            AslDelete.USERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = model.UPDIPNO;
            AslDelete.DELLTUDE = model.UPDLTUDE;
            AslDelete.TABLEID = "TAMS_PASSENGER";
            AslDelete.DELDATA = Convert.ToString("CARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            AslDelete.USERPC = model.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }




        // GET: /Passenger/
        public ActionResult FirstPartCreate()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstPartCreate(HttpPostedFileBase file, TAMS_PASSENGER model, string command)
        {



            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            if (command == "Create")
            {
                model.USERPC = strHostName;
                model.INSIPNO = ipAddress.ToString();
                model.INSTIME = Convert.ToDateTime(td);
                model.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Passenger_Images/"), model.CARDNO + "_" + fileName);

                    model.CARDIDPIC = "/Passenger_Images/" + model.CARDNO + "_" + fileName;

                    file.SaveAs(path);
                    ViewBag.UploadMessage = "Upload successfully done! ";
                }
                catch
                {
                    ViewBag.UploadMessage = "Image file is not in correct Format.";
                }


                Int64 max_cardno = Convert.ToInt64(
               (from n in db.passengerDbSet where n.COMPID == model.COMPID && n.CARDYY == model.CARDYY select n.CARDNO).Max());
                if (max_cardno == 0)
                {
                    string aa = Convert.ToString(model.CARDYY);
                    model.CARDNO = aa.Substring(2, 2) + "00001";
                    model.CARDID = model.COMPID + "10202" + model.CARDNO;
                    db.passengerDbSet.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    model.CARDNO = Convert.ToString(max_cardno + 1);
                    model.CARDID = model.COMPID + "10202" + model.CARDNO;
                    Int64 length = Convert.ToInt64(model.CARDNO.Length);
                    if (length == 7)
                    {
                        db.passengerDbSet.Add(model);
                        db.SaveChanges();

                    }
                    else
                    {
                        TempData["PassengerMessage"] = "Passenger Not Created ";

                        return RedirectToAction("FirstPartCreate");
                    }
                }






















                //Insert_Asl_LogData(model);

                Int64 partyid = Convert.ToInt64(Convert.ToString(model.COMPID) + "102020200001");

                if (model.AGENTID == partyid)
                {
                    GL_ACCHART acchart = new GL_ACCHART();



                    acchart.USERPC = model.USERPC;
                    acchart.INSIPNO = model.INSIPNO;
                    acchart.INSLTUDE = model.INSLTUDE;
                    acchart.INSTIME = model.INSTIME;
                    acchart.INSUSERID = Convert.ToInt64(model.INSUSERID);

                    acchart.COMPID = Convert.ToInt64(model.COMPID);
                    acchart.HEADTP = 1;

                    acchart.ACCOUNTCD = Convert.ToInt64(Convert.ToString(model.COMPID) + "10202" + model.CARDNO);
                    acchart.CONTROLCD = Convert.ToInt64(Convert.ToString(model.COMPID) + "10202" + model.CARDNO.ToString().Substring(0, 2) + "00000");
                    acchart.ACCOUNTNM = model.CARDNO + "-" + model.PSGRNM;
                    acchart.BRANCHID = 0;
                    acchart.HLEVELCD = 5;
                    acchart.HDRCRTP = "D";
                    acchart.HPOSTTP = "P";
                    acchart.HSTATUS = "A";
                    acchart.USERPC = strHostName;
                    acchart.INSIPNO = ipAddress.ToString();
                    acchart.INSTIME = Convert.ToDateTime(td);
                    acchart.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    db.GlAcchartDbSet.Add(acchart);

                    db.SaveChanges();




                }
                TempData["PassengerMessage"] = "Card No:" + model.CARDNO + " - " + model.PSGRNM + " Created";

                return RedirectToAction("FirstPartCreate");


            }
            else if (command == "Create & Print")
            {
                model.USERPC = strHostName;
                model.INSIPNO = ipAddress.ToString();
                model.INSTIME = Convert.ToDateTime(td);
                model.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);





                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Passenger_Images/"), model.CARDNO + "_" + fileName);

                    model.CARDIDPIC = "/Passenger_Images/" + model.CARDNO + "_" + fileName;

                    file.SaveAs(path);
                    ViewBag.UploadMessage = "Upload successfully done! ";
                }
                catch
                {
                    ViewBag.UploadMessage = "Image file is not in correct Format.";
                }


                Int64 max_cardno = Convert.ToInt64(
               (from n in db.passengerDbSet where n.COMPID == model.COMPID && n.CARDYY == model.CARDYY select n.CARDNO).Max());
                if (max_cardno == 0)
                {
                    string aa = Convert.ToString(model.CARDYY);
                    model.CARDNO = aa.Substring(2, 2) + "00001";
                    model.CARDID = model.COMPID + "10202" + model.CARDNO;
                    db.passengerDbSet.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    model.CARDNO = Convert.ToString(max_cardno + 1);
                    model.CARDID = model.COMPID + "10202" + model.CARDNO;
                    Int64 length = Convert.ToInt64(model.CARDNO.Length);
                    if (length == 7)
                    {
                        db.passengerDbSet.Add(model);
                        db.SaveChanges();

                    }
                    else
                    {
                        TempData["PassengerMessage"] = "Passenger Not Created ";

                        return RedirectToAction("FirstPartCreate");
                    }
                }

               // Insert_Asl_LogData(model);

                Int64 partyid = Convert.ToInt64(Convert.ToString(model.COMPID) + "102020200001");

                if (model.AGENTID == partyid)
                {
                    GL_ACCHART acchart = new GL_ACCHART();



                    acchart.USERPC = model.USERPC;
                    acchart.INSIPNO = model.INSIPNO;
                    acchart.INSLTUDE = model.INSLTUDE;
                    acchart.INSTIME = model.INSTIME;
                    acchart.INSUSERID = Convert.ToInt64(model.INSUSERID);

                    acchart.COMPID = Convert.ToInt64(model.COMPID);
                    acchart.HEADTP = 1;

                    acchart.ACCOUNTCD = Convert.ToInt64(Convert.ToString(model.COMPID) + "10202" + model.CARDNO);
                    acchart.CONTROLCD = Convert.ToInt64(Convert.ToString(model.COMPID) + "10202" + model.CARDNO.ToString().Substring(0, 2) + "00000");
                    acchart.ACCOUNTNM = model.CARDNO + "-" + model.PSGRNM;
                    acchart.BRANCHID = 0;
                    acchart.HLEVELCD = 5;
                    acchart.HDRCRTP = "D";
                    acchart.HPOSTTP = "P";
                    acchart.HSTATUS = "A";
                    acchart.USERPC = strHostName;
                    acchart.INSIPNO = ipAddress.ToString();
                    acchart.INSTIME = Convert.ToDateTime(td);
                    acchart.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    db.GlAcchartDbSet.Add(acchart);
                    db.SaveChanges();





                }
                TempData["PassengerCardInfo"] = model;
                return RedirectToAction("PassengerCardReport");

            }



            //return RedirectToAction("SecondPartCreate");


            return RedirectToAction("FirstPartCreate");
        }


        public ActionResult PassengerCardReport()
        {

            TAMS_PASSENGER model = (TAMS_PASSENGER)TempData["PassengerCardInfo"];
            return View(model);
        }


        public ActionResult SecondPartCreate()
        {
            var model = (TAMS_PASSENGER)TempData["FirstPartCreate_Model"];
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SecondPartCreate(TAMS_PASSENGER model)//create and update this is
        {
            //var FirstPartCreate_Model = (PageModel)TempData["FirstPartCreate_Model"];
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];



            var searchid = (from n in db.passengerDbSet where n.COMPID == model.COMPID && n.Id == model.Id select n);
            foreach (TAMS_PASSENGER item in searchid)
            {

                item.PERADDR = model.PERADDR;
                item.PERDIST = model.PERDIST;
                item.PERPC = model.PERPC;
                item.PERPO = model.PERPO;
                item.PERPS = model.PERPS;

                item.PREADDR = model.PREADDR;
                item.PREDIST = model.PREDIST;
                item.PREPC = model.PREPC;
                item.PREPO = model.PREPO;
                item.PREPS = model.PREPS;


                item.GRDNM = model.GRDNM;
                item.GRDREL = model.GRDREL;
                item.GRDMOBNO = model.GRDMOBNO;
                item.GRDEMAIL = model.GRDEMAIL;
                item.GRDWPSGR = model.GRDWPSGR;

                item.PKGSTP = model.PKGSTP;
                item.PKGSAMT = model.PKGSAMT;

                item.MOALLEMNM = model.MOALLEMNM;
                item.MOALLEMFEE = model.MOALLEMFEE;
                item.MOALLEMADDR = model.MOALLEMADDR;
                item.MOALLEMMOBNO = model.MOALLEMMOBNO;




                item.USERPC = strHostName;
                item.UPDIPNO = ipAddress.ToString();
                item.UPDTIME = Convert.ToDateTime(td);
                item.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                item.UPDLTUDE = model.INSLTUDE;
            }

            model.USERPC = strHostName;
            model.UPDIPNO = ipAddress.ToString();
            model.UPDTIME = Convert.ToDateTime(td);
            model.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            model.UPDLTUDE = model.INSLTUDE;

            Update_Asl_LogData2(model);

            db.SaveChanges();

            TempData["PassengerMessage"] = "Passenger Information Updated for Card No: " + model.CARDNO + " - " + model.PSGRNM;

            return RedirectToAction("SecondPartCreate");


        }






        //JseonResult for DateChanged and get year.............................Start
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getyear(DateTime changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(7, 4);

            Int64 getYearInt = Convert.ToInt64(getYear);
            string cardNo = "";
            var maxCardNo = Convert.ToInt64((from n in db.passengerDbSet where n.COMPID == comid && n.CARDYY == getYearInt select n.CARDNO).Max());
            if (maxCardNo == 0)
            {
                cardNo = getYear.Substring(2, 2) + "00001";
            }
            else
            {
                cardNo = Convert.ToString(maxCardNo + 1);
            }







            var result = new { year = getYear, cardno = cardNo };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //JseonResult for DateChanged and get year.............................End

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult YearChanged_getCardNo(string changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);



            Int64 getYearInt = Convert.ToInt64(changedtxt);
            string cardNo = "";
            var maxCardNo = Convert.ToInt64((from n in db.passengerDbSet where n.COMPID == comid && n.CARDYY == getYearInt select n.CARDNO).Max());
            if (maxCardNo == 0)
            {
                cardNo = changedtxt.Substring(2, 2) + "00001";
            }
            else
            {
                cardNo = Convert.ToString(maxCardNo + 1);
            }







            var result = new { cardno = cardNo };
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CardNo_Changed(Int64 changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 Id = 0;
            decimal pkgamt = 0, mllfee = 0;
            string Passengername = "", permanentadd = "", perpo = "", perpc = "", perps = "", perdist = "", preadd = "", prepo = "",
                prepc = "", preps = "", predist = "", Blood = "", gardian = "", gardianrel = "",
               pkgstp = "", gardianmobile = "", gardianemail = "", gardianpsgr = "", mllnm = "", mlladd = "", mllmob = "";
            string changedtxt2 = Convert.ToString(changedtxt);
            var rt = from n in db.passengerDbSet where n.COMPID == comid && n.CARDNO == changedtxt2 select n;


            //item.GRDEMAIL = model.GRDEMAIL;
            //item.GRDWPSGR = model.GRDWPSGR;
            foreach (var x in rt)
            {
                Id = x.Id;
                permanentadd = x.PERADDR;
                perpo = x.PERPO;
                perpc = x.PERPC;
                Passengername = x.PSGRNM;
                perps = x.PERPS;
                perdist = x.PERDIST;
                preadd = x.PREADDR;
                prepo = x.PREPO;
                prepc = x.PREPC;
                preps = x.PREPS;
                predist = x.PREDIST;

                gardian = x.GRDNM;
                gardianrel = x.GRDREL;
                gardianmobile = x.GRDMOBNO;
                gardianemail = x.GRDEMAIL;
                gardianpsgr = x.GRDWPSGR;

                pkgstp = x.PKGSTP;
                pkgamt = Convert.ToDecimal(x.PKGSAMT);
                mllfee = Convert.ToDecimal(x.MOALLEMFEE);
                mllnm = x.MOALLEMNM;
                mlladd = x.MOALLEMADDR;
                mllmob = x.MOALLEMMOBNO;

            }


            var result = new
            {
                id = Id,
                name = Passengername,
                PerAdd = permanentadd,
                Perpo = perpo,
                Perpc = perpc,
                Perps = perps,
                Perdist = perdist,
                Preadd = preadd,
                Prepo = prepo,
                Prepc = prepc,
                Preps = preps,
                Predist = predist,

                Gardian = gardian,
                Gardrel = gardianrel,
                Gardmob = gardianmobile,
                Gardemail = gardianemail,
                Gardpsgr = gardianpsgr,


                PKGamt = pkgamt,
                MoBlFee = mllfee,
                mollname = mllnm,
                molladdress = mlladd,
                mollmob = mllmob


            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CardNo_Changed2(Int64 changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 Id = 0, CARDYY = 0, AGE = 0, agentID = 0, CARDCID = 0, CARRIERID = 0, licenseid = 0, ngno = 0, plgrimid = 0;
            decimal pkgamt = 0, mllfee = 0;
            string PSGRNM = "",
                FATHERNM = "",
                MOTHERNM = "",
                NATIONALITY = "",
                PROFESSION = "",
                BIRTHPLACE = "",
                COUNTRY = "",
                ISSUEPLACE = "",
                ROOT = "",
                VNTP = "",
                VNNO = "",
                VNIPLACE = "",
                REMARKS = "",
                PASSPORTNO = "", Agentname = "", gg = "", Carriername = "", cardtype = "", designation = "", mobileno = "", telno = "", Nid = "", passporttp = "", img = "";
            string changedtxt2 = Convert.ToString(changedtxt);

            string CARDDT = "", DOB = "", ISSUEDT = "", EXPIREDT = "", VNIDT = "", VNEDT = "", packagetype = "", passengertp = "", maritalst = "", bloodgrp = "", education = "";
            var rt = (from n in db.passengerDbSet where n.COMPID == comid && n.CARDNO == changedtxt2 select n).ToList();

            foreach (var x in rt)
            {
                Id = x.Id;
                PSGRNM = x.PSGRNM;

                string aa = Convert.ToString(x.CARDDT);
                DateTime bb = DateTime.Parse(aa);
                CARDDT = bb.ToString("dd-MMM-yyyy");

                CARDYY = Convert.ToInt64(x.CARDYY);
                cardtype = x.CARDCID;
                packagetype = x.PKGSTP;
                licenseid = x.LICENSEID;
                agentID = Convert.ToInt64(x.AGENTID);
                passengertp = x.PSGRTP;
                FATHERNM = x.FATHERNM;
                MOTHERNM = x.MOTHERNM;

                if (Convert.ToString(x.DOB) != "")
                {
                    string xx = Convert.ToString(x.DOB);
                    DateTime yy = DateTime.Parse(xx);
                    DOB = yy.ToString("dd-MMM-yyyy");
                }


                NATIONALITY = x.NATIONALITY;
                maritalst = x.MSTATUS;
                gg = x.GENDER;
                bloodgrp = x.BLOODG;
                PROFESSION = x.PROFESSION;
                education = x.EDUCATION;
                designation = x.DESIGNATION;
                BIRTHPLACE = x.BIRTHPLACE;

                COUNTRY = x.COUNTRY;
                mobileno = x.MOBNO;
                telno = x.TELNO;
                Nid = x.NID;

                PASSPORTNO = x.PASSPORTNO;

                if (Convert.ToString(x.ISSUEDT) != "")
                {
                    string ss = Convert.ToString(x.ISSUEDT);
                    DateTime ff = DateTime.Parse(ss);
                    ISSUEDT = ff.ToString("dd-MMM-yyyy");
                }


                ISSUEPLACE = x.ISSUEPLACE;
                if (Convert.ToString(x.EXPIREDT) != "")
                {
                    string foring = Convert.ToString(x.EXPIREDT);
                    DateTime foring2 = DateTime.Parse(foring);
                    EXPIREDT = foring2.ToString("dd-MMM-yyyy");
                }


                passporttp = x.PASSPORTTP;
                ROOT = x.ROUTE;

                CARRIERID = Convert.ToInt64(x.CARRIERID);
                VNTP = x.VNTP;
                VNNO = x.VNNO;
                if (Convert.ToString(x.VNIDT) != "")
                {
                    string VNIDT1 = Convert.ToString(x.VNIDT);
                    DateTime VNIDT2 = DateTime.Parse(VNIDT1);
                    VNIDT = VNIDT2.ToString("dd-MMM-yyyy");

                }

                if (Convert.ToString(x.VNEDT) != "")
                {
                    string VNEDT1 = Convert.ToString(x.VNEDT);
                    DateTime VNED2 = DateTime.Parse(VNEDT1);
                    VNEDT = VNED2.ToString("dd-MMM-yyyy");
                }



                VNIPLACE = x.VNIPLACE;
                REMARKS = x.REMARKS;

                ngno = x.NGNO;
                plgrimid = x.PILGRIMID;
                img = x.CARDIDPIC;

            }



            var result = new
            {
                id = Id,
                passenger = PSGRNM,
                carddt = CARDDT,
                Year = CARDYY,
                Cardtype = cardtype,
                Packagetp = packagetype,
                License = licenseid,
                agentname = agentID,
                Passengertp = passengertp,

                fathername = FATHERNM,
                mothername = MOTHERNM,
                gender = gg,
                dateofbirth = DOB,

                nationality = NATIONALITY,
                profession = PROFESSION,
                Maritalst = maritalst,
                Bloodgrp = bloodgrp,
                Education = education,
                Designation = designation,

                birthplace = BIRTHPLACE,
                country = COUNTRY,

                Mobno = mobileno,
                telpno = telno,
                NID = Nid,


                passportno = PASSPORTNO,
                issudt = ISSUEDT,
                issueplace = ISSUEPLACE,
                expiredt = EXPIREDT,
                Passporttp = passporttp,

                root = ROOT,
                carriername = CARRIERID,
                vntype = VNTP,
                vnno = VNNO,
                vnidt = VNIDT,
                vnedt = VNEDT,
                vniplace = VNIPLACE,
                remarks = REMARKS,
                Ngno = ngno,
                Plgrimid = plgrimid,
                IMG = img


            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        //AutoComplete
        public JsonResult TagSearch(string term)
        {
            var compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());





            var tags = from p in db.passengerDbSet
                       where p.COMPID == compid
                       select p.CARDNO;


            return this.Json(tags.Where(t => t.StartsWith(term)),
                       JsonRequestBehavior.AllowGet);



        }
        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword(string changedText)
        {
            var compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String itemId = "";

            // string changedText = Convert.ToString(changedText1);

            var rt = db.passengerDbSet.Where(n => n.CARDNO.StartsWith(changedText) &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             costcnm = n.CARDNO

                                                         }).ToList();
            int lenChangedtxt = changedText.Length;

            Int64 cont = rt.Count();
            Int64 k = 0;
            string status = "";
            if (changedText != "" && cont != 0)
            {
                while (status != "no")
                {
                    k = 0;
                    foreach (var n in rt)
                    {
                        string ss = Convert.ToString(n.costcnm);
                        string subss = "";
                        if (ss.Length >= lenChangedtxt)
                        {
                            subss = ss.Substring(0, lenChangedtxt);
                            subss = subss.ToUpper();
                        }
                        else
                        {
                            subss = "";
                        }


                        if (subss == changedText.ToUpper())
                        {
                            k++;
                        }
                        if (k == cont)
                        {
                            status = "yes";
                            lenChangedtxt++;
                            if (ss.Length > lenChangedtxt - 1)
                            {
                                changedText = changedText + ss[lenChangedtxt - 1];
                            }

                        }
                        else
                        {
                            status = "no";
                            //lenChangedtxt--;
                        }

                    }

                }
                if (lenChangedtxt == 1)
                {
                    itemId = changedText.Substring(0, lenChangedtxt);
                }

                else
                {
                    itemId = changedText.Substring(0, lenChangedtxt - 1);
                }



            }
            else
            {
                itemId = changedText;
            }






            var result = new { Cardno = itemId };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult FirstPartUpdate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstPartUpdate(HttpPostedFileBase file, TAMS_PASSENGER model, string command)
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            if (command == "Update")
            {
                var searchid = (from n in db.passengerDbSet where n.COMPID == model.COMPID && n.Id == model.Id select n);


                Int64 agent = 0;

                foreach (TAMS_PASSENGER item in searchid)
                {

                    item.CARDDT = model.CARDDT;
                    item.CARDYY = model.CARDYY;
                    item.PKGSTP = model.PKGSTP;
                    if(item.AGENTID==101102020200001)
                    {

                    }
                    else{
                        if (model.AGENTID == 101102020200001)
                        {

                        }
                        else
                        {
                            item.AGENTID = model.AGENTID;
                        }
                       
                    }
                   
                    item.GENDER = model.GENDER;
                    item.FATHERNM = model.FATHERNM;
                    item.LICENSEID = model.LICENSEID;
                    item.PSGRTP = model.PSGRTP;
                    item.MOTHERNM = model.MOTHERNM;
                    item.NATIONALITY = model.NATIONALITY;
                    item.PROFESSION = model.PROFESSION;
                    item.DOB = model.DOB;
                    item.MSTATUS = model.MSTATUS;
                    item.GENDER = model.GENDER;
                    item.BLOODG = model.BLOODG;
                    item.EDUCATION = model.EDUCATION;
                    item.DESIGNATION = model.DESIGNATION;
                    item.TELNO = model.TELNO;
                    item.MOBNO = model.MOBNO;
                    item.NID = model.NID;
                    item.BIRTHPLACE = model.BIRTHPLACE;
                    item.COUNTRY = model.COUNTRY;

                    item.PASSPORTNO = model.PASSPORTNO;
                    item.ISSUEDT = model.ISSUEDT;
                    item.ISSUEPLACE = model.ISSUEPLACE;
                    item.PASSPORTTP = model.PASSPORTTP;
                    item.EXPIREDT = model.EXPIREDT;
                    item.ROUTE = model.ROUTE;
                    item.CARRIERID = model.CARRIERID;
                    item.VNTP = model.VNTP;
                    item.VNNO = model.VNNO;

                    item.VNIDT = model.VNIDT;
                    item.VNEDT = model.VNEDT;
                    item.VNIPLACE = model.VNIPLACE;
                    item.REMARKS = model.REMARKS;
                    item.CARDCID = model.CARDCID;

                    item.NGNO = model.NGNO;
                    item.PILGRIMID = model.PILGRIMID;

                    item.USERPC = strHostName;
                    item.UPDIPNO = ipAddress.ToString();
                    item.UPDTIME = Convert.ToDateTime(td);
                    item.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    item.UPDLTUDE = model.INSLTUDE;



                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Passenger_Images/"), model.CARDNO + "_" + fileName);

                        item.CARDIDPIC = "/Passenger_Images/" + model.CARDNO + "_" + fileName;

                        file.SaveAs(path);
                        ViewBag.UploadMessage = "Upload successfully done! ";
                    }
                    else
                    {
                        item.CARDIDPIC = item.CARDIDPIC;
                    }




                }

                model.USERPC = strHostName;
                model.UPDIPNO = ipAddress.ToString();
                model.UPDTIME = Convert.ToDateTime(td);
                model.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.UPDLTUDE = model.INSLTUDE;


                Update_Asl_LogData(model);
                //  db.passengerDbSet.Add(model);
                db.SaveChanges();

                TempData["PassengerMessage"] = "Passenger Information Updated for Card No: " + model.CARDNO + " - " + model.PSGRNM;

                return RedirectToAction("FirstPartUpdate");
            }
            else if (command == "Update & Print")
            {
                var searchid = (from n in db.passengerDbSet where n.COMPID == model.COMPID && n.Id == model.Id select n);




                foreach (TAMS_PASSENGER item in searchid)
                {

                    item.CARDDT = model.CARDDT;
                    item.CARDYY = model.CARDYY;
                    item.PKGSTP = model.PKGSTP;
                    item.AGENTID = model.AGENTID;
                    item.GENDER = model.GENDER;
                    item.FATHERNM = model.FATHERNM;
                    item.LICENSEID = model.LICENSEID;
                    item.PSGRTP = model.PSGRTP;
                    item.MOTHERNM = model.MOTHERNM;
                    item.NATIONALITY = model.NATIONALITY;
                    item.PROFESSION = model.PROFESSION;
                    item.DOB = model.DOB;
                    item.MSTATUS = model.MSTATUS;
                    item.GENDER = model.GENDER;
                    item.BLOODG = model.BLOODG;
                    item.EDUCATION = model.EDUCATION;
                    item.DESIGNATION = model.DESIGNATION;
                    item.TELNO = model.TELNO;
                    item.MOBNO = model.MOBNO;
                    item.NID = model.NID;
                    item.BIRTHPLACE = model.BIRTHPLACE;
                    item.COUNTRY = model.COUNTRY;

                    item.PASSPORTNO = model.PASSPORTNO;
                    item.ISSUEDT = model.ISSUEDT;
                    item.ISSUEPLACE = model.ISSUEPLACE;
                    item.PASSPORTTP = model.PASSPORTTP;
                    item.EXPIREDT = model.EXPIREDT;
                    item.ROUTE = model.ROUTE;
                    item.CARRIERID = model.CARRIERID;
                    item.VNTP = model.VNTP;
                    item.VNNO = model.VNNO;

                    item.VNIDT = model.VNIDT;
                    item.VNEDT = model.VNEDT;
                    item.VNIPLACE = model.VNIPLACE;
                    item.REMARKS = model.REMARKS;
                    item.CARDCID = model.CARDCID;

                    item.NGNO = model.NGNO;
                    item.PILGRIMID = model.PILGRIMID;

                    item.USERPC = strHostName;
                    item.UPDIPNO = ipAddress.ToString();
                    item.UPDTIME = Convert.ToDateTime(td);
                    item.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    item.UPDLTUDE = model.INSLTUDE;



                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Passenger_Images/"), model.CARDNO + "_" + fileName);

                        item.CARDIDPIC = "/Passenger_Images/" + model.CARDNO + "_" + fileName;

                        file.SaveAs(path);
                        ViewBag.UploadMessage = "Upload successfully done! ";
                    }
                    else
                    {
                        item.CARDIDPIC = item.CARDIDPIC;
                    }




                }

                model.USERPC = strHostName;
                model.UPDIPNO = ipAddress.ToString();
                model.UPDTIME = Convert.ToDateTime(td);
                model.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.UPDLTUDE = model.INSLTUDE;


                Update_Asl_LogData(model);
                //  db.passengerDbSet.Add(model);
                db.SaveChanges();

                TempData["PassengerCardInfo"] = model;
                return RedirectToAction("PassengerCardReport");
            }

            else
            {
                return RedirectToAction("FirstPartUpdate");
            }

        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TAMS_PASSENGER model)
        {
            model = db.passengerDbSet.Find(model.Id);

            var search_otherpassenger = (from n in db.passengerCardDbSet where n.COMPID == model.COMPID && n.CARDNO == model.CARDNO select n).ToList();
            Int64 acc=Convert.ToInt64(Convert.ToString(model.COMPID)+"10202"+model.CARDNO);

            var search_account = (from n in db.GlAcchartDbSet where n.COMPID == model.COMPID && n.ACCOUNTCD == acc select n).ToList();
            if(search_otherpassenger.Count!=0 || search_account.Count!=0)
            {
                TempData["PassengerMessage"] = "'" + model.CARDNO + "' couldn't be deleted";


                return RedirectToAction("Delete");
            }
            else
            {
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];


                model.USERPC = strHostName;
                model.UPDIPNO = ipAddress.ToString();
                model.UPDTIME = Convert.ToDateTime(td);
                model.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                model.UPDLTUDE = model.INSLTUDE;

                Delete_Asl_LogData(model);
                Delete_ASL_DELETE(model);

                db.passengerDbSet.Remove(model);
                db.SaveChanges();

                TempData["PassengerMessage"] = "'" + model.CARDNO + "' successfully Deleted";


                return RedirectToAction("Delete");
            }
            
        }

        public JsonResult Moallemname(string term)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());





            var tags = from p in db.passengerDbSet
                       where p.COMPID == compid
                       select p.MOALLEMNM;


            return this.Json(tags.Where(t => t.StartsWith(term)),
                       JsonRequestBehavior.AllowGet);



        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Moallemnm_changed(string changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);






            string Add = "", MOB = "";
            var rt = (from n in db.passengerDbSet where n.COMPID == comid && n.MOALLEMNM == changedtxt select n).ToList();

            foreach (var x in rt)
            {

                Add = x.MOALLEMADDR;
                MOB = x.MOALLEMMOBNO;







            }



            var result = new
            {
                address = Add,
                mobileno = MOB

            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
