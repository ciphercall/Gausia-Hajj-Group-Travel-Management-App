using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Travel.Models;
using Travel.Models.DTO;

namespace Travel.Controllers.Account
{
    public class AccountController : AppController
    {
        TravelDbContext db = new TravelDbContext();

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;
        public AccountController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];

            ViewData["HighLight_Menu_BillingForm"] = "Heigh Light Menu";
        }






        public ASL_LOG aslLog = new ASL_LOG();
        

        // SAVE ALL INFORMATION from grid(GLAcchart data) TO Asl_lOG Database Table.
        public void Insert_Account_LogData(GlAcchartDTO model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.COMPID && n.USERID == model.INSUSERID select n.LOGSLNO).Max());
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
            aslLog.TABLEID = "GL_ACCHART";

            string parentAccountName = "", headType = "", branchName="",ParentLevel="";

            var findParentAccount = (from m in db.GlAcchartDbSet
                where m.COMPID == model.COMPID && m.ACCOUNTCD == model.ACCOUNTCD && m.HEADTP == model.HEADTP
                select new {m.ACCOUNTNM,m.HLEVELCD}).Distinct().ToList();
            foreach (var xx in findParentAccount)
            {
                parentAccountName = xx.ACCOUNTNM;
                ParentLevel = xx.HLEVELCD.ToString();
            }

            var find_branchName = (from n in db.Asl_BranchDbSet where n.COMPID == model.COMPID && n.BRANCHID==model.BRANCHID select new{n.BRANCHNM}).ToList();
            foreach (var item in find_branchName)
            {
                branchName = item.BRANCHNM.ToString();
            }

            if (model.HEADTP == 1)
            {
                headType = "ASSET";
            }
            else if (model.HEADTP == 2)
            {
                headType = "LIABILITY";
            }
            else if (model.HEADTP == 3)
            {
                headType = "INCOME";
            }
            else if (model.HEADTP == 4)
            {
                headType = "EXPENDETURE";
            }

            aslLog.LOGDATA = Convert.ToString("Chat of Accounts information page. Type: " + headType + ",\nParent Account: " + parentAccountName + ",\nParentLevel: " + ParentLevel + ",\nBranch: " + branchName + ",\nChildLevel: " + model.HLEVELCD + ",\nChild Account: " + model.ACCOUNTNM + ",\nAccount CD: " + model.ACCOUNTCD + ",\nControl CD: " + model.CONTROLCD + ",\nStatus: " + model.HSTATUS +  ".");
            aslLog.USERPC = model.USERPC;

            db.AslLogDbSet.Add(aslLog);
            db.SaveChanges();
        }






        // Edit ALL INFORMATION from Grid(GLAcchart data) TO Asl_lOG Database Table.
        public void Update_Account_LogData(GlAcchartDTO model)
        {
            TimeZoneInfo timeZoneInfo;
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime PrintDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            var date = Convert.ToString(PrintDate.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(PrintDate.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.COMPID && n.USERID == model.INSUSERID select n.LOGSLNO).Max());
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
            aslLog.LOGTYPE = "UPDATE";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = ipAddress.ToString();
            aslLog.LOGLTUDE = model.INSLTUDE;
            aslLog.TABLEID = "GL_ACCHART";

            string parentAccountName = "", headType = "", branchName = "", ParentLevel = "";

            var findParentAccount = (from m in db.GlAcchartDbSet
                                     where m.COMPID == model.COMPID && m.ACCOUNTCD == model.ACCOUNTCD && m.HEADTP == model.HEADTP
                                     select new { m.ACCOUNTNM, m.HLEVELCD }).Distinct().ToList();
            foreach (var xx in findParentAccount)
            {
                parentAccountName = xx.ACCOUNTNM;
                ParentLevel = xx.HLEVELCD.ToString();
            }

            var find_branchName = (from n in db.Asl_BranchDbSet where n.COMPID == model.COMPID && n.BRANCHID == model.BRANCHID select new { n.BRANCHNM }).ToList();
            foreach (var item in find_branchName)
            {
                branchName = item.BRANCHNM.ToString();
            }

            if (model.HEADTP == 1)
            {
                headType = "ASSET";
            }
            else if (model.HEADTP == 2)
            {
                headType = "LIABILITY";
            }
            else if (model.HEADTP == 3)
            {
                headType = "INCOME";
            }
            else if (model.HEADTP == 4)
            {
                headType = "EXPENDETURE";
            }


            aslLog.LOGDATA = Convert.ToString("Chat of Accounts information page. Type: " + headType + ",\nParent Account: " + parentAccountName + ",\nParentLevel: " + ParentLevel + ",\nBranch: " + branchName + ",\nChildLevel: " + model.HLEVELCD + ",\nChild Account: " + model.ACCOUNTNM + ",\nAccount CD: " + model.ACCOUNTCD + ",\nControl CD: " + model.CONTROLCD + ",\nStatus: " + model.HSTATUS + ".");
            aslLog.USERPC = strHostName;

            db.AslLogDbSet.Add(aslLog);
            db.SaveChanges();
        }








      






        






        // GET: Index
        public ActionResult ChartOfAccounts()
        {
            return View();
        }




        //Account name autocomplete (Create Page)
        public JsonResult TagSearch_Account(string term, string compid, string type)
        {
            Int64 companyid = Convert.ToInt64(compid);
            Int64 Type = Convert.ToInt64(type);
            List<string> result = new List<string>();
            var tags = from p in db.GlAcchartDbSet where p.COMPID == companyid && p.HEADTP == Type && p.HLEVELCD!=5 select new { p.ACCOUNTNM, p.ACCOUNTCD, p.HLEVELCD };
            foreach (var tag in tags)
            {
                result.Add(tag.ACCOUNTNM.ToString() + " | L-" + tag.HLEVELCD.ToString());
            }


            return this.Json(result.Where(t => t.StartsWith(term)), JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult keyword_Account(string changedText, string companyid, string type)
        {
            var compid = Convert.ToInt16(companyid);
            Int64 Type = Convert.ToInt64(type);
            String itemId = "";

            // string changedText = Convert.ToString(changedText1);

            List<string> accountList = new List<string>();
            var tags = from p in db.GlAcchartDbSet where p.COMPID == compid && p.HEADTP == Type && p.HLEVELCD != 5 select new { p.ACCOUNTNM, p.ACCOUNTCD, p.HLEVELCD };
            foreach (var tag in tags)
            {
                accountList.Add(tag.ACCOUNTNM.ToString() + " | L-" + tag.HLEVELCD.ToString());
            }


            var rt = accountList.Where(t => t.StartsWith(changedText)).ToList();

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
                        string ss = Convert.ToString(n);
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

            String accountNM = "";
            Int64 level = 0, accountCD = 0;

            string[] multi = itemId.Split('|');
            foreach (var multiEMail in multi)
            {
                accountNM = multiEMail.ToString();
                break;
            }

            var findpatientInfo = db.GlAcchartDbSet.Where(n => n.ACCOUNTNM == accountNM && n.HEADTP == Type &&
                                                      n.COMPID == compid && n.HLEVELCD != 5).Select(n => new
                                                      {
                                                          n.ACCOUNTCD,
                                                          n.ACCOUNTNM,
                                                          n.HLEVELCD,

                                                      });
            foreach (var n in findpatientInfo)
            {
                accountCD = Convert.ToInt64(n.ACCOUNTCD);
                accountNM = Convert.ToString(n.ACCOUNTNM);
                level = n.HLEVELCD;
            }






            var result = new
            {
                ACCOUNTCD = accountCD,
                ACCOUNTNM = accountNM,
                HLEVELCD = level,
            };
            return Json(result, JsonRequestBehavior.AllowGet);

        }




        public ActionResult AccountsReport()
        {
            return View();
        }


    }
}