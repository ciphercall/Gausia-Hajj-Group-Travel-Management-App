using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Travel.Models.Travel;

namespace Travel.Controllers.Travel
{
    public class PassCardController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;
        public PassCardController()
        {
            ViewData["Hightlight_valid_Travel_Form"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }


        public ASL_LOG aslLog = new ASL_LOG();

        public void Insert_Asl_LogData(TAMS_PSNGRCARD model)
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
            aslLog.TABLEID = "TAMS_PSNGRCARD";
            aslLog.LOGDATA =
                Convert.ToString("Card NoP:" + model.CARDNOP + ",\nCARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Update_Asl_LogData(TAMS_PSNGRCARD model)
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
            aslLog.TABLEID = "TAMS_PSNGRCARD";
            aslLog.LOGDATA =
                Convert.ToString("CARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Update_Asl_LogData2(TAMS_PSNGRCARD model)
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
            aslLog.TABLEID = "TAMS_PSNGRCARD";
            aslLog.LOGDATA =
                Convert.ToString("Passenger Name: " + model.PSGRNM + ",\nCARD PNO: " + model.CARDNOP + ",\nPermanent add: " + model.PERADDR + ",\nDistrict: " + model.PERDIST + ",\nPost Office: " + model.PERPO + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Delete_Asl_LogData(TAMS_PSNGRCARD model)
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
            aslLog.TABLEID = "TAMS_PSNGRCARD";
            aslLog.LOGDATA =
                Convert.ToString("CARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();


        public void Delete_ASL_DELETE(TAMS_PSNGRCARD model)
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
            AslDelete.TABLEID = "TAMS_PSNGRCARD";
            AslDelete.DELDATA = Convert.ToString("CARD Date: " + model.CARDDT + ",\nCARD NO: " + model.CARDNO + ",\nAgent Id: " + model.AGENTID + ",\nPassenger Name: " + model.PSGRNM + ",\nCARD ID: " + model.CARDCID + ".");
            AslDelete.USERPC = model.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }

        // GET: /Passenger/
        public ActionResult FirstPartCreate()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CardNo_Changed2(Int64 changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 Id = 0, CARDYY = 0, AGE = 0, agentID = 0, CARRIERID = 0, LICENSEID = 0, AGENTID=0;


            string changedtxt2 = Convert.ToString(changedtxt);

            string CARDDT = "", DOB = "", ISSUEDT = "", EXPIREDT = "", VNIDT = "", CARDNOP = "", CARDCID = "", PKGSTP = "",PSGRTP="";
            var rt = (from n in db.passengerDbSet where n.COMPID == comid && n.CARDNO == changedtxt2 select n).ToList();

            foreach (var x in rt)
            {
                Id = x.Id;

                CARDDT = Convert.ToString(x.CARDDT);
                CARDYY = Convert.ToInt64(x.CARDYY);


                CARDCID = x.CARDCID;
                PKGSTP = x.PKGSTP;
                LICENSEID = x.LICENSEID;
                PSGRTP = x.PSGRTP;
                AGENTID = x.AGENTID;
                CARRIERID = Convert.ToInt64(x.CARRIERID);
            }
            var maxcardNop = Convert.ToInt64((from n in db.passengerCardDbSet where n.COMPID == comid && n.CARDNO == changedtxt2 select n.CARDNOP).Max());
            if (maxcardNop == 0)
            {
                CARDNOP = changedtxt2 + "01";
            }
            else
            {
                CARDNOP = Convert.ToString(maxcardNop + 1);
            }


            var result = new
            {
                id = Id,

                carddt = CARDDT,
                Year = CARDYY,
                cardnop = CARDNOP,

                cardcid=CARDCID,
                pkgstp=PKGSTP,
                license=LICENSEID,
                psgrtp=PSGRTP,
                agentid=AGENTID,
                carrierid=CARRIERID


            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstPartCreate(HttpPostedFileBase file, TAMS_PSNGRCARD model)
        {
           


                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                model.USERPC = strHostName;
                model.INSIPNO = ipAddress.ToString();
                model.INSTIME = Convert.ToDateTime(td);
                model.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/PassCard_Images/"), model.CARDNOP + "_" + fileName);

                    model.CARDIDPIC = "/PassCard_Images/" + model.CARDNOP + "_" + fileName;

                    file.SaveAs(path);
                    ViewBag.UploadMessage = "Upload successfully done! ";
                }
                catch
                {
                    ViewBag.UploadMessage = "Image file is not in correct Format.";
                }

                var Cardid = (from n in db.passengerDbSet where n.COMPID == model.COMPID && n.CARDNO == model.CARDNO select new { n.CARDID }).ToList();
                foreach (var x in Cardid)
                {
                    model.CARDID = x.CARDID;
                }



                Insert_Asl_LogData(model);
                db.passengerCardDbSet.Add(model);





                db.SaveChanges();

                TempData["PassengerMessage"] = "Passenger Card Created ";

                return RedirectToAction("FirstPartCreate");
                //return RedirectToAction("SecondPartCreate");
          
        }

        public ActionResult SecondPartCreate()
        {
            var model = (TAMS_PSNGRCARD)TempData["FirstPartCreate_Model"];
            return View(model);
        }

        //AutoComplete
        public JsonResult TagSearch(string term)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());





            var tags = from p in db.passengerCardDbSet
                       where p.COMPID == compid
                       select p.CARDNOP;


            return this.Json(tags.Where(t => t.StartsWith(term)),
                       JsonRequestBehavior.AllowGet);



        }

        //AutoComplete 
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String itemId = "";

            // string changedText = Convert.ToString(changedText1);

            var rt = db.passengerCardDbSet.Where(n => n.CARDNOP.StartsWith(changedText) &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             costcnm = n.CARDNOP

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






            var result = new { Cardnop = itemId };
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
            var rt = from n in db.passengerCardDbSet where n.COMPID == comid && n.CARDNOP == changedtxt2 select n;


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SecondPartCreate(TAMS_PSNGRCARD model)//create and update this is
        {
            //var FirstPartCreate_Model = (PageModel)TempData["FirstPartCreate_Model"];
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];



            var searchid = (from n in db.passengerCardDbSet where n.COMPID == model.COMPID && n.Id == model.Id select n);
            foreach (TAMS_PSNGRCARD item in searchid)
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

         
            //  db.passengerDbSet.Add(model);
            db.SaveChanges();

            TempData["PassengerMessage"] = "Passenger Updated ";

            return RedirectToAction("SecondPartCreate");


        }


        public ActionResult FirstPartUpdate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FirstPartUpdate(HttpPostedFileBase file, TAMS_PSNGRCARD model)
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            var searchid = (from n in db.passengerCardDbSet where n.COMPID == model.COMPID && n.Id == model.Id select n);

            foreach (TAMS_PSNGRCARD item in searchid)
            {

                item.CARDDT = model.CARDDT;
                item.CARDYY = model.CARDYY;
                item.AGENTID = model.AGENTID;
                item.GENDER = model.GENDER;
                item.FATHERNM = model.FATHERNM;

                item.MOTHERNM = model.MOTHERNM;
                item.NATIONALITY = model.NATIONALITY;
                item.PROFESSION = model.PROFESSION;
                item.DOB = model.DOB;
               
                item.BIRTHPLACE = model.BIRTHPLACE;
                item.COUNTRY = model.COUNTRY;
                item.PASSPORTNO = model.PASSPORTNO;
                item.ISSUEDT = model.ISSUEDT;
                item.ISSUEPLACE = model.ISSUEPLACE;
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

                item.USERPC = strHostName;
                item.UPDIPNO = ipAddress.ToString();
                item.UPDTIME = Convert.ToDateTime(td);
                item.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                item.UPDLTUDE = model.INSLTUDE;


                item.PKGSTP = model.PKGSTP;
                item.LICENSEID = model.LICENSEID;
                item.PSGRTP = model.PSGRTP;
                item.BLOODG = model.BLOODG;
                item.EDUCATION = model.EDUCATION;
                item.MSTATUS = model.MSTATUS;
                item.DESIGNATION = model.DESIGNATION;
                item.MOBNO = model.MOBNO;
                item.TELNO = model.TELNO;
                item.NGNO = model.NGNO;
                item.PILGRIMID = model.PILGRIMID;
                item.NID = model.NID;
                item.PASSPORTTP = model.PASSPORTTP;

                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/PassCard_Images/"), model.CARDNOP + "_" + fileName);

                    item.CARDIDPIC = "/PassCard_Images/" + model.CARDNOP + "_" + fileName;

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

            db.SaveChanges();

            TempData["PassengerMessage"] = "Passenger's Card Updated ";

            return RedirectToAction("FirstPartUpdate");


        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CardNoP_Changed2(Int64 changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 Id = 0, CARDYY = 0, AGE = 0, agentID = 0, CARDCID = 0, CARRIERID = 0;
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
                PASSPORTNO = "", Agentname = "", gg = "", Carriername = "",
                txtPKGSTP = "", txtLICENSEID = "", txtpsgtype = "", txtBLOODG = "", txtMSTATUS = "", txtEDUCATION = "", txtDESIGNATION = "", txtMOBNO = "", txtTELNO="",
                txtNGNO = "", txtPILGRIMID = "", txtNID = "", txtPASSPORTTP = "", img = "";

            string changedtxt2 = Convert.ToString(changedtxt);

            string CARDDT = "", DOB = "", ISSUEDT = "", EXPIREDT = "", VNIDT = "", VNEDT = "";
            var rt = (from n in db.passengerCardDbSet where n.COMPID == comid && n.CARDNOP == changedtxt2 select n).ToList();
            // var agent=(from m in db.GlAcchartDbSet where m.COMPID == comid select m).ToList();
            // var carrier=(from v in db.carrierDbSet where v.COMPID == comid select v).ToList();
            foreach (var x in rt)
            {
                Id = x.Id;
                PSGRNM = x.PSGRNM;
                CARDDT = Convert.ToString(x.CARDDT);
                CARDYY = Convert.ToInt64(x.CARDYY);

                agentID = Convert.ToInt64(x.AGENTID);
                gg = x.GENDER;
                FATHERNM = x.FATHERNM;
                MOTHERNM = x.MOTHERNM;
                NATIONALITY = x.NATIONALITY;
                PROFESSION = x.PROFESSION;
                DOB = Convert.ToString(x.DOB);
               
                BIRTHPLACE = x.BIRTHPLACE;
                COUNTRY = x.COUNTRY;
                PASSPORTNO = x.PASSPORTNO;
                ISSUEDT = Convert.ToString(x.ISSUEDT);
                ISSUEPLACE = x.ISSUEPLACE;
                EXPIREDT = Convert.ToString(x.EXPIREDT);
                ROOT = x.ROUTE;

                CARRIERID = Convert.ToInt64(x.CARRIERID);
                VNTP = x.VNTP;
                VNNO = x.VNNO;
                VNIDT = Convert.ToString(x.VNIDT);
                VNEDT = Convert.ToString(x.VNEDT);
                VNIPLACE = x.VNIPLACE;
                REMARKS = x.REMARKS;
                CARDCID = Convert.ToInt64(x.CARDCID);

                txtPKGSTP = x.PKGSTP;
                txtLICENSEID = Convert.ToString(x.LICENSEID);
                txtpsgtype = x.PSGRTP;
                txtBLOODG = x.BLOODG;
                txtMSTATUS = x.MSTATUS;
                txtEDUCATION = x.EDUCATION;
                txtDESIGNATION = x.DESIGNATION;
                txtMOBNO = x.MOBNO;
                txtTELNO = x.TELNO;
                txtNGNO = Convert.ToString(x.NGNO);
                txtPILGRIMID = Convert.ToString(x.PILGRIMID);
                txtNID = x.NID;
                txtPASSPORTTP = x.PASSPORTTP;
                img = x.CARDIDPIC;
            }


            var result = new
            {
                id = Id,
                passenger = PSGRNM,
                carddt = CARDDT,
                Year = CARDYY,
                agentname = agentID,
                gender = gg,
                fathername = FATHERNM,
                mothername = MOTHERNM,
                nationality = NATIONALITY,
                profession = PROFESSION,
                dateofbirth = DOB,
                age = AGE,
                birthplace = BIRTHPLACE,
                country = COUNTRY,
                passportno = PASSPORTNO,
                issudt = ISSUEDT,
                issueplace = ISSUEPLACE,
                expiredt = EXPIREDT,
                root = ROOT,
                carriername = CARDCID,
                vntype = VNTP,
                vnno = VNNO,
                vnidt = VNIDT,
                vnedt = VNEDT,
                vniplace = VNIPLACE,
                remarks = REMARKS,
                cardcid = CARDCID,


                PKGSTP = txtPKGSTP,
                LICENSEID= txtLICENSEID,
               PSGRTP= txtpsgtype,
                 BLOODG=txtBLOODG,
                MSTATUS=txtMSTATUS,
                EDUCATION=txtEDUCATION,
                DESIGNATION=txtDESIGNATION,
                MOBNO = txtMOBNO,
                TELNO = txtTELNO,
                NGNO = txtNGNO,
                 PILGRIMID=txtPILGRIMID,
                NID = txtNID,
                PASSPORTTP = txtPASSPORTTP,
                IMG = img


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