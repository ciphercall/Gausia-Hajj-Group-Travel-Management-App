﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Travel.Controllers;
using Travel.Models.Account;
using Travel.Models;

namespace Travel.Controllers
{
    public class TransactionController : AppController
    {

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        private TravelDbContext db = new TravelDbContext();
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public TransactionController()
        {
            ViewData["HighLight_Menu_GL_Form"] = "heighlight";
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
                     where n.COMPID == model.AGlStrans.COMPID && n.USERID == model.AGlStrans.INSUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.AGlStrans.COMPID);
            aslLog.USERID = model.AGlStrans.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.AGlStrans.INSIPNO;
            aslLog.LOGLTUDE = model.AGlStrans.INSLTUDE;
            aslLog.TABLEID = "GL_STRANS";
            string DebitCDTxtFieldName = "", CreditCDTxtFieldName = "";
            if (model.AGlStrans.TRANSTP == "MREC")
            {
                DebitCDTxtFieldName = "Received To";
                CreditCDTxtFieldName = "Received From";
            }
            else if (model.AGlStrans.TRANSTP == "MPAY")
            {
                DebitCDTxtFieldName = "Paid To";
                CreditCDTxtFieldName = "Paid From";
            }
            else if (model.AGlStrans.TRANSTP == "JOUR")
            {
                DebitCDTxtFieldName = "Debited To";
                CreditCDTxtFieldName = "Credited To";
            }
            else if (model.AGlStrans.TRANSTP == "CONT")
            {
                DebitCDTxtFieldName = "Deposited To";
                CreditCDTxtFieldName = "Withdrawl From";
            }


            string costPoolName = "";
            var getCostPoolName = (from m in db.GlCostPoolDbSet
                                   where m.COMPID == aslLog.COMPID && m.COSTPID == model.AGlStrans.COSTPID
                                   select new { m.COSTPNM }).Distinct().ToList();
            foreach (var VARIABLE in getCostPoolName)
            {
                costPoolName = VARIABLE.COSTPNM;
            }

            string DEBITCD = "";
            var getDebitName = (from m in db.GlAcchartDbSet
                                where m.COMPID == aslLog.COMPID && m.ACCOUNTCD == model.AGlStrans.DEBITCD
                                select new { m.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getDebitName)
            {
                DEBITCD = VARIABLE.ACCOUNTNM;
            }

            string CREDITCD = "";
            var getCreditName = (from n in db.GlAcchartDbSet
                                 where n.COMPID == aslLog.COMPID && n.ACCOUNTCD == model.AGlStrans.CREDITCD
                                 select new { n.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getCreditName)
            {
                CREDITCD = VARIABLE.ACCOUNTNM;
            }
            aslLog.LOGDATA = Convert.ToString("TransType: " + model.AGlStrans.TRANSTP + ",\nTrans Date: " + model.AGlStrans.TRANSDT + ",\nTransNo: " + model.AGlStrans.TRANSNO +
                ",\nTransFor: " + model.AGlStrans.TRANSFOR + ",\nCost Pool: " + costPoolName + ",\nTrans Mode: " + model.AGlStrans.TRANSMODE +
                ",\n" + DebitCDTxtFieldName + ": " + DEBITCD + ",\n" + CreditCDTxtFieldName + ": " + CREDITCD + ",\nCheque NO: " + model.AGlStrans.CHEQUENO +
                ",\nCheque Date: " + model.AGlStrans.CHEQUEDT + ",\nRemarks: " + model.AGlStrans.REMARKS + ",\nAmount: " + model.AGlStrans.AMOUNT + ".");

            aslLog.USERPC = model.AGlStrans.USERPC;
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
                     where n.COMPID == model.AGlStrans.COMPID && n.USERID == model.AGlStrans.UPDUSERID
                     select n.LOGSLNO).Max());

            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }


            aslLog.COMPID = Convert.ToInt64(model.AGlStrans.COMPID);
            aslLog.USERID = model.AGlStrans.INSUSERID;
            aslLog.LOGTYPE = "Update";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.AGlStrans.INSIPNO;
            aslLog.LOGLTUDE = model.AGlStrans.INSLTUDE;
            aslLog.TABLEID = "GL_STRANS";
            string DebitCDTxtFieldName = "", CreditCDTxtFieldName = "";
            if (model.AGlStrans.TRANSTP == "MREC")
            {
                DebitCDTxtFieldName = "Received To";
                CreditCDTxtFieldName = "Received From";
            }
            else if (model.AGlStrans.TRANSTP == "MPAY")
            {
                DebitCDTxtFieldName = "Paid To";
                CreditCDTxtFieldName = "Paid From";
            }
            else if (model.AGlStrans.TRANSTP == "JOUR")
            {
                DebitCDTxtFieldName = "Debited To";
                CreditCDTxtFieldName = "Credited To";
            }
            else if (model.AGlStrans.TRANSTP == "CONT")
            {
                DebitCDTxtFieldName = "Deposited To";
                CreditCDTxtFieldName = "Withdrawl From";
            }


            string costPoolName = "";
            var getCostPoolName = (from m in db.GlCostPoolDbSet
                                   where m.COMPID == aslLog.COMPID && m.COSTPID == model.AGlStrans.COSTPID
                                   select new { m.COSTPNM }).Distinct().ToList();
            foreach (var VARIABLE in getCostPoolName)
            {
                costPoolName = VARIABLE.COSTPNM;
            }

            string DEBITCD = "";
            var getDebitName = (from m in db.GlAcchartDbSet
                                where m.COMPID == aslLog.COMPID && m.ACCOUNTCD == model.AGlStrans.DEBITCD
                                select new { m.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getDebitName)
            {
                DEBITCD = VARIABLE.ACCOUNTNM;
            }

            string CREDITCD = "";
            var getCreditName = (from n in db.GlAcchartDbSet
                                 where n.COMPID == aslLog.COMPID && n.ACCOUNTCD == model.AGlStrans.CREDITCD
                                 select new { n.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getCreditName)
            {
                CREDITCD = VARIABLE.ACCOUNTNM;
            }

            aslLog.LOGDATA = Convert.ToString("TransType: " + model.AGlStrans.TRANSTP + ",\nTrans Date: " + model.AGlStrans.TRANSDT + ",\nTransNo: " + model.AGlStrans.TRANSNO +
                ",\nTransFor: " + model.AGlStrans.TRANSFOR + ",\nCost Pool: " + costPoolName + ",\nTrans Mode: " + model.AGlStrans.TRANSMODE +
                ",\n" + DebitCDTxtFieldName + ": " + DEBITCD + ",\n" + CreditCDTxtFieldName + ": " + CREDITCD + ",\nCheque NO: " + model.AGlStrans.CHEQUENO +
                ",\nCheque Date: " + model.AGlStrans.CHEQUEDT + ",\nRemarks: " + model.AGlStrans.REMARKS + ",\nAmount: " + model.AGlStrans.AMOUNT + ".");



            aslLog.USERPC = model.AGlStrans.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public void Delete_Asl_LogData(GL_STRANS model)
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
            aslLog.USERID = model.INSUSERID;
            aslLog.LOGTYPE = "Delete";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.INSIPNO;
            aslLog.LOGLTUDE = model.INSLTUDE;
            aslLog.TABLEID = "GL_STRANS";
            string DebitCDTxtFieldName = "", CreditCDTxtFieldName = "";
            if (model.TRANSTP == "MREC")
            {
                DebitCDTxtFieldName = "Received To";
                CreditCDTxtFieldName = "Received From";
            }
            else if (model.TRANSTP == "MPAY")
            {
                DebitCDTxtFieldName = "Paid To";
                CreditCDTxtFieldName = "Paid From";
            }
            else if (model.TRANSTP == "JOUR")
            {
                DebitCDTxtFieldName = "Debited To";
                CreditCDTxtFieldName = "Credited To";
            }
            else if (model.TRANSTP == "CONT")
            {
                DebitCDTxtFieldName = "Deposited To";
                CreditCDTxtFieldName = "Withdrawl From";
            }

            string costPoolName = "";
            var getCostPoolName = (from m in db.GlCostPoolDbSet
                                   where m.COMPID == aslLog.COMPID && m.COSTPID == model.COSTPID
                                   select new { m.COSTPNM }).Distinct().ToList();
            foreach (var VARIABLE in getCostPoolName)
            {
                costPoolName = VARIABLE.COSTPNM;
            }

            string DEBITCD = "";
            var getDebitName = (from m in db.GlAcchartDbSet
                                where m.COMPID == aslLog.COMPID && m.ACCOUNTCD == model.DEBITCD
                                select new { m.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getDebitName)
            {
                DEBITCD = VARIABLE.ACCOUNTNM;
            }

            string CREDITCD = "";
            var getCreditName = (from n in db.GlAcchartDbSet
                                 where n.COMPID == aslLog.COMPID && n.ACCOUNTCD == model.CREDITCD
                                 select new { n.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getCreditName)
            {
                CREDITCD = VARIABLE.ACCOUNTNM;
            }


            aslLog.LOGDATA =
                Convert.ToString("TransType: " + model.TRANSTP + ",\nTrans Date: " + model.TRANSDT + ",\nTransNo: " + model.TRANSNO + ",\nTransFor: " + model.TRANSFOR +
                ",\nCost Pool: " + costPoolName + ",\nTrans Mode: " + model.TRANSMODE + ",\n" + DebitCDTxtFieldName + ": " + DEBITCD + ",\n" + CreditCDTxtFieldName + ": " + CREDITCD +
                ",\nCheque NO: " + model.CHEQUENO + ",\nCheque Date: " + model.CHEQUEDT + ",\nRemarks: " + model.REMARKS + ",\nAmount: " + model.AMOUNT + ".");
            aslLog.USERPC = model.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }

        public ASL_DELETE AslDelete = new ASL_DELETE();
        public void Delete_ASL_DELETE(GL_STRANS model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("HH:mm:ss tt"));

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
            AslDelete.USERID = model.UPDUSERID;
            AslDelete.DELSLNO = AslDelete.DELSLNO;
            AslDelete.DELDATE = Convert.ToString(date);
            AslDelete.DELTIME = Convert.ToString(time);
            AslDelete.DELIPNO = model.UPDIPNO;
            AslDelete.DELLTUDE = model.UPDLTUDE;
            AslDelete.TABLEID = "GL_STRANS";

            string DebitCDTxtFieldName = "", CreditCDTxtFieldName = "";
            if (model.TRANSTP == "MREC")
            {
                DebitCDTxtFieldName = "Received To";
                CreditCDTxtFieldName = "Received From";
            }
            else if (model.TRANSTP == "MPAY")
            {
                DebitCDTxtFieldName = "Paid To";
                CreditCDTxtFieldName = "Paid From";
            }
            else if (model.TRANSTP == "JOUR")
            {
                DebitCDTxtFieldName = "Debited To";
                CreditCDTxtFieldName = "Credited To";
            }
            else if (model.TRANSTP == "CONT")
            {
                DebitCDTxtFieldName = "Deposited To";
                CreditCDTxtFieldName = "Withdrawl From";
            }

            string costPoolName = "";
            var getCostPoolName = (from m in db.GlCostPoolDbSet
                                   where m.COMPID == aslLog.COMPID && m.COSTPID == model.COSTPID
                                   select new { m.COSTPNM }).Distinct().ToList();
            foreach (var VARIABLE in getCostPoolName)
            {
                costPoolName = VARIABLE.COSTPNM;
            }

            string DEBITCD = "";
            var getDebitName = (from m in db.GlAcchartDbSet
                                where m.COMPID == aslLog.COMPID && m.ACCOUNTCD == model.DEBITCD
                                select new { m.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getDebitName)
            {
                DEBITCD = VARIABLE.ACCOUNTNM;
            }

            string CREDITCD = "";
            var getCreditName = (from n in db.GlAcchartDbSet
                                 where n.COMPID == aslLog.COMPID && n.ACCOUNTCD == model.CREDITCD
                                 select new { n.ACCOUNTNM }).Distinct().ToList();
            foreach (var VARIABLE in getCreditName)
            {
                CREDITCD = VARIABLE.ACCOUNTNM;
            }


            AslDelete.DELDATA =
                Convert.ToString("TransType: " + model.TRANSTP + ",\nTrans Date: " + model.TRANSDT + ",\nTransNo: " + model.TRANSNO + ",\nTransFor: " + model.TRANSFOR +
                ",\nCost Pool: " + costPoolName + ",\nTrans Mode: " + model.TRANSMODE + ",\n" + DebitCDTxtFieldName + ": " + DEBITCD + ",\n" + CreditCDTxtFieldName + ": " + CREDITCD +
                ",\nCheque NO: " + model.CHEQUENO + ",\nCheque Date: " + model.CHEQUEDT + ",\nRemarks: " + model.REMARKS + ",\nAmount: " + model.AMOUNT + ".");

            AslDelete.USERPC = model.USERPC;
            db.AslDeleteDbSet.Add(AslDelete);
        }














        // GET: /Transaction/

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PageModel model,string command)
        {
            if (model.AGlStrans.TRANSTP != null && model.AGlStrans.TRANSDT != null && model.AGlStrans.TRANSNO != null && model.AGlStrans.BRANCHID!=null)
            {
                if (command == "Create")
                {

                    //Get Ip ADDRESS,Time & user PC Name
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];


                    model.AGlStrans.USERPC = strHostName;
                    model.AGlStrans.INSIPNO = ipAddress.ToString();
                    model.AGlStrans.INSTIME = Convert.ToDateTime(td);

                    model.AGlStrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.AGlStrans.TRANSMY = model.AGlStrans.TRANSMY.ToUpper();


                    var cardid = (from n in db.passengerDbSet
                                  where n.COMPID == model.AGlStrans.COMPID && n.CARDNO == model.AGlStrans.CARDNO
                                  select new { n.CARDID }).ToList();
                    foreach (var item in cardid)
                    {
                        model.AGlStrans.CARDID = Convert.ToString(item.CARDID);
                    }

                    var duplicate_data_test = (from n in db.GlStransDbSet
                                               where
                                                   n.COMPID == model.AGlStrans.COMPID && n.TRANSTP == model.AGlStrans.TRANSTP &&
                                                   n.TRANSNO == model.AGlStrans.TRANSNO && n.BRANCHID==model.AGlStrans.BRANCHID
                                               select n).ToList();
                    if (duplicate_data_test.Count == 0)
                    {
                        Insert_Asl_LogData(model);
                        db.GlStransDbSet.Add(model.AGlStrans);
                        db.SaveChanges();

                        TempData["TransactionMessage"] = "Transaction Created ";
                    }
                    else
                    {
                        TempData["TransactionMessage"] = "Try Again";
                    }

                    return RedirectToAction("Create");
                }

                if (command == "Create & Print")
                {

                    //Get Ip ADDRESS,Time & user PC Name
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];


                    model.AGlStrans.USERPC = strHostName;
                    model.AGlStrans.INSIPNO = ipAddress.ToString();
                    model.AGlStrans.INSTIME = Convert.ToDateTime(td);

                    model.AGlStrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                    model.AGlStrans.TRANSMY = model.AGlStrans.TRANSMY.ToUpper();


                    var cardid = (from n in db.passengerDbSet
                                  where n.COMPID == model.AGlStrans.COMPID && n.CARDNO == model.AGlStrans.CARDNO
                                  select new { n.CARDID }).ToList();
                    foreach (var item in cardid)
                    {
                        model.AGlStrans.CARDID = Convert.ToString(item.CARDID);
                    }

                    var duplicate_data_test = (from n in db.GlStransDbSet
                                               where
                                                   n.COMPID == model.AGlStrans.COMPID && n.TRANSTP == model.AGlStrans.TRANSTP &&
                                                   n.TRANSNO == model.AGlStrans.TRANSNO && n.BRANCHID==model.AGlStrans.BRANCHID
                                               select n).ToList();
                    if (duplicate_data_test.Count == 0)
                    {
                        Insert_Asl_LogData(model);
                        db.GlStransDbSet.Add(model.AGlStrans);
                        db.SaveChanges();

                        TempData["TransactionCreate"] = null;
                        TempData["Transaction"] = model;

                        return RedirectToAction("VoucharReport");
                    }
                    else
                    {
                        TempData["TransactionMessage"] = "Try Again";
                        return RedirectToAction("Create");
                    }

                    // return RedirectToAction("Create");
                }

            }
            else
            {
                TempData["Transaction_TransactionType"] = "Please firstly select transaction type field !";
                return RedirectToAction("Create");
            }
            



            return RedirectToAction("Create");
           
            
        }


        public ActionResult VoucharReport()
        {

            PageModel model = (PageModel)TempData["Transaction"];
            return View(model);
        }

        public ActionResult Update()
        {

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PageModel model,string command)
        {
            if (model.AGlStrans.TRANSTP != null && model.AGlStrans.TRANSDT != null && model.AGlStrans.TRANSNO != null && model.AGlStrans.BRANCHID!=null)
            {
                if (command == "Update")
                {
                    db.Entry(model.AGlStrans).State = EntityState.Modified;

                    //Get Ip ADDRESS,Time & user PC Name
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];

                    model.AGlStrans.USERPC = strHostName;
                    model.AGlStrans.INSIPNO = ipAddress.ToString();
                    model.AGlStrans.INSTIME = Convert.ToDateTime(td);

                    model.AGlStrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                    model.AGlStrans.UPDIPNO = ipAddress.ToString();
                    model.AGlStrans.UPDTIME = Convert.ToDateTime(td);
                    //asluserco.UPDTIME = DateTime.Parse(td.ToString(), dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    //Update User ID save AslUSerco table attribute INSUSERID
                    model.AGlStrans.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    model.AGlStrans.INSLTUDE = model.AGlStrans.INSLTUDE;
                    model.AGlStrans.UPDLTUDE = model.AGlStrans.INSLTUDE;

                    model.AGlStrans.Gl_StransID = model.AGlStrans.Gl_StransID;
                    model.AGlStrans.BRANCHID = model.AGlStrans.BRANCHID;
                    model.AGlStrans.TRANSTP = model.AGlStrans.TRANSTP;
                    model.AGlStrans.TRANSDT = model.AGlStrans.TRANSDT;
                    model.AGlStrans.TRANSMY = model.AGlStrans.TRANSMY.ToUpper();
                    model.AGlStrans.TRANSNO = model.AGlStrans.TRANSNO;

                    model.AGlStrans.TRANSFOR = model.AGlStrans.TRANSFOR;
                    model.AGlStrans.COSTPID = model.AGlStrans.COSTPID;

                    var cardid = (from n in db.passengerDbSet
                                  where n.COMPID == model.AGlStrans.COMPID && n.CARDNO == model.AGlStrans.CARDNO
                                  select new { n.CARDID }).ToList();
                    foreach (var item in cardid)
                    {
                        model.AGlStrans.CARDID = Convert.ToString(item.CARDID);
                    }

                    model.AGlStrans.TRANSMODE = model.AGlStrans.TRANSMODE;
                    model.AGlStrans.DEBITCD = model.AGlStrans.DEBITCD;
                    model.AGlStrans.CREDITCD = model.AGlStrans.CREDITCD;
                    model.AGlStrans.CHEQUENO = model.AGlStrans.CHEQUENO;
                    model.AGlStrans.CHEQUEDT = model.AGlStrans.CHEQUEDT;
                    model.AGlStrans.AMOUNT = model.AGlStrans.AMOUNT;
                    model.AGlStrans.REMARKS = model.AGlStrans.REMARKS;

                    Update_Asl_LogData(model);

                    db.SaveChanges();
                    TempData["TransactionUpdate"] = "'" + model.AGlStrans.TRANSNO + "' successfully updated!";

                    return RedirectToAction("Update");
                }
                else if (command == "Update & Print")
                {
                    db.Entry(model.AGlStrans).State = EntityState.Modified;

                    //Get Ip ADDRESS,Time & user PC Name
                    string strHostName = System.Net.Dns.GetHostName();
                    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                    IPAddress ipAddress = ipHostInfo.AddressList[0];

                    model.AGlStrans.USERPC = strHostName;
                    model.AGlStrans.INSIPNO = ipAddress.ToString();
                    model.AGlStrans.INSTIME = Convert.ToDateTime(td);

                    model.AGlStrans.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                    model.AGlStrans.UPDIPNO = ipAddress.ToString();
                    model.AGlStrans.UPDTIME = Convert.ToDateTime(td);
                    //asluserco.UPDTIME = DateTime.Parse(td.ToString(), dateformat, System.Globalization.DateTimeStyles.AssumeLocal);
                    //Update User ID save AslUSerco table attribute INSUSERID
                    model.AGlStrans.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                    model.AGlStrans.INSLTUDE = model.AGlStrans.INSLTUDE;
                    model.AGlStrans.UPDLTUDE = model.AGlStrans.INSLTUDE;

                    model.AGlStrans.Gl_StransID = model.AGlStrans.Gl_StransID;
                    model.AGlStrans.BRANCHID = model.AGlStrans.BRANCHID;
                    model.AGlStrans.TRANSTP = model.AGlStrans.TRANSTP;
                    model.AGlStrans.TRANSDT = model.AGlStrans.TRANSDT;
                    model.AGlStrans.TRANSMY = model.AGlStrans.TRANSMY.ToUpper();
                    model.AGlStrans.TRANSNO = model.AGlStrans.TRANSNO;

                    model.AGlStrans.TRANSFOR = model.AGlStrans.TRANSFOR;
                    model.AGlStrans.COSTPID = model.AGlStrans.COSTPID;
                    var cardid = (from n in db.passengerDbSet
                                  where n.COMPID == model.AGlStrans.COMPID && n.CARDNO == model.AGlStrans.CARDNO
                                  select new { n.CARDID }).ToList();
                    foreach (var item in cardid)
                    {
                        model.AGlStrans.CARDID = Convert.ToString(item.CARDID);
                    }

                    model.AGlStrans.TRANSMODE = model.AGlStrans.TRANSMODE;
                    model.AGlStrans.DEBITCD = model.AGlStrans.DEBITCD;
                    model.AGlStrans.CREDITCD = model.AGlStrans.CREDITCD;
                    model.AGlStrans.CHEQUENO = model.AGlStrans.CHEQUENO;
                    model.AGlStrans.CHEQUEDT = model.AGlStrans.CHEQUEDT;
                    model.AGlStrans.AMOUNT = model.AGlStrans.AMOUNT;
                    model.AGlStrans.REMARKS = model.AGlStrans.REMARKS;

                    Update_Asl_LogData(model);

                    db.SaveChanges();

                    TempData["TransactionMessage"] = "Transaction Updated ";
                    TempData["Transaction"] = model;
                    return RedirectToAction("VoucharReport");

                }
            }
            else
            {
                TempData["Transaction_TransactionType"] = "Please firstly select transaction type field !";
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
            if (model.AGlStrans.TRANSTP != null && model.AGlStrans.TRANSDT !=null && model.AGlStrans.TRANSNO!=null && model.AGlStrans.BRANCHID!=null)
            {
                GL_STRANS aStrans = db.GlStransDbSet.Find(model.AGlStrans.Gl_StransID);


                //Get Ip ADDRESS,Time & user PC Name 
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aStrans.USERPC = strHostName;
                aStrans.UPDIPNO = ipAddress.ToString();
                aStrans.UPDTIME = Convert.ToDateTime(td);

                //Delete User ID save AslUSerco table attribute INSUSERID
                aStrans.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                //Get current LOGLTUDE data 
                aStrans.UPDLTUDE = model.AGlStrans.UPDLTUDE;

                Delete_Asl_LogData(aStrans);
                Delete_ASL_DELETE(aStrans);

                db.GlStransDbSet.Remove(aStrans);
                db.SaveChanges();

                TempData["TransactionDelete"] = "'" + aStrans.TRANSNO + "' successfully Deleted";
                return RedirectToAction("Delete");
            }
            else
            {
                TempData["Transaction_TransactionType"] = "Please firstly select transaction type field !";
                return RedirectToAction("Delete");
            }



        }








