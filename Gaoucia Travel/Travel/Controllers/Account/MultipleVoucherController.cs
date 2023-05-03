﻿using Travel.Models;
using Travel.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel.Controllers.Account
{
    public class MultipleVoucherController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;


        public MultipleVoucherController()
        {
            ViewData["HighLight_Menu_GL_Form"] = "heighlight";
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }

        // GET: /MultipleVoucher/
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(MTransDTO model, string command)
        {
            if (command == "Print")
            {
                TempData["multiplevoucher"] = model;
                return RedirectToAction("MultipleVoucherReport");

            }
            else
                return null;


        }


        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(MTransDTO model, string command)
        {
            if (command == "Print")
            {
                TempData["multiplevoucher"] = model;
                return RedirectToAction("MultipleVoucherReport");

            }
            else
                return null;


        }

        public ActionResult PrintRow(Int64 id)
        {
            MTransDTO model = new MTransDTO();
            var search_data = (from n in db.MtransdbSet where n.ID == id select n).ToList();
            foreach(var ss in search_data)
            {
                model.ID = ss.ID;
                model.CompanyID = ss.COMPID;
                model.BranchID = ss.BRANCHID;
                model.TransDate = Convert.ToString(ss.TRANSDT);
                model.TransType = ss.TRANSTP;
                model.Transno = ss.TRANSNO;
                model.TransMonthYear = ss.TRANSMY;

                model.TransSerial = ss.TRANSSL;
                model.TransFor = ss.TRANSFOR;
                model.TransactionMode = ss.TRANSMODE;
                model.CostPoolID = ss.COSTPID;
                model.CreditCode = ss.CREDITCD;
                model.DebitCode = ss.DEBITCD;
                if(ss.CHEQUEDT!=null)
                {
                    model.CHEQUEDT = Convert.ToString(ss.CHEQUEDT);
                }
                else
                {
                    model.CHEQUEDT = null;
                }
               
                model.CHEQUENO = ss.CHEQUENO;
                model.REMARKS = ss.REMARKS;
                model.Amount = ss.AMOUNT;
            }
            TempData["multiplevoucher"] = model;
            return RedirectToAction("MultipleVoucherReport2");
        }
        public ActionResult MultipleVoucherReport()
        {
            MTransDTO model = (MTransDTO)TempData["multiplevoucher"];
            return View(model);
        }

        public ActionResult MultipleVoucherReport2()
        {
            MTransDTO model = (MTransDTO)TempData["multiplevoucher"];
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getMonth(DateTime changedtxt, string changedtxt2)//with Trans No Generation
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

            var query = from n in db.MtransMasterdbSet where n.COMPID == comid && n.TRANSTP == changedtxt2 select new { n.TRANSNO };

            Int64 maxvalue = 0, Trans = 0;

            //var nquery = select MAX(TRANSNO) from GL_STRANS where TRANSNO like ('201501%');

            //maxvalue = Convert.ToInt64((from n in db.GlStransDbSet where n.TRANSNO.Contains(halftransno) select n.TRANSNO).Max());
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


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getMonth_TransNo(DateTime changedtxt, string changedtxt2)//with Trans No Generation
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

            var query = (from n in db.MtransMasterdbSet where n.COMPID == comid && n.TRANSTP == changedtxt2 && n.TRANSDT == changedtxt select n).ToList();

            Int64 maxvalue = 0, Trans = 0;

            //var nquery = select MAX(TRANSNO) from GL_STRANS where TRANSNO like ('201501%');

            //maxvalue = Convert.ToInt64((from n in db.GlStransDbSet where n.TRANSNO.Contains(halftransno) select n.TRANSNO).Max());
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
            if (Transno.Count() == 0)
            {
                var result = new { month = Month, TransNo = "no Data" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new { month = Month, TransNo = Transno };
                return Json(result, JsonRequestBehavior.AllowGet);
            }




        }



        //debitcd load
        public JsonResult Debitcdload(string type)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            List<SelectListItem> debitcd = new List<SelectListItem>();
            string company = Convert.ToString(comid);
            //string b = company + "101";
            string c = company + "10201";

            //Int64 matchingHead1 = Convert.ToInt64(b);
            Int64 matchingHead2 = Convert.ToInt64(c);


            if (type == "MREC")
            {


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans1)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        debitcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }

                }
            }
            else if (type == "MPAY")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        debitcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "CONT")
            {


                var ans3 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans3)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        debitcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "JOUR")
            {


                var ans4 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans4)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        debitcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }






            return Json(new SelectList(debitcd, "Value", "Text"));

        }

        public JsonResult Creditload(string type)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);


            string company = Convert.ToString(comid);
            //string b = company + "101";
            string c = company + "10201";

            //Int64 matchingHead1 = Convert.ToInt64(b);
            Int64 matchingHead2 = Convert.ToInt64(c);


            List<SelectListItem> creditcd = new List<SelectListItem>();
            if (type == "MREC")
            {


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans1)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }

                }
            }
            else if (type == "MPAY")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "CONT")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }

                }
            }
            else if (type == "JOUR")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }


            return Json(new SelectList(creditcd, "Value", "Text"));

        }

        public JsonResult CreditloadAfterDebitselect(string type, Int64 dvalue)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);


            string company = Convert.ToString(comid);
            //string b = company + "101";
            string c = company + "10201";

            //Int64 matchingHead1 = Convert.ToInt64(b);
            Int64 matchingHead2 = Convert.ToInt64(c);


            List<SelectListItem> creditcd = new List<SelectListItem>();


            if (type == "CONT")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD == dvalue)
                    {

                    }
                    else
                    {
                        if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                        {
                            creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                        }
                    }

                }
            }
            else if (type == "JOUR")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD == dvalue)
                    {

                    }
                    else
                    {
                        if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                        {
                            creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                        }
                    }
                }
            }
            else if (type == "MREC")
            {


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans1)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "MPAY")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD == 5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        creditcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }

            return Json(new SelectList(creditcd, "Value", "Text"));


        }






	}
}