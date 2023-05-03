using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Travel.Models.Travel;

namespace Travel.Controllers.Travel
{
    public class RPInfoController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public RPInfoController()
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
                     where n.COMPID == model.RPDRCR.COMPID && n.USERID == model.RPDRCR.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.RPDRCR.COMPID);
            aslLog.USERID = model.RPDRCR.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.RPDRCR.INSIPNO;
            aslLog.LOGLTUDE = model.RPDRCR.INSLTUDE;
            aslLog.TABLEID = "TAMS_RPDRCR";
            aslLog.LOGDATA =
                Convert.ToString("Trans Date: " + model.RPDRCR.TRANSDT + ",\nTrans NO: " + model.RPDRCR.TRANSNO + ",\nCard Id: " + model.RPDRCR.CARDID + ",\nTicket No: " + model.RPDRCR.TICKETNO + ",\nTicket Date: " + model.RPDRCR.TICKETDT + ".");
            aslLog.USERPC = model.RPDRCR.USERPC;
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
                      where n.COMPID == model.RPDRCR.COMPID && n.USERID == model.RPDRCR.UPDUSERID
                      select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.RPDRCR.COMPID);
            aslLog.USERID = model.RPDRCR.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.RPDRCR.UPDIPNO;
            aslLog.LOGLTUDE = model.RPDRCR.UPDLTUDE;
            aslLog.TABLEID = "TAMS_RPDRCR";
            aslLog.LOGDATA =
               Convert.ToString("Trans Date: " + model.RPDRCR.TRANSDT + ",\nTrans NO: " + model.RPDRCR.TRANSNO + ",\nCard Id: " + model.RPDRCR.CARDID + ",\nTicket No: " + model.RPDRCR.TICKETNO + ",\nTicket Date: " + model.RPDRCR.TICKETDT + ".");
            aslLog.USERPC = model.RPDRCR.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Delete_Asl_LogData(TAMS_RPDRCR model)
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
            aslLog.LOGTYPE = "Delete";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.UPDIPNO;
            aslLog.LOGLTUDE = model.UPDLTUDE;
            aslLog.TABLEID = "TAMS_RPDRCR";
            aslLog.LOGDATA =
               Convert.ToString("Trans Date: " + model.TRANSDT + ",\nTrans NO: " + model.TRANSNO + ",\nCard Id: " + model.CARDID + ",\nTicket No: " + model.TICKETNO + ",\nTicket Date: " + model.TICKETDT + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();


        public void Delete_ASL_DELETE(TAMS_RPDRCR model)
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
            AslDelete.TABLEID = "TAMS_RPDRCR";
            AslDelete.DELDATA = Convert.ToString("Trans Date: " + model.TRANSDT + ",\nTrans NO: " + model.TRANSNO + ",\nCard Id: " + model.CARDID + ",\nTicket No: " + model.TICKETNO + ",\nTicket Date: " + model.TICKETDT + ".");
            AslDelete.USERPC = model.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }

        // GET: /RPInfo/

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(PageModel model,string command)
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            if (command == "Create")
            {
                if (model.RPDRCR.TRANSFOR == "PAYABLE")
                {
                    model.RPDRCR.TICKETDT = model.RPDRCR.INSTIME;
                    model.RPDRCR.TICKETNO = model.RPDRCR.USERPC;
                }


                model.RPDRCR.USERPC = strHostName;
                model.RPDRCR.INSIPNO = ipAddress.ToString();
                model.RPDRCR.INSTIME = Convert.ToDateTime(td);
                model.RPDRCR.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                var cardnodata = from n in db.passengerDbSet
                                 where n.COMPID == model.RPDRCR.COMPID && n.CARDNO == model.passenger.CARDNO
                                 select new { n.CARDYY, n.CARDID };
                foreach (var l in cardnodata)
                {
                    model.RPDRCR.CARDID = Convert.ToInt64(l.CARDID);
                    model.RPDRCR.CARDYY = Convert.ToInt64(l.CARDYY);
                }



                //model.RPDRCR.ACCOUNTCD = Convert.ToInt64(model.AGL_acchart.ACCOUNTCD);

                var check_for_duplicate =
                                    (from n in db.RPDRCRDbSet
                                     where n.COMPID == model.RPDRCR.COMPID && n.TRANSNO == model.RPDRCR.TRANSNO
                                     select n).ToList();
                if (check_for_duplicate.Count == 0)
                {
                    model.RPDRCR.TRANSSL = Convert.ToInt64(Convert.ToString(model.RPDRCR.TRANSNO) + "01");
                    Insert_Asl_LogData(model);
                    db.RPDRCRDbSet.Add(model.RPDRCR);
                    if (model.RPDRCR.CARDID == 0)
                    {
                        TempData["RPMessage"] = "Select Card No";
                        TempData["RPInfo"] = model;

                        return RedirectToAction("Create");
                    }
                    else
                    {
                        db.SaveChanges();
                    }
                   
                    if (model.RPDRCR.TRANSFOR == "RECEIVABLE")
                    {
                        TempData["RPMessage"] = "Received From Card No : " + model.passenger.CARDNO + " Serial No : " +
                                                model.RPDRCR.TRANSNO + " Saved";
                    }
                    else
                    {
                        TempData["RPMessage"] = "Payable To Card No : " + model.passenger.CARDNO + " Serial No : " +
                                                model.RPDRCR.TRANSNO + " Saved";
                    }
                    //TempData["TransactionMessage"] = null;
                    TempData["RPInfo"] = model;

                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["RPMessage"] = "Try Again";
                }

            }

            if (command == "Create & Print")
            {
                var LoggedCompId = System.Web.HttpContext.Current.Session["loggedCompID"].ToString();
                var loggedUserID = System.Web.HttpContext.Current.Session["loggedUserID"].ToString();

                var createStatus = "";

                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());
                string query = string.Format("SELECT STATUS, INSERTR, UPDATER, DELETER FROM ASL_ROLE WHERE  CONTROLLERNAME='RPInfo' AND COMPID='{0}' AND USERID = '{1}'", LoggedCompId, loggedUserID);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                da.Fill(ds);

                foreach (DataRow row in ds.Rows)
                {
                    createStatus = row["INSERTR"].ToString();
                }

                conn.Close();

                if (createStatus == 'I'.ToString())
                {
                    //TempData["ShowAddButton"] = "Show Add Button";


                    TempData["message"] = "Permission not granted!";
                    return RedirectToAction("Create");
                }
                //...............................................................................................

                if (model.RPDRCR.TRANSFOR == "PAYABLE")
                {
                    model.RPDRCR.TICKETDT = model.RPDRCR.INSTIME;
                    model.RPDRCR.TICKETNO = model.RPDRCR.USERPC;
                }

                //Get Ip ADDRESS,Time & user PC Name


                model.RPDRCR.USERPC = strHostName;
                model.RPDRCR.INSIPNO = ipAddress.ToString();
                model.RPDRCR.INSTIME = Convert.ToDateTime(td);
                model.RPDRCR.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                var cardnodata = from n in db.passengerDbSet
                                 where n.COMPID == model.RPDRCR.COMPID && n.CARDNO == model.passenger.CARDNO
                                 select new { n.CARDYY, n.CARDID };
                foreach (var l in cardnodata)
                {
                    model.RPDRCR.CARDID = Convert.ToInt64(l.CARDID);
                    model.RPDRCR.CARDYY = Convert.ToInt64(l.CARDYY);
                }



                //model.RPDRCR.ACCOUNTCD = Convert.ToInt64(model.AGL_acchart.ACCOUNTCD);
                var check_for_duplicate =
                    (from n in db.RPDRCRDbSet
                        where n.COMPID == model.RPDRCR.COMPID && n.TRANSNO == model.RPDRCR.TRANSNO
                        select n).ToList();
                if (check_for_duplicate.Count == 0)
                {
                    model.RPDRCR.TRANSSL = Convert.ToInt64(Convert.ToString(model.RPDRCR.TRANSNO) + "01");
                    Insert_Asl_LogData(model);
                    db.RPDRCRDbSet.Add(model.RPDRCR);
                    if (model.RPDRCR.CARDID == 0)
                    {
                        TempData["RPMessage"] = "Select Card No";
                        TempData["RPInfo"] = model;

                        return RedirectToAction("Create");
                    }
                    else
                    {
                        db.SaveChanges();
                    }
                   
                    if (model.RPDRCR.TRANSFOR == "RECEIVABLE")
                    {
                        TempData["RPMessage"] = "Received From Card No : " + model.passenger.CARDNO + " Serial No : " +
                                                model.RPDRCR.TRANSNO + " Saved";
                    }
                    else
                    {
                        TempData["RPMessage"] = "Payable To Card No : " + model.passenger.CARDNO + " Serial No : " +
                                                model.RPDRCR.TRANSNO + " Saved";
                    }
                    //TempData["TransactionMessage"] = null;
                    TempData["RPInfo"] = model;

                    return RedirectToAction("VoucharReport");
                }
                else
                {
                    TempData["RPMessage"] = "Try Again";
                }

             

            }
          
        
        

          


            return RedirectToAction("Create");
        }


        public ActionResult VoucharReport()
        {

            PageModel model = (PageModel)TempData["RPInfo"];
            return View(model);
        }
       

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PageModel model)
        {

            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];




            var searchid = (from n in db.RPDRCRDbSet where n.COMPID == model.RPDRCR.COMPID && n.Id == model.RPDRCR.Id select n).ToList();
            //var costcid = (from n in db.GLCostPMSTDbSet
            //               where n.COMPID == model.RPDRCR.COMPID && n.COSTCNM == model.AGlCostPMST.COSTCNM
            //               select new { n.COSTCID }).ToList();

            var costpid = (from n in db.GlCostPoolDbSet
                           where n.COMPID == model.RPDRCR.COMPID && n.COSTPNM == model.AGlCostPool.COSTPNM
                           select new { n.COSTPID }).ToList();
            var accountcd = (from n in db.GlAcchartDbSet
                             where n.COMPID == model.RPDRCR.COMPID && n.ACCOUNTNM == model.AGL_acchart.ACCOUNTNM
                             select new { n.ACCOUNTCD }).ToList();

            foreach (TAMS_RPDRCR item in searchid)
            {

                //foreach (var l in costcid)
                //{
                //   item.CARDCID = Convert.ToInt64(l.COSTCID);
                //}

                item.CARDCID = model.RPDRCR.CARDCID;
                foreach (var x in costpid)
                {
                    item.CARDPID = Convert.ToInt64(x.COSTPID);
                }


                foreach (var x in accountcd)
                {
                    item.ACCOUNTCD = Convert.ToInt64(x.ACCOUNTCD);
                }


                item.TICKETNO = model.RPDRCR.TICKETNO;
                item.TICKETDT = model.RPDRCR.TICKETDT;
                item.AMOUNT = model.RPDRCR.AMOUNT;
                item.AMTCASH = model.RPDRCR.AMTCASH;
                item.AMTCREDIT = model.RPDRCR.AMTCREDIT;
                item.REMARKS = model.RPDRCR.REMARKS;
                item.TRANSSL = item.TRANSSL;




                item.USERPC = strHostName;
                item.UPDIPNO = ipAddress.ToString();
                item.UPDTIME = Convert.ToDateTime(td);
                item.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                item.UPDLTUDE = model.RPDRCR.INSLTUDE;
            }

            model.RPDRCR.USERPC = strHostName;
            model.RPDRCR.UPDIPNO = ipAddress.ToString();
            model.RPDRCR.UPDTIME = Convert.ToDateTime(td);
            model.RPDRCR.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            model.RPDRCR.UPDLTUDE = model.RPDRCR.INSLTUDE;

            Update_Asl_LogData(model);

            db.SaveChanges();

            if (model.RPDRCR.TRANSFOR == "RECEIVABLE")
            {
                TempData["RPMessage"] = "Received From Card No : " + model.passenger.CARDNO + " Serial No : " + model.RPDRCR.TRANSNO + " Updated";
            }
            else
            {
                TempData["RPMessage"] = "Payable To Card No : " + model.passenger.CARDNO + " Serial No : " + model.RPDRCR.TRANSNO + " Updated";
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
            TAMS_RPDRCR aRpdrcr = db.RPDRCRDbSet.Find(model.RPDRCR.Id);



            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];


            aRpdrcr.USERPC = strHostName;
            aRpdrcr.UPDIPNO = ipAddress.ToString();
            aRpdrcr.UPDTIME = Convert.ToDateTime(td);
            aRpdrcr.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            aRpdrcr.UPDLTUDE = aRpdrcr.INSLTUDE;

            Delete_Asl_LogData(aRpdrcr);
            Delete_ASL_DELETE(aRpdrcr);

            db.RPDRCRDbSet.Remove(aRpdrcr);
            db.SaveChanges();

            if (model.RPDRCR.TRANSFOR == "RECEIVABLE")
            {
                TempData["RPMessage"] = "Received From Card No : " + model.passenger.CARDNO + " Serial No : " + model.RPDRCR.TRANSNO + " Deleted";
            }
            else
            {
                TempData["RPMessage"] = "Payable To Card No : " + model.passenger.CARDNO + " Serial No : " + model.RPDRCR.TRANSNO + " Deleted";
            }


            return RedirectToAction("Delete");
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


        //AutoComplete
        public JsonResult TagSearch(string term)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());





            var tags = from p in db.GLCostPMSTDbSet
                       where p.COMPID == compid
                       select p.COSTCNM;


            return this.Json(tags.Where(t => t.StartsWith(term)),
                       JsonRequestBehavior.AllowGet);



        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String itemId = "";

            // string changedText = Convert.ToString(changedText1);

            var rt = db.GLCostPMSTDbSet.Where(n => n.COSTCNM.StartsWith(changedText) &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             costcnm = n.COSTCNM

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






            var result = new { CostCNM = itemId };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        //AutoComplete
        public JsonResult TagSearch2(string query, Int64 query2)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());


            //Int64 costcid = Convert.ToInt64(Convert.ToString(compid) + "001");


            var tags = from p in db.GlCostPoolDbSet
                       where p.COMPID == compid && p.COSTCID == query2
                       select p.COSTPNM;


            return this.Json(tags.Where(t => t.StartsWith(query)),
                       JsonRequestBehavior.AllowGet);



        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword2(string changedText, Int64 changedText2)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String itemId = "";

            // string changedText = Convert.ToString(changedText1);
          
            var rt = db.GlCostPoolDbSet.Where(n => n.COSTPNM.StartsWith(changedText)&& n.COSTCID==changedText2 &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             costcnm = n.COSTPNM

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






            var result = new { CostPNM = itemId };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        //AutoComplete
        public JsonResult TagSearch3(string query)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());





            var tags = from p in db.passengerDbSet
                where p.COMPID == compid
                select new {p.CARDNO, p.PSGRNM};


            return this.Json(tags.Where(t => t.CARDNO.StartsWith(query)),
                       JsonRequestBehavior.AllowGet);



        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword3(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
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

            string psgrnm = "";
            var rt2 = db.passengerDbSet.Where(n => n.CARDNO == itemId && n.COMPID == compid).Select(n => new
            {
                name = n.PSGRNM
              

            });
            foreach (var n in rt2)
            {
                psgrnm = n.name;


            }



            var result = new { Cardno = itemId, PSGRNM = psgrnm };
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public JsonResult TagSearch4(string term)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());





            var tags = from p in db.GlAcchartDbSet
                       where p.COMPID == compid
                       select p.ACCOUNTNM;


            return this.Json(tags.Where(t => t.StartsWith(term)),
                       JsonRequestBehavior.AllowGet);



        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword4(string changedText)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
            String itemId = "";

            // string changedText = Convert.ToString(changedText1);

            var rt = db.GlAcchartDbSet.Where(n => n.ACCOUNTNM.StartsWith(changedText) &&
                                                         n.COMPID == compid).Select(n => new
                                                         {
                                                             costcnm = n.ACCOUNTNM

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






            var result = new { Accountnm = itemId };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Serialload(string type, DateTime type2, string type3)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> transno = new List<SelectListItem>();
            string company = Convert.ToString(comid);
            string CARDID = company + "1" + type;
            Int64 cardid = Convert.ToInt64(CARDID);

            var ans = from n in db.RPDRCRDbSet where n.COMPID == comid && n.CARDID == cardid && n.TRANSFOR == type3 && n.TRANSDT == type2 select new { n.TRANSNO };
            foreach (var x in ans)
            {
                transno.Add(new SelectListItem { Text = Convert.ToString(x.TRANSNO), Value = Convert.ToString(x.TRANSNO) });
            }






            return Json(new SelectList(transno, "Value", "Text"));

        }

        public JsonResult Serialload2(DateTime type, string type2)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> transno = new List<SelectListItem>();
            string company = Convert.ToString(comid);
            //string CARDID = company + "1" + type;
            //Int64 cardid = Convert.ToInt64(CARDID);

            var ans = (from n in db.RPDRCRDbSet where n.COMPID == comid && n.TRANSFOR == type2 && n.TRANSDT == type select new { n.TRANSNO }).Distinct();
            foreach (var x in ans)
            {
                transno.Add(new SelectListItem { Text = Convert.ToString(x.TRANSNO), Value = Convert.ToString(x.TRANSNO) });
            }






            return Json(new SelectList(transno, "Value", "Text"));

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Update_SelectTransNo(Int64 changedtxt)
        {

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            //string company = Convert.ToString(compid);
            //string CARDID = company + "1" + changedtxt;
            //Int64 cardid = Convert.ToInt64(CARDID);

            var selectdata = (from n in db.RPDRCRDbSet where n.COMPID == compid && n.TRANSNO == changedtxt select new { n.Id, n.CARDCID, n.CARDPID, n.ACCOUNTCD, n.TICKETNO, n.TICKETDT, n.AMOUNT, n.AMTCASH, n.AMTCREDIT, n.REMARKS, n.TRANSMY }).ToList();
            var selectcostpoolhead =
                (from n in db.GLCostPMSTDbSet where n.COMPID == compid select new { n.COSTCID, n.COSTCNM }).ToList();
            var selectcostpool =
                (from n in db.GlCostPoolDbSet where n.COMPID == compid select new { n.COSTPNM, n.COSTPID }).ToList();
            var accountname =
                (from n in db.GlAcchartDbSet where n.COMPID == compid select new { n.ACCOUNTNM, n.ACCOUNTCD }).ToList();

            string transmy = "", ticketno = "", ticketdt = "", remarks = "", cardcid = "", cardpid = "", accountcd = "";

            Int64 primaryid = 0, cardcidvalue = 0;
            decimal amount = 0, amtcash = 0, amtcredit = 0;
            foreach (var l in selectdata)
            {
                primaryid = Convert.ToInt64(l.Id);


                transmy = Convert.ToString(l.TRANSMY);
                foreach (var x in selectcostpoolhead)
                {
                    if (x.COSTCID == l.CARDCID)
                    {
                        cardcid = Convert.ToString(x.COSTCNM);
                        cardcidvalue = Convert.ToInt64(x.COSTCID);
                        break;
                    }

                }
                foreach (var y in selectcostpool)
                {
                    if (y.COSTPID == l.CARDPID)
                    {
                        cardpid = Convert.ToString(y.COSTPNM);
                        break;
                    }

                }
                foreach (var z in accountname)
                {
                    if (z.ACCOUNTCD == l.ACCOUNTCD)
                    {
                        accountcd = Convert.ToString(z.ACCOUNTNM);
                        break;
                    }

                }


                ticketno = Convert.ToString(l.TICKETNO);

                ticketdt = Convert.ToString(l.TICKETDT);
                amount = Convert.ToDecimal(l.AMOUNT);
                amtcash = Convert.ToDecimal(l.AMTCASH);
                amtcredit = Convert.ToDecimal(l.AMTCREDIT);
                remarks = Convert.ToString(l.REMARKS);

            }


            var result = new { keyid = primaryid, Month = transmy, CardType = cardcid, Cardcid = cardcidvalue, EffectHead = cardpid, AccountName = accountcd, TicketNo = ticketno, TicketDate = ticketdt, Amount = amount, CashAmount = amtcash, CreditAmount = amtcredit, Remarks = remarks };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cardpidload(string type)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> listcostpname = new List<SelectListItem>();

          Int64 Type = Convert.ToInt64(type);



                var ans1 = from n in db.GlCostPoolDbSet where n.COMPID == comid && n.COSTCID==Type select new { n.COSTPNM, n.COSTPID };
                foreach (var x in ans1)
                {
                    listcostpname.Add(new SelectListItem { Text = x.COSTPNM, Value = Convert.ToString(x.COSTPID) });
                }
           return Json(new SelectList(listcostpname, "Value", "Text"));

        }

        public JsonResult AccountLoad(string type, string type2)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
           
            List<SelectListItem> listaccountname = new List<SelectListItem>();


            if (type2 == "RECEIVABLE")
            {
                var ans = (from n in db.passengerDbSet where n.COMPID == comid && n.CARDNO == type select new { n.AGENTID, n.PSGRNM }).ToList();
                var account =
                    (from n in db.GlAcchartDbSet where n.COMPID == comid select new { n.ACCOUNTNM, n.ACCOUNTCD }).ToList();
                foreach (var x in ans)
                {
                    if (x.AGENTID.ToString().Substring(3, 7) == "1020202")
                    {
                        foreach (var item in account)
                        {
                            if (item.ACCOUNTCD.ToString().Substring(8, 7) == type)
                            {
                                listaccountname.Add(new SelectListItem { Text = Convert.ToString(type + "-" + x.PSGRNM), Value = Convert.ToString(comid + "10202" + type) });
                                break;
                            }
                        
                       
                        }
                    }
                    else
                    {
                        foreach (var item in account)
                        {
                            if (item.ACCOUNTCD == x.AGENTID)
                            {
                                listaccountname.Add(new SelectListItem { Text = item.ACCOUNTNM, Value = Convert.ToString(item.ACCOUNTCD) });
                                break;
                            }
                        }

                    }

                }


            }
            else
            {
                var account = (from n in db.GlAcchartDbSet where n.COMPID == comid select new { n.ACCOUNTNM, n.ACCOUNTCD }).ToList();
                foreach (var item in account)
                {
                    if (item.ACCOUNTCD.ToString().Substring(3, 5) == "20201" || item.ACCOUNTCD.ToString().Substring(3, 5) == "10202")
                    {
                        listaccountname.Add(new SelectListItem { Text = item.ACCOUNTNM, Value = Convert.ToString(item.ACCOUNTCD) });
                    }



                }

            }










            return Json(new SelectList(listaccountname, "Value", "Text"));

        }



        public JsonResult TicketLoad(string type, string type2)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> ticket = new List<SelectListItem>();
            var cardid_find=(from n in db.passengerDbSet where n.COMPID==comid && n.CARDNO==type select n).ToList();
            Int64 cardid=0;
            foreach(var ss in cardid_find)
            {
                cardid=Convert.ToInt64(ss.CARDID);
            }

            var find_ticket = (from n in db.RPDRCRDbSet where n.COMPID == comid && n.TRANSFOR == "PAYABLE" && n.CARDID == cardid select n).Distinct().ToList();
            foreach (var TravelRpdrcr in find_ticket)
            {
                if (TravelRpdrcr.TICKETNO != null)
                {
                    ticket.Add(new SelectListItem { Text = TravelRpdrcr.TICKETNO, Value = TravelRpdrcr.TICKETNO });
                }

            }









            return Json(new SelectList(ticket, "Value", "Text"));

        }



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CardNo_Changed(string changedtxt)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            Int64 Year = 0;
            decimal pkgamt = 0, mllfee = 0;
            string Passengername = "", permanentadd = "", perpo = "", perpc = "", perps = "", perdist = "", preadd = "", prepo = "",
                prepc = "", preps = "", predist = "", Blood = "", gardian = "", gardianadd = "", gardianrel = "", nomineename = "",
                nomineeadd = "", nomineerel = "", pkgstp = "";
            //string changedtxt2 = Convert.ToString(changedtxt);
            var rt = from n in db.passengerDbSet where n.COMPID == comid && n.CARDNO == changedtxt select n;
            foreach (var x in rt)
            {
              
                Passengername = x.PSGRNM;
                Year = Convert.ToInt64(x.CARDYY);
            }


            var result = new
            {
               
                name = Passengername,
               year=Year
            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult TicketNoLoad(string COMPID)
        {
            Int64 comid = Convert.ToInt64(COMPID);
           




            //DateTime datetm = Convert.ToDateTime(dd);

            List<SelectListItem> ticketno = new List<SelectListItem>();



            var ticket = (from n in db.RPDRCRDbSet
                            where n.COMPID == comid && n.TRANSFOR == "PAYABLE"
                            select new
                            {
                                n.TICKETNO
                            }
                            )
                            .Distinct()
                            .ToList();

            
            foreach (var f in ticket)
            {
                if (f.TICKETNO != null)
                {
                    ticketno.Add(new SelectListItem { Text = f.TICKETNO, Value = f.TICKETNO });
                }
                
            }

      
          
            return Json(new SelectList(ticketno, "Value", "Text"));
          
        }
        public JsonResult TicketDate(string query)
        {
            var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());





            var search_date = from p in db.RPDRCRDbSet
                              where p.COMPID == compid && p.TICKETNO == query
                       select new { p.TICKETDT};
            string dd = "";
            foreach (var rpdata in search_date)
            {
                DateTime aa = Convert.ToDateTime(rpdata.TICKETDT);
                dd = aa.ToString("dd-MMM-yyyy");

            }
            return this.Json(dd,
                       JsonRequestBehavior.AllowGet);



        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