        //JseonResult for DateChanged and get year.............................Start
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DateChanged_getMonth(DateTime changedtxt,string changedtxt2)//with Trans No Generation
        {
            Int64 comid=Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            string converttoString = Convert.ToString(changedtxt.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(9, 2);
            string getMonth = converttoString.Substring(3, 3);
            string Month = getMonth + "-" + getYear;


            string converttoString1 = Convert.ToString(changedtxt.ToString("dd-MM-yyyy"));
            string getyear = converttoString1.Substring(8, 2);
            string getmonth = converttoString1.Substring(3, 2);
            string halftransno = getyear + getmonth;

            var query = from n in db.GlStransDbSet where n.COMPID == comid && n.TRANSTP==changedtxt2 select new {n.TRANSNO};

            Int64 maxvalue=0,Trans=0;

            //var nquery = select MAX(TRANSNO) from GL_STRANS where TRANSNO like ('201501%');
            
            //maxvalue = Convert.ToInt64((from n in db.GlStransDbSet where n.TRANSNO.Contains(halftransno) select n.TRANSNO).Max());
            List<SelectListItem> Transno=new List<SelectListItem>();

            foreach (var x in query)
            {

                string temp = Convert.ToString(x.TRANSNO);
                string substring = temp.Substring(0, 4);
                if (substring == halftransno)
                {
                    Transno.Add(new SelectListItem{Text = x.TRANSNO.ToString(),Value = x.TRANSNO.ToString()});
                   
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
           
            var result = new { month = Month,TransNo=transno };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        //JseonResult for DateChanged and get year.............................End

        public JsonResult Invoiceload(string theDate, string type, string branch)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            DateTime dt = Convert.ToDateTime(theDate);
            Int64 branchid = Convert.ToInt64(branch);
            //DateTime dd = DateTime.Parse(theDate, dateformat, System.Globalization.DateTimeStyles.AssumeLocal);

            


            //DateTime datetm = Convert.ToDateTime(dd);

            List<SelectListItem> trans = new List<SelectListItem>();

         

            var transres = (from n in db.GlStransDbSet
                            where n.TRANSDT == dt && n.COMPID == comid && n.TRANSTP==type && n.BRANCHID==branchid
                            select new
                            {
                                n.TRANSNO
                            }
                            )
                            .Distinct()
                            .ToList();

            string transNO = null;
            foreach (var f in transres)
            {
                if (transNO == null)
                {
                    transNO = Convert.ToString(f.TRANSNO);
                }
                trans.Add(new SelectListItem { Text = f.TRANSNO.ToString(), Value = f.TRANSNO.ToString() });
            }

            var FirsttransNO = new { TransNO = transNO };
            //return Json(new SelectList(trans, "Value", "Text"), FirsttransNO.ToString());
            return Json(new SelectList(trans, "Value", "Text"));
            //return Json(transres, JsonRequestBehavior.AllowGet);
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


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans1)
                {
                    if(x.ACCOUNTCD.ToString().Substring(0,8)==c)
                    {
                         debitcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                   
                }
            }
            else if (type == "MPAY")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if(x.ACCOUNTCD.ToString().Substring(0,8)!=c)
                    {
                         debitcd.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "CONT")
            {


                var ans3 = from n in db.GlAcchartDbSet where n.COMPID == comid &&  n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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


                var ans4 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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


        //creditcd load
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


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == comid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
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









        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Update_SelectTransNo(Int64 changedtxt, string type)
        {

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var selectdata = from n in db.GlStransDbSet where n.COMPID == compid && n.TRANSNO == changedtxt && n.TRANSTP == type select new { n.Gl_StransID, n.TRANSDT, n.TRANSTP, n.TRANSFOR, n.TRANSMY, n.COSTPID, n.TRANSMODE, n.DEBITCD,
                n.CREDITCD, n.REMARKS, n.AMOUNT, n.CHEQUEDT, n.CHEQUENO,n.CARDNO };
            string transfor = "", transmode = "", chequeno = "", chequedate = "", remarks = "", debitaccount = "", creditaccount = "",cardno = "";

            Int64 costpoolid = 0, debitcd = 0, creditcd = 0, amount = 0, primaryid = 0;

            foreach (var l in selectdata)
            {
                primaryid = Convert.ToInt64(l.Gl_StransID);

                //string xx = Convert.ToString(l.CHEQUEDT);
                //DateTime date = DateTime.Parse(xx);
                //chequedate = date.ToString("dd-MMM-yyyy");

                costpoolid = Convert.ToInt64(l.COSTPID);
                transfor = Convert.ToString(l.TRANSFOR);
                transmode = Convert.ToString(l.TRANSMODE);
                debitcd = Convert.ToInt64(l.DEBITCD);
                creditcd = Convert.ToInt64(l.CREDITCD);

                chequeno = Convert.ToString(l.CHEQUENO);
                chequedate = Convert.ToString(l.CHEQUEDT);
                remarks = Convert.ToString(l.REMARKS);
                amount = Convert.ToInt64(l.AMOUNT);
                cardno = Convert.ToString(l.CARDNO);
            }

            if (debitcd != null)
            {
                var res = from x in db.GlAcchartDbSet
                          where x.COMPID == compid && x.ACCOUNTCD == debitcd
                          select new { x.ACCOUNTNM };
                foreach (var n in res)
                {
                    debitaccount = n.ACCOUNTNM;
                }
            }


            if (creditcd != null)
            {
                //debitcd = Convert.ToInt64(l.DEBITCD);
                var res2 = from x in db.GlAcchartDbSet
                           where x.COMPID == compid && x.ACCOUNTCD == creditcd
                           select new { x.ACCOUNTNM };
                foreach (var n in res2)
                {
                    creditaccount = n.ACCOUNTNM;
                }
            }




            List<SelectListItem> Debitlist = new List<SelectListItem>();
            string company = Convert.ToString(compid);
            //string b = company + "101";
            string c = company + "10201";

            //Int64 matchingHead1 = Convert.ToInt64(b);
            Int64 matchingHead2 = Convert.ToInt64(c);


            if (type == "MREC")
            {


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans1)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        Debitlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                    
                }
            }
            else if (type == "MPAY")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        Debitlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "CONT")
            {


                var ans3 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans3)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        Debitlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "JOUR")
            {


                var ans4 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans4)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        Debitlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            //For credit
            List<SelectListItem> Creditlist = new List<SelectListItem>();


            if (type == "CONT")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        Creditlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                   


                }
            }
            else if (type == "JOUR")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {

                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        Creditlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }

                }
            }
            else if (type == "MREC")
            {


                var ans1 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans1)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) != c)
                    {
                        Creditlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }
            else if (type == "MPAY")
            {



                var ans2 = from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD==5 select new { n.ACCOUNTCD, n.ACCOUNTNM };
                foreach (var x in ans2)
                {
                    if (x.ACCOUNTCD.ToString().Substring(0, 8) == c)
                    {
                        Creditlist.Add(new SelectListItem { Text = x.ACCOUNTNM, Value = Convert.ToString(x.ACCOUNTCD) });
                    }
                }
            }

            var findpassengername =
               (from n in db.passengerDbSet where n.COMPID == compid && n.CARDNO == cardno select new { n.PSGRNM })
                   .ToList();
            string passenger = "";
            foreach (var x in findpassengername)
            {
                passenger = x.PSGRNM;
            }


            var result = new { keyid = primaryid, For = transfor, date = chequedate, costpool = costpoolid, Cardno = cardno, Passenger = passenger, mode = transmode, firstdebittext = debitaccount, debitCD = debitcd, debit = Debitlist, firstcredittext = creditaccount, creditCD = creditcd, credit = Creditlist, cheque = chequeno, ChequeDate = chequedate, remarks = remarks, amount = amount };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CardNo_Changed(string type, string type1)
        {
            Int64 comid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);
            var searchaccountcd =
                     (from n in db.GlAcchartDbSet where n.COMPID == comid select new { n.ACCOUNTCD, n.ACCOUNTNM }).ToList();
            var agentfind =
                   (from n in db.passengerDbSet
                    where n.COMPID == comid && n.CARDNO == type
                    select new { n.AGENTID }).ToList();
            List<SelectListItem> credit = new List<SelectListItem>();
            foreach (var x in agentfind)
            {
                if (x.AGENTID.ToString().Substring(3, 7) == "1020202")
                {
                    foreach (var item in searchaccountcd)
                    {
                        if (item.ACCOUNTCD.ToString().Substring(8, 7) == type)
                        {
                            credit.Add(new SelectListItem { Text = item.ACCOUNTNM, Value = Convert.ToString(item.ACCOUNTCD) });
                            break;
                        }
                    }
                }
                else
                {

                    foreach (var item in searchaccountcd)
                    {
                        if (item.ACCOUNTCD == x.AGENTID)
                        {
                            credit.Add(new SelectListItem { Text = item.ACCOUNTNM, Value = Convert.ToString(item.ACCOUNTCD) });
                            break;
                        }
                    }

                }

            }







            return Json(new SelectList(credit, "Value", "Text"));
        }

    }
}
