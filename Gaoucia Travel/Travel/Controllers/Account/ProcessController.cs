using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Travel.DataAccess;
using Travel.Models;

namespace Travel.Controllers
{
    public class ProcessController : AppController
    {
        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;


        TravelDbContext db = new TravelDbContext();
        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;



        public ProcessController()
        {
            ViewData["HighLight_Menu_GL_Form"] = "heighlight";
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }

        public ASL_LOG aslLog = new ASL_LOG();


        public void Insert_Process_LogData(PageModel model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("d"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.AGlMaster.COMPID && n.USERID == model.AGlMaster.INSUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(model.AGlMaster.COMPID);
            aslLog.USERID = model.AGlMaster.INSUSERID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.AGlMaster.INSIPNO;
            aslLog.LOGLTUDE = model.AGlMaster.INSLTUDE;
            aslLog.TABLEID = "GL_MASTER PROCESS";

            string username = "";
            var UserNameFind = (from n in db.AslUsercoDbSet where n.USERID == aslLog.USERID select n).ToList();
            foreach (var name in UserNameFind)
            {
                username = name.USERNM;
            }

            aslLog.LOGDATA = Convert.ToString("Process: " + "Process to GlMaster" + "Time: " + aslLog.LOGTIME + ",\nDate: " + model.AGlMaster.TRANSDT + ",\nUserName: " + username + ".");
            aslLog.USERPC = model.AGlMaster.USERPC;
            db.AslLogDbSet.Add(aslLog);
        }
        // GET: /Process/

        public ActionResult Index()
        {
            return View();
        }


        [AcceptVerbs("POST")]
        [ActionName("Index")]
        public ActionResult IndexPost(PageModel model, string command)
        {
            if (command == "Process")
            {
                if (model.AGlMaster.TRANSDT != null)
                {

                    Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);


                    var checkDate = (from n in db.GlStransDbSet where n.TRANSDT == model.AGlMaster.TRANSDT && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();
                    var checkDate_Mtrans = (from n in db.MtransdbSet where n.TRANSDT == model.AGlMaster.TRANSDT && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();
                    var checkDate_RPDRCR = (from n in db.RPDRCRDbSet where n.TRANSDT == model.AGlMaster.TRANSDT && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();

                    DateTime transdt = Convert.ToDateTime(model.AGlMaster.TRANSDT);
                  
                    var result = processRefund.refundProcess(transdt);

                    //var result = IndoorTransProcess.indoorTransProcess(transdt);
                    //var process_pharmacy = PharmacyMasterProcess.PharmacyProcess(transdt);
                    //var bill_process = billProcess2.processbill(transdt);
                    //var sale_process = PharmacyProcess.SaleSaleReturn(transdt);

                    if (checkDate.Count != 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_STRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();

                        foreach (var x in checkDate)
                        {


                            if (x.TRANSTP == "MREC")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_STRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 10001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";


                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;


                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 10002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);

                                    Insert_Process_LogData(model);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }

                            }

                            else if (x.TRANSTP == "MPAY")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_STRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 20001;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = 20002;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "JOUR")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_STRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 30001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 30002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";
                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "CONT")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_STRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 40001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;


                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";


                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 40002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();


                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            TempData["message"] = "Process Completed for Date : " + model.AGlMaster.TRANSDT;




                        }



                    }
                    else if (checkDate.Count == 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_STRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();
                    }

                    if (checkDate_Mtrans.Count != 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT 
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_MTRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();

                        foreach (var x in checkDate_Mtrans)
                        {


                            if (x.TRANSTP == "MREC")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 50001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";


                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;


                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 50002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);

                                    Insert_Process_LogData(model);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }

                            }

                            else if (x.TRANSTP == "MPAY")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 60001;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = 60002;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;


                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "JOUR")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 70001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 70002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_STRANS";
                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            else if (x.TRANSTP == "CONT")
                            {
                                Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                                    where a.COMPID == compid && a.TRANSTP == x.TRANSTP && a.TABLEID == "GL_MTRANS"
                                                                    select a.TRANSSL).Max());

                                if (maxSlCheck == 0)
                                {
                                    model.AGlMaster.TRANSSL = 80001;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";


                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = 80002;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;
                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();


                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = maxSlCheck + 1;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.DEBITCD;
                                    model.AGlMaster.CREDITCD = x.CREDITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = x.AMOUNT;
                                    model.AGlMaster.CREDITAMT = 0;

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                    model.AGlMaster.TRANSSL = maxSlCheck + 2;

                                    model.AGlMaster.TRANSDT = x.TRANSDT;
                                    model.AGlMaster.COMPID = x.COMPID;
                                    model.AGlMaster.BRANCHID = x.BRANCHID;
                                    model.AGlMaster.TRANSTP = x.TRANSTP;
                                    model.AGlMaster.TRANSMY = x.TRANSMY;
                                    model.AGlMaster.TRANSNO = x.TRANSNO;
                                    model.AGlMaster.TRANSFOR = x.TRANSFOR;
                                    model.AGlMaster.TRANSMODE = x.TRANSMODE;
                                    model.AGlMaster.COSTPID = x.COSTPID;

                                    model.AGlMaster.CARDID = x.CARDID;
                                    model.AGlMaster.CARDNO = x.CARDNO;

                                    model.AGlMaster.DEBITCD = x.CREDITCD;
                                    model.AGlMaster.CREDITCD = x.DEBITCD;
                                    model.AGlMaster.CHEQUENO = x.CHEQUENO;
                                    model.AGlMaster.CHEQUEDT = x.CHEQUEDT;
                                    model.AGlMaster.REMARKS = x.REMARKS;

                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = x.AMOUNT;

                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TABLEID = "GL_MTRANS";

                                    model.AGlMaster.USERPC = strHostName;
                                    model.AGlMaster.INSIPNO = ipAddress.ToString();
                                    model.AGlMaster.INSTIME = Convert.ToDateTime(td);

                                    model.AGlMaster.INSUSERID =
                                        Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                                    model.AGlMaster.INSLTUDE = model.AGlMaster.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    Insert_Process_LogData(model);
                                    db.SaveChanges();
                                }
                            }
                            TempData["message"] = "Process Completed for Date : " + model.AGlMaster.TRANSDT;




                        }



                    }
                    else if (checkDate_Mtrans.Count == 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {
                            if (VARIABLE.TABLEID == "GL_MTRANS")
                            {
                                db.GlMasterDbSet.Remove(VARIABLE);
                            }
                        }



                        db.SaveChanges();
                    }

                    if (checkDate_RPDRCR.Count != 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT && l.TABLEID == "RPDRCR"
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {

                            db.GlMasterDbSet.Remove(VARIABLE);

                        }



                        db.SaveChanges();

                        foreach (var rpdata in checkDate_RPDRCR)
                        {

                            if (rpdata.TRANSFOR == "RECEIVABLE")
                            {

                                model.AGlMaster.COMPID = rpdata.COMPID;
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                model.AGlMaster.TRANSNO = rpdata.TRANSNO;
                                var maxSLfind = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());
                                if (maxSLfind.ToString().Substring(0, 1) == "5")
                                {
                                    model.AGlMaster.TRANSSL = maxSLfind + 1;
                                }
                                else
                                {
                                    model.AGlMaster.TRANSSL = 50001;
                                }
                                model.AGlMaster.TRANSDRCR = "DEBIT";
                                model.AGlMaster.TRANSFOR = "CARD";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = rpdata.CARDPID;
                                model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);
                                model.AGlMaster.DEBITCD = rpdata.ACCOUNTCD;

                                var costicdfind = (from n in db.GLCostPMSTDbSet
                                                   where n.COMPID == rpdata.COMPID && n.COSTCID == rpdata.CARDCID
                                                   select new { n.COSTICD }).ToList();
                                foreach (var x in costicdfind)
                                {
                                    model.AGlMaster.CREDITCD = x.COSTICD;
                                }
                                model.AGlMaster.REMARKS = "RECEIVABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                model.AGlMaster.DEBITAMT = rpdata.AMOUNT;
                                model.AGlMaster.CREDITAMT = 0;
                                model.AGlMaster.TABLEID = "RPDRCR";
                                model.AGlMaster.USERPC = rpdata.USERPC;
                                model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                model.AGlMaster.INSTIME = rpdata.INSTIME;
                                model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;
                                if (model.AGlMaster.CREDITCD == 0)
                                {

                                }
                                else
                                {
                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                }


                                model.AGlMaster.COMPID = rpdata.COMPID;
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                model.AGlMaster.TRANSNO = rpdata.TRANSNO;

                                var maxSLfind2 = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());

                                if (maxSLfind2.ToString().Substring(0, 1) == "5")
                                {
                                    model.AGlMaster.TRANSSL = maxSLfind2 + 1;
                                }



                                model.AGlMaster.TRANSDRCR = "CREDIT";
                                model.AGlMaster.TRANSFOR = "CARD";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = rpdata.CARDPID;
                                model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);

                                model.AGlMaster.CREDITCD = rpdata.ACCOUNTCD;


                                foreach (var x in costicdfind)
                                {
                                    model.AGlMaster.DEBITCD = x.COSTICD;
                                }
                                model.AGlMaster.REMARKS = "RECEIVABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                model.AGlMaster.DEBITAMT = 0;
                                model.AGlMaster.CREDITAMT = rpdata.AMOUNT;
                                model.AGlMaster.TABLEID = "RPDRCR";
                                model.AGlMaster.USERPC = rpdata.USERPC;
                                model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                model.AGlMaster.INSTIME = rpdata.INSTIME;
                                model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;
                                if (model.AGlMaster.DEBITCD == 0)
                                {

                                }
                                else
                                {
                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();
                                }



                                if (rpdata.AMTCASH > 0)
                                {
                                    model.AGlMaster.COMPID = rpdata.COMPID;
                                    model.AGlMaster.TRANSTP = "MREC";
                                    model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                    model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                    model.AGlMaster.TRANSNO = rpdata.TRANSNO;

                                    var maxSLfind3 = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());

                                    if (maxSLfind3.ToString().Substring(0, 1) == "5")
                                    {
                                        model.AGlMaster.TRANSSL = maxSLfind3 + 1;
                                    }

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TRANSFOR = "CARD";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = rpdata.CARDPID;
                                    model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);
                                    model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(compid) + "102010100001");


                                    model.AGlMaster.CREDITCD = rpdata.ACCOUNTCD;

                                    model.AGlMaster.REMARKS = "RECEIVABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                    model.AGlMaster.DEBITAMT = rpdata.AMTCASH;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TABLEID = "RPDRCR";
                                    model.AGlMaster.USERPC = rpdata.USERPC;
                                    model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                    model.AGlMaster.INSTIME = rpdata.INSTIME;
                                    model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                    model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.COMPID = rpdata.COMPID;
                                    model.AGlMaster.TRANSTP = "MREC";
                                    model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                    model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                    model.AGlMaster.TRANSNO = rpdata.TRANSNO;
                                    var maxSLfind4 = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());

                                    if (maxSLfind4.ToString().Substring(0, 1) == "5")
                                    {
                                        model.AGlMaster.TRANSSL = maxSLfind4 + 1;
                                    }


                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TRANSFOR = "CARD";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = rpdata.CARDPID;
                                    model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);

                                    model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(compid) + "102010100001");



                                    model.AGlMaster.DEBITCD = rpdata.ACCOUNTCD;

                                    model.AGlMaster.REMARKS = "RECEIVABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = rpdata.AMTCASH;
                                    model.AGlMaster.TABLEID = "RPDRCR";
                                    model.AGlMaster.USERPC = rpdata.USERPC;
                                    model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                    model.AGlMaster.INSTIME = rpdata.INSTIME;
                                    model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                    model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                }

                            }

                            if (rpdata.TRANSFOR == "PAYABLE")
                            {

                                model.AGlMaster.COMPID = rpdata.COMPID;
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                model.AGlMaster.TRANSNO = rpdata.TRANSNO;
                                var maxSLfind = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());

                                if (maxSLfind.ToString().Substring(0, 1) == "5")
                                {
                                    model.AGlMaster.TRANSSL = maxSLfind + 1;
                                }


                                else
                                {
                                    model.AGlMaster.TRANSSL = 50001;
                                }
                                model.AGlMaster.TRANSDRCR = "DEBIT";
                                model.AGlMaster.TRANSFOR = "CARD";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = rpdata.CARDPID;
                                model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);
                                model.AGlMaster.CREDITCD = rpdata.ACCOUNTCD;

                                var costicdfind = (from n in db.GLCostPMSTDbSet
                                                   where n.COMPID == rpdata.COMPID && n.COSTCID == rpdata.CARDCID
                                                   select new { n.COSTECD }).ToList();
                                foreach (var x in costicdfind)
                                {
                                    model.AGlMaster.DEBITCD = x.COSTECD;
                                }
                                model.AGlMaster.REMARKS = "PAYABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                model.AGlMaster.DEBITAMT = rpdata.AMOUNT;
                                model.AGlMaster.CREDITAMT = 0;
                                model.AGlMaster.TABLEID = "RPDRCR";
                                model.AGlMaster.USERPC = rpdata.USERPC;
                                model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                model.AGlMaster.INSTIME = rpdata.INSTIME;
                                model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;
                                if (model.AGlMaster.DEBITCD == 0)
                                {

                                }
                                else
                                {
                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();
                                }



                                model.AGlMaster.COMPID = rpdata.COMPID;
                                model.AGlMaster.TRANSTP = "JOUR";
                                model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                model.AGlMaster.TRANSNO = rpdata.TRANSNO;

                                var maxSLfind2 = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());
                                if (maxSLfind2.ToString().Substring(0, 1) == "5")
                                {
                                    model.AGlMaster.TRANSSL = maxSLfind2 + 1;
                                }


                                model.AGlMaster.TRANSDRCR = "CREDIT";
                                model.AGlMaster.TRANSFOR = "CARD";
                                model.AGlMaster.TRANSMODE = "CASH";
                                model.AGlMaster.COSTPID = rpdata.CARDPID;
                                model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);

                                model.AGlMaster.DEBITCD = rpdata.ACCOUNTCD;


                                foreach (var x in costicdfind)
                                {
                                    model.AGlMaster.CREDITCD = x.COSTECD;
                                }
                                model.AGlMaster.REMARKS = "PAYABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                model.AGlMaster.DEBITAMT = 0;
                                model.AGlMaster.CREDITAMT = rpdata.AMOUNT;
                                model.AGlMaster.TABLEID = "RPDRCR";
                                model.AGlMaster.USERPC = rpdata.USERPC;
                                model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                model.AGlMaster.INSTIME = rpdata.INSTIME;
                                model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;
                                if (model.AGlMaster.CREDITCD == 0)
                                {

                                }
                                else
                                {
                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();
                                }



                                if (rpdata.AMTCASH > 0)
                                {
                                    model.AGlMaster.COMPID = rpdata.COMPID;
                                    model.AGlMaster.TRANSTP = "MPAY";
                                    model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                    model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                    model.AGlMaster.TRANSNO = rpdata.TRANSNO;

                                    var maxSLfind3 = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());
                                    if (maxSLfind3.ToString().Substring(0, 1) == "5")
                                    {
                                        model.AGlMaster.TRANSSL = maxSLfind3 + 1;
                                    }

                                    model.AGlMaster.TRANSDRCR = "DEBIT";
                                    model.AGlMaster.TRANSFOR = "CARD";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = rpdata.CARDPID;
                                    model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);

                                    model.AGlMaster.CREDITCD = Convert.ToInt64(Convert.ToString(compid) + "102010100001");
                                    model.AGlMaster.DEBITCD = rpdata.ACCOUNTCD;


                                    model.AGlMaster.REMARKS = "PAYABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                    model.AGlMaster.DEBITAMT = rpdata.AMTCASH;
                                    model.AGlMaster.CREDITAMT = 0;
                                    model.AGlMaster.TABLEID = "RPDRCR";
                                    model.AGlMaster.USERPC = rpdata.USERPC;
                                    model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                    model.AGlMaster.INSTIME = rpdata.INSTIME;
                                    model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                    model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();


                                    model.AGlMaster.COMPID = rpdata.COMPID;
                                    model.AGlMaster.TRANSTP = "MPAY";
                                    model.AGlMaster.TRANSDT = rpdata.TRANSDT;
                                    model.AGlMaster.TRANSMY = rpdata.TRANSMY;
                                    model.AGlMaster.TRANSNO = rpdata.TRANSNO;

                                    var maxSLfind4 = Convert.ToInt64((from n in db.GlMasterDbSet where n.COMPID == rpdata.COMPID && n.TRANSDT == model.AGlMaster.TRANSDT select n.TRANSSL).Max());
                                    if (maxSLfind4.ToString().Substring(0, 1) == "5")
                                    {
                                        model.AGlMaster.TRANSSL = maxSLfind4 + 1;
                                    }


                                    model.AGlMaster.TRANSDRCR = "CREDIT";
                                    model.AGlMaster.TRANSFOR = "CARD";
                                    model.AGlMaster.TRANSMODE = "CASH";
                                    model.AGlMaster.COSTPID = rpdata.CARDPID;
                                    model.AGlMaster.CARDID = Convert.ToString(rpdata.CARDID);

                                    model.AGlMaster.CREDITCD = rpdata.ACCOUNTCD;



                                    model.AGlMaster.DEBITCD = Convert.ToInt64(Convert.ToString(compid) + "102010100001");

                                    model.AGlMaster.REMARKS = "PAYABLE - " + rpdata.TICKETNO + "," + rpdata.TICKETDT;
                                    model.AGlMaster.DEBITAMT = 0;
                                    model.AGlMaster.CREDITAMT = rpdata.AMTCASH;
                                    model.AGlMaster.TABLEID = "RPDRCR";
                                    model.AGlMaster.USERPC = rpdata.USERPC;
                                    model.AGlMaster.INSUSERID = rpdata.INSUSERID;
                                    model.AGlMaster.INSTIME = rpdata.INSTIME;
                                    model.AGlMaster.INSIPNO = rpdata.INSIPNO;
                                    model.AGlMaster.INSLTUDE = rpdata.INSLTUDE;

                                    db.GlMasterDbSet.Add(model.AGlMaster);
                                    db.SaveChanges();

                                }

                            }
                        }
                        TempData["message"] = "Process Completed for Date : " + model.AGlMaster.TRANSDT;

                    }
                    else if (checkDate_RPDRCR.Count == 0)
                    {
                        var forremovedata = (from l in db.GlMasterDbSet
                                             where l.COMPID == compid && l.TRANSDT == model.AGlMaster.TRANSDT && l.TABLEID == "RPDRCR"
                                             select l).ToList();

                        foreach (var VARIABLE in forremovedata)
                        {

                            db.GlMasterDbSet.Remove(VARIABLE);

                        }



                        db.SaveChanges();
                    }
                  
                   else 
                    {
                        TempData["message"] = "No no...process would not done";

                    }

                }

            }
            else
            {
                TempData["message"] = null;
            }




            return RedirectToAction("Index");
        }


    }
}
