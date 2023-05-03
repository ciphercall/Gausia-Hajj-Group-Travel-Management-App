using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Travel.Models.Travel;

namespace Travel.Controllers
{
    public class RefundController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;


        public RefundController()
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
                     where n.COMPID == model.refund.COMPID && n.USERID == model.refund.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.refund.COMPID);
            aslLog.USERID = model.refund.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.refund.INSIPNO;
            aslLog.LOGLTUDE = model.refund.INSLTUDE;
            aslLog.TABLEID = "TAMS_REFUND";
            aslLog.LOGDATA =
                Convert.ToString("Trans Date: " + model.refund.TRANSDT + ",\nTrans NO: " + model.refund.TRANSNO + ",\nCost Pool Head: " + model.refund.CARDCID + ",\nAccount CD: " + model.refund.ACCOUNTCD + ",\nCARD ID: " + model.refund.CARDID + ".");
            aslLog.USERPC = model.refund.USERPC;
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
                     where n.COMPID == model.refund.COMPID && n.USERID == model.refund.UPDUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.refund.COMPID);
            aslLog.USERID = model.refund.UPDUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.refund.UPDIPNO;
            aslLog.LOGLTUDE = model.refund.UPDLTUDE;
            aslLog.TABLEID = "TAMS_REFUND";
            aslLog.LOGDATA =
                Convert.ToString("Trans Date: " + model.refund.TRANSDT + ",\nTrans NO: " + model.refund.TRANSNO + ",\nCost Pool Head: " + model.refund.CARDCID + ",\nAccount CD: " + model.refund.ACCOUNTCD + ",\nCARD ID: " + model.refund.CARDID + ".");
            aslLog.USERPC = model.refund.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public void Delete_Asl_LogData(TAMS_REFUND model)
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
            aslLog.USERID = model.UPDUSERID;
            aslLog.LOGTYPE = "Delete";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.UPDIPNO;
            aslLog.LOGLTUDE = model.UPDLTUDE;
            aslLog.TABLEID = "TAMS_REFUND";
            aslLog.LOGDATA =
                Convert.ToString("Trans Date: " + model.TRANSDT + ",\nTrans NO: " + model.TRANSNO + ",\nCost Pool Head: " + model.CARDCID + ",\nAccount CD: " + model.ACCOUNTCD + ",\nCARD ID: " + model.CARDID + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }


        public ASL_DELETE AslDelete = new ASL_DELETE();


        public void Delete_ASL_DELETE(TAMS_REFUND model)
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
            AslDelete.TABLEID = "TAMS_REFUND";
            AslDelete.DELDATA = Convert.ToString("Trans Date: " + model.TRANSDT + ",\nTrans NO: " + model.TRANSNO + ",\nCost Pool Head: " + model.CARDCID + ",\nAccount CD: " + model.ACCOUNTCD + ",\nCARD ID: " + model.CARDID + ".");
            AslDelete.USERPC = model.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }

        // GET: /Refund/

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(PageModel model)
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];


            model.refund.USERPC = strHostName;
            model.refund.INSIPNO = ipAddress.ToString();
            model.refund.INSTIME = Convert.ToDateTime(td);
            model.refund.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

            var costcid = (from n in db.GLCostPMSTDbSet
                           where n.COMPID == model.refund.COMPID && n.COSTCNM == model.AGlCostPMST.COSTCNM
                           select new { n.COSTCID }).ToList();
            foreach (var l in costcid)
            {
                model.refund.CARDCID = Convert.ToInt64(l.COSTCID);
            }

            var costpid = (from n in db.GlCostPoolDbSet
                           where n.COMPID == model.refund.COMPID && n.COSTPNM == model.AGlCostPool.COSTPNM
                           select new { n.COSTPID });
            foreach (var x in costpid)
            {
                model.refund.CARDPID = Convert.ToInt64(x.COSTPID);
            }

            var cardnodata = from n in db.passengerDbSet
                             where n.COMPID == model.refund.COMPID && n.CARDNO == model.passenger.CARDNO
                             select new { n.CARDYY, n.CARDID };
            foreach (var l in cardnodata)
            {
                model.refund.CARDID = Convert.ToString(l.CARDID);
                model.refund.CARDYY = Convert.ToInt64(l.CARDYY);
            }


            var accountcd = from n in db.GlAcchartDbSet
                            where n.COMPID == model.refund.COMPID && n.ACCOUNTNM == model.AGL_acchart.ACCOUNTNM
                            select new { n.ACCOUNTCD };
            foreach (var x in accountcd)
            {
                model.refund.ACCOUNTCD = Convert.ToInt64(x.ACCOUNTCD);
            }

            Insert_Asl_LogData(model);
            db.refundDbSet.Add(model.refund);
            db.SaveChanges();

            TempData["RefundMessage"] = "Refund Inserted ";

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

            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];




            var searchid = (from n in db.refundDbSet where n.COMPID == model.refund.COMPID && n.Id == model.refund.Id select n).ToList();

           

            foreach (TAMS_REFUND item in searchid)
            {

                
                item.REFUNDS = model.refund.REFUNDS;
                item.REFUNDT = model.refund.REFUNDT;
                item.DEDAMTS = model.refund.DEDAMTS;
                item.DEDAMTT = model.refund.DEDAMTT;
              
                item.REMARKS = model.refund.REMARKS;





                item.USERPC = strHostName;
                item.UPDIPNO = ipAddress.ToString();
                item.UPDTIME = Convert.ToDateTime(td);
                item.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                item.UPDLTUDE = model.refund.INSLTUDE;
            }

            model.refund.USERPC = strHostName;
            model.refund.UPDIPNO = ipAddress.ToString();
            model.refund.UPDTIME = Convert.ToDateTime(td);
            model.refund.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            model.refund.UPDLTUDE = model.refund.INSLTUDE;

            Update_Asl_LogData(model);

            db.SaveChanges();

            TempData["RefundMessage"] = "Refund Updated ";

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
            TAMS_REFUND aRefund = db.refundDbSet.Find(model.refund.Id);



            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];


            aRefund.USERPC = strHostName;
            aRefund.UPDIPNO = ipAddress.ToString();
            aRefund.UPDTIME = Convert.ToDateTime(td);
            aRefund.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
            aRefund.UPDLTUDE = aRefund.INSLTUDE;

            Delete_Asl_LogData(aRefund);
            Delete_ASL_DELETE(aRefund);

            db.refundDbSet.Remove(aRefund);
            db.SaveChanges();

            TempData["RefundMessage"] = "'" + aRefund.TRANSNO + "' successfully Deleted";


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

            var query = from n in db.refundDbSet where n.COMPID == comid select new { n.TRANSNO };

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

     
        public JsonResult Cardpid_changedCardNoLoad(string type,string type2)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
           // DateTime ddt = Convert.ToDateTime(type3);

            List<SelectListItem> cardno = new List<SelectListItem>();
          
            Int64 COSTPID = 0,COSTCID=0;

            var costpidfind =
                (from n in db.GlCostPoolDbSet where n.COMPID == comid && n.COSTPNM == type select new {n.COSTPID})
                    .ToList();
            foreach (var x in costpidfind)
            {
                COSTPID = Convert.ToInt64(x.COSTPID);
            }
            var costheadfind =
                (from n in db.GLCostPMSTDbSet where n.COMPID == comid && n.COSTCNM == type2 select new {n.COSTCID})
                    .ToList();
            foreach (var x in costheadfind)
            {
                COSTCID = Convert.ToInt64(x.COSTCID);
            }

            var ans = (from n in db.RPDRCRDbSet where n.COMPID == comid && n.CARDPID == COSTPID && n.CARDCID==COSTCID && n.TRANSFOR=="PAYABLE" select new { n.CARDID }).Distinct().OrderBy(x=>x.CARDID).ToList();
            foreach (var x in ans)
            {
                cardno.Add(new SelectListItem { Text = Convert.ToString(x.CARDID).Substring(8,7), Value = Convert.ToString(x.CARDID).Substring(8,7) });
            }






            return Json(new SelectList(cardno, "Value", "Text"));

        }


        public JsonResult TicketLoad(string type,string type2)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> ticketno = new List<SelectListItem>();
            string company = Convert.ToString(comid);
            string CARDID = company + "10202" + type;
           Int64 cardid = Convert.ToInt64(CARDID);
           Int64 CARDPID = 0;
           var Cardpidfind =
               (from n in db.GlCostPoolDbSet where n.COMPID == comid && n.COSTPNM == type2 select new { n.COSTPID })
                   .ToList();
           foreach (var x in Cardpidfind)
           {
               CARDPID = Convert.ToInt64(x.COSTPID);
           }



           var ans = (from n in db.RPDRCRDbSet where n.COMPID == comid && n.CARDID == cardid && n.TRANSFOR == "PAYABLE" && n.CARDPID == CARDPID select new { n.TICKETNO }).ToList();
            foreach (var x in ans)
            {
                ticketno.Add(new SelectListItem { Text = Convert.ToString(x.TICKETNO), Value = Convert.ToString(x.TICKETNO) });
            }






            return Json(new SelectList(ticketno, "Value", "Text"));

        }


        public JsonResult TransNoLoad(DateTime type)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> transno = new List<SelectListItem>();


          //  DateTime dd = Convert.ToDateTime(type);

            var ans = (from n in db.refundDbSet where n.COMPID == comid && n.TRANSDT == type select new { n.TRANSNO }).ToList();

            foreach (var x in ans)
            {
                transno.Add(new SelectListItem { Text = Convert.ToString(x.TRANSNO), Value = Convert.ToString(x.TRANSNO) });
            }






            return Json(new SelectList(transno, "Value", "Text"));

        }



         [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult getdate_amount(string changedtxt,string changedtxt2,string changedtxt3,string passenger)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            string company = Convert.ToString(comid);
            string CARDID = company + "10202" + changedtxt2;
            Int64 cardid = Convert.ToInt64(CARDID);

            Int64 CARDPID = 0;
            var costpidfind =
                (from n in db.GlCostPoolDbSet where n.COMPID == comid && n.COSTPNM == changedtxt3 select new { n.COSTPID })
                    .ToList();
            foreach (var x in costpidfind)
            {
                CARDPID = Convert.ToInt64(x.COSTPID);
            }

            var ans1 = (from n in db.RPDRCRDbSet where n.COMPID == comid && n.CARDID == cardid && n.TRANSFOR == "PAYABLE" && n.CARDPID == CARDPID
                            && n.TICKETNO==changedtxt
                        select new { n.TICKETDT,n.AMOUNT,n.ACCOUNTCD }).ToList();

             string DDate = "";
             Int64 Accountcd = 0;
             decimal sumpayable = 0,sumreceivable=0;
             foreach (var x in ans1)
             {
                 sumpayable = Convert.ToDecimal(sumpayable + x.AMOUNT);
                 DDate = Convert.ToString(x.TICKETDT);
                 Accountcd = Convert.ToInt64(x.ACCOUNTCD);
             }

             var ans2 = (from n in db.RPDRCRDbSet
                         where n.COMPID == comid && n.CARDID == cardid && n.TRANSFOR == "RECEIVABLE" && n.CARDPID == CARDPID
                             && n.TICKETNO == changedtxt
                         select new { n.TICKETDT, n.AMOUNT }).ToList();
             foreach (var x in ans2)
             {
                 sumreceivable = Convert.ToDecimal(sumreceivable + x.AMOUNT);
             }

             var accountname =
                 (from n in db.GlAcchartDbSet
                     where n.COMPID == comid && n.ACCOUNTCD == Accountcd
                     select new {n.ACCOUNTNM}).ToList();
             string name = "";
             foreach (var x in accountname)
             {
                 name = Convert.ToString(x.ACCOUNTNM);
             }


             var agentfind =
                 (from n in db.RPDRCRDbSet where n.COMPID == comid && n.CARDID == cardid && n.TICKETNO==changedtxt && n.TRANSFOR=="RECEIVABLE" select new { n.ACCOUNTCD })
                     .ToList();
             var agg =
                 (from m in db.GlAcchartDbSet
                     where m.COMPID == comid && m.HEADTP == 1
                     select new {m.ACCOUNTCD, m.ACCOUNTNM}).ToList();
             Int64 aggent = 0;
             string receivable_name = "";
             foreach (var x in agentfind)
             {
                
                 
                     foreach (var item in agg)
                     {
                         if (item.ACCOUNTCD == x.ACCOUNTCD)
                         {
                             receivable_name = item.ACCOUNTNM;
                             break;
                         }
                         else
                         {
                             receivable_name = passenger;
                         }
                     }
                
                 
             }
             if (agentfind.Count == 0)
             {
                 receivable_name = passenger;
             }

             var result = new { date = DDate, buy = sumpayable, sale = sumreceivable,AccountName=name,ReceivableHead=receivable_name };
            return Json(result, JsonRequestBehavior.AllowGet);
        }



         [AcceptVerbs(HttpVerbs.Get)]
         public JsonResult keyword(string changedText)
         {
             var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
             String itemName = "";

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
                     itemName = changedText.Substring(0, lenChangedtxt);
                 }

                 else
                 {
                     itemName = changedText.Substring(0, lenChangedtxt - 1);
                 }



             }
             else
             {
                 itemName = changedText;
             }




             //var rt2 = db.GLCostPMSTDbSet.Where(n => n.COSTCNM==changedText &&
             //                                             n.COMPID == compid).Select(n => new
             //                                             {
             //                                                 costcnm = n.COSTCNM,
             //                                                 costcid=n.COSTCID

             //                                             }).ToList();
             //Int64 id = 0;
             //foreach (var itemid in rt2)
             //{
             //    id = Convert.ToInt64(itemid.costcid);
             //}

             var result = new { CostCNM = itemName};
             return Json(result, JsonRequestBehavior.AllowGet);

         }


         [AcceptVerbs(HttpVerbs.Get)]
         public JsonResult keyword2(string changedText)
         {
             var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
             String itemName = "";

              Int64 costcid = Convert.ToInt64(Convert.ToString(compid) + "001");

             var rt = db.GlCostPoolDbSet.Where(n => n.COSTPNM.StartsWith(changedText) &&
                                                          n.COMPID == compid && n.COSTCID == costcid).Select(n => new
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
                     itemName = changedText.Substring(0, lenChangedtxt);
                 }

                 else
                 {
                     itemName = changedText.Substring(0, lenChangedtxt - 1);
                 }



             }
             else
             {
                 itemName = changedText;
             }




             //var rt2 = db.GlCostPoolDbSet.Where(n => n.COSTPNM==changedText &&
             //                                            n.COMPID == compid).Select(n => new
             //                                            {
             //                                                costcnm = n.COSTPNM,
             //                                                costpid=n.COSTPID

             //                                            }).ToList();
             //Int64 costpid = 0;
             //foreach (var itemid in rt2)
             //{
             //    costpid = Convert.ToInt64(itemid.costpid);
             //}

             var result = new { CostPNM = itemName };
             return Json(result, JsonRequestBehavior.AllowGet);

         }
         [AcceptVerbs(HttpVerbs.Get)]
         public JsonResult keyword4(string changedText)
         {
             var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());
             String itemName = "";

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
                     itemName = changedText.Substring(0, lenChangedtxt);
                 }

                 else
                 {
                     itemName = changedText.Substring(0, lenChangedtxt - 1);
                 }



             }
             else
             {
                 itemName = changedText;
             }


            



             var result = new { Accountnm = itemName };
             return Json(result, JsonRequestBehavior.AllowGet);

         }


         [AcceptVerbs(HttpVerbs.Get)]
         public JsonResult Update_SelectTransNo(Int64 changedtxt)
         {

             Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            
             var selectdata = (from n in db.refundDbSet where n.COMPID == compid && n.TRANSNO == changedtxt select new { n.Id,n.CARDID, n.CARDCID, n.CARDPID, n.ACCOUNTCD, n.TICKETNO, n.TICKETDT, n.BUYAMTS, n.SALAMTT, n.REFUNDS, n.REFUNDT,n.DEDAMTS,n.DEDAMTT,n.REMARKS }).ToList();
             
             var selectcostpoolhead =
                 (from n in db.GLCostPMSTDbSet where n.COMPID == compid select new { n.COSTCID, n.COSTCNM }).ToList();
             var selectcostpool =
                 (from n in db.GlCostPoolDbSet where n.COMPID == compid select new { n.COSTPNM, n.COSTPID }).ToList();
             var accountname =
                 (from n in db.GlAcchartDbSet where n.COMPID == compid select new { n.ACCOUNTNM, n.ACCOUNTCD }).ToList();
             var passenger =
                 (from n in db.passengerDbSet where n.COMPID == compid select new {n.CARDID, n.PSGRNM}).ToList();

             string ticketno = "", ticketdt = "", remarks = "", cardcid = "", cardpid = "", accountcd = "",Cardno="",psgrm="";

             Int64 primaryid = 0;
             decimal buyamount = 0, saleamount = 0, refunds = 0, refundsT = 0, deductionS = 0, deductionT=0;
             foreach (var l in selectdata)
             {
                 primaryid = Convert.ToInt64(l.Id);

                 Cardno = Convert.ToString(l.CARDID).Substring(4, 6);
                 foreach (var x in passenger)
                 {
                     if (x.CARDID == l.CARDID)
                     {
                         psgrm = Convert.ToString(x.PSGRNM);
                         break;
                     }
                     
                 }
                 foreach (var x in selectcostpoolhead)
                 {
                     if (x.COSTCID == l.CARDCID)
                     {
                         cardcid = Convert.ToString(x.COSTCNM);
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
                 buyamount = Convert.ToDecimal(l.BUYAMTS);
                 saleamount = Convert.ToDecimal(l.SALAMTT);
                refunds = Convert.ToDecimal(l.REFUNDS);
                 refundsT = Convert.ToDecimal(l.REFUNDT);
                 deductionS = Convert.ToDecimal(l.DEDAMTS);
                 deductionT = Convert.ToDecimal(l.DEDAMTT);
                 remarks = Convert.ToString(l.REMARKS);

             }


             var result = new { keyid = primaryid, costhead = cardcid, accountname = accountcd, effecthead = cardpid, b = Cardno, passengername = psgrm, a = ticketno, ticketdate = ticketdt, buyamount = buyamount, saleamount = saleamount, refunds = refunds, refundst = refundsT, deductions = deductionS, deductiont = deductionT, Remarks = remarks };

             return Json(result, JsonRequestBehavior.AllowGet);
         }

         public JsonResult TagSearch(string term)
         {
             var compid = Convert.ToInt16(System.Web.HttpContext.Current.Session["loggedCompID"].ToString());




             Int64 coscid = Convert.ToInt64(Convert.ToString(compid) + "001");

             var tags = from p in db.GLCostPMSTDbSet
                        where p.COMPID == compid && p.COSTCID == coscid
                        select p.COSTCNM;


             return this.Json(tags.Where(t => t.StartsWith(term)),
                        JsonRequestBehavior.AllowGet);



         }


    }
}
