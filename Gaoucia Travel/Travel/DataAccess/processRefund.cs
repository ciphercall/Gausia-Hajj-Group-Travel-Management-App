using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Travel.Models;

namespace Travel.DataAccess
{
    public class processRefund
    {
        public static string refundProcess(DateTime TransDate)
        {
            IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
            DateTime td;


            TravelDbContext db = new TravelDbContext();
            //Get Ip ADDRESS,Time & user PC Name
            string strHostName;
            IPHostEntry ipHostInfo;
            IPAddress ipAddress;

            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

            Int64 compid = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedCompID"]);

            var checkdata = (from n in db.refundDbSet where n.TRANSDT == TransDate && n.COMPID == compid select n).OrderBy(x => x.TRANSNO).ToList();

            string converttoString = Convert.ToString(TransDate.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(9, 2);
            string getMonth = converttoString.Substring(3, 3);
            string Month = getMonth + "-" + getYear;

            if (checkdata.Count != 0)
            {
                var forremovedata = (from l in db.GlMasterDbSet
                                     where l.COMPID == compid && l.TRANSDT == TransDate && l.TABLEID == "TAMS_REFUND"
                                     select l).ToList();

                foreach (var VARIABLE in forremovedata)
                {

                    db.GlMasterDbSet.Remove(VARIABLE);

                }



                db.SaveChanges();
                foreach (var x in checkdata)
                {


                    if (x.REFUNDS != 0)
                    {
                        Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                            where a.COMPID == compid && a.TRANSTP == "JOUR" && a.TRANSDT == TransDate
                                                            select a.TRANSSL).Max());
                        GL_MASTER model = new GL_MASTER();
                        if (maxSlCheck == 0)
                        {
                            model.TRANSSL = 60001;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            model.DEBITCD = x.ACCOUNTCD;//Supplier
                            var expense =
                                (from n in db.GLCostPMSTDbSet
                                 where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                 select n).ToList();
                            Int64 credit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                credit = glCostpmst.COSTECD;
                            }
                            model.CREDITCD = credit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + x.REMARKS;
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.REFUNDS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = 60002;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            Int64 debit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                debit = glCostpmst.COSTECD;
                            }
                            model.DEBITCD = debit;
                            model.CREDITCD = x.ACCOUNTCD;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.REFUNDS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();

                        }
                        else
                        {
                            model.TRANSSL = maxSlCheck + 1;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            model.DEBITCD = x.ACCOUNTCD;//Supplier
                            var expense =
                                (from n in db.GLCostPMSTDbSet
                                 where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                 select n).ToList();
                            Int64 credit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                credit = glCostpmst.COSTECD;
                            }
                            model.CREDITCD = credit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.REFUNDS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = maxSlCheck + 2;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            Int64 debit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                debit = glCostpmst.COSTECD;
                            }
                            model.DEBITCD = debit;
                            model.CREDITCD = x.ACCOUNTCD;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.REFUNDS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();
                        }

                    }
                    if (x.DEDAMTS != 0)
                    {
                        Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                            where a.COMPID == compid && a.TRANSTP == "JOUR" && a.TRANSDT == TransDate
                                                            select a.TRANSSL).Max());
                        GL_MASTER model = new GL_MASTER();
                        if (maxSlCheck == 0)
                        {
                            model.TRANSSL = 60001;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            model.DEBITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001");
                            var expense =
                               (from n in db.GLCostPMSTDbSet
                                where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                select n).ToList();
                            Int64 credit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                credit = glCostpmst.COSTECD;
                            }
                            model.CREDITCD = credit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.DEDAMTS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = 60002;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            Int64 debit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                debit = glCostpmst.COSTECD;
                            }
                            model.DEBITCD = debit;
                            model.CREDITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001"); ;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.DEDAMTS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();

                        }
                        else
                        {
                            model.TRANSSL = maxSlCheck + 1;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            model.DEBITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001");
                            var expense =
                               (from n in db.GLCostPMSTDbSet
                                where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                select n).ToList();
                            Int64 credit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                credit = glCostpmst.COSTECD;
                            }
                            model.CREDITCD = credit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.REFUNDS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = maxSlCheck + 2;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            Int64 debit = 0;
                            foreach (var glCostpmst in expense)
                            {
                                debit = glCostpmst.COSTECD;
                            }
                            model.DEBITCD = debit;
                            model.CREDITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001");
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.REFUNDS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();
                        }

                    }
                    if (x.REFUNDT != 0)
                    {
                        Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                            where a.COMPID == compid && a.TRANSTP == "JOUR" && a.TRANSDT == TransDate
                                                            select a.TRANSSL).Max());
                        GL_MASTER model = new GL_MASTER();
                        if (maxSlCheck == 0)
                        {
                            model.TRANSSL = 60001;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            var Income =
                               (from n in db.GLCostPMSTDbSet
                                where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                select n).ToList();
                            Int64 debit = 0;
                            foreach (var glCostpmst in Income)
                            {
                                debit = glCostpmst.COSTICD;
                            }

                            model.DEBITCD = debit;

                            Int64 cardid = Convert.ToInt64(x.CARDID);
                            var receivable_code =
                               (from n in db.RPDRCRDbSet
                                where n.COMPID == x.COMPID && n.CARDID == cardid && n.TRANSFOR == "RECEIVABLE"
                                select n).ToList();
                            Int64 credit = 0;
                            foreach (var rpdrcr in receivable_code)
                            {
                                credit = rpdrcr.ACCOUNTCD;
                            }

                            model.CREDITCD = credit;

                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.DEDAMTS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = 60002;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;


                            model.DEBITCD = credit;

                            model.CREDITCD = debit;

                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.DEDAMTS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();

                        }
                        else
                        {
                            model.TRANSSL = maxSlCheck + 1;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            var Income =
                                (from n in db.GLCostPMSTDbSet
                                 where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                 select n).ToList();
                            Int64 debit = 0;
                            foreach (var glCostpmst in Income)
                            {
                                debit = glCostpmst.COSTICD;
                            }

                            model.DEBITCD = debit;

                            Int64 cardid = Convert.ToInt64(x.CARDID);
                            var receivable_code =
                               (from n in db.RPDRCRDbSet
                                where n.COMPID == x.COMPID && n.CARDID == cardid && n.TRANSFOR == "RECEIVABLE"
                                select n).ToList();
                            Int64 credit = 0;
                            foreach (var rpdrcr in receivable_code)
                            {
                                credit = rpdrcr.ACCOUNTCD;
                            }

                            model.CREDITCD = credit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.REFUNDS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = maxSlCheck + 2;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            model.DEBITCD = credit;
                            model.CREDITCD = debit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.REFUNDS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();
                        }

                    }
                    if (x.DEDAMTT != 0)
                    {
                        Int64 maxSlCheck = Convert.ToInt64((from a in db.GlMasterDbSet
                                                            where a.COMPID == compid && a.TRANSTP == "JOUR" && a.TRANSDT == TransDate
                                                            select a.TRANSSL).Max());
                        GL_MASTER model = new GL_MASTER();
                        if (maxSlCheck == 0)
                        {
                            model.TRANSSL = 60001;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            var Income =
                               (from n in db.GLCostPMSTDbSet
                                where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                select n).ToList();
                            Int64 debit = 0;
                            foreach (var glCostpmst in Income)
                            {
                                debit = glCostpmst.COSTICD;
                            }
                            model.DEBITCD = debit;
                            model.CREDITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001");
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.DEDAMTS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = 60002;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            model.DEBITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001"); ;
                            model.CREDITCD = debit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.DEDAMTS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();

                        }
                        else
                        {
                            model.TRANSSL = maxSlCheck + 1;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            var Income =
                                (from n in db.GLCostPMSTDbSet
                                 where n.COMPID == x.COMPID && n.COSTCID == x.CARDCID
                                 select n).ToList();
                            Int64 debit = 0;
                            foreach (var glCostpmst in Income)
                            {
                                debit = glCostpmst.COSTICD;
                            }
                            model.DEBITCD = debit;
                            model.CREDITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001");
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);
                            model.TRANSDRCR = "DEBIT";
                            model.TABLEID = "TAMS_REFUND";


                            model.DEBITAMT = x.REFUNDS;
                            model.CREDITAMT = 0;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;


                            db.GlMasterDbSet.Add(model);
                            db.SaveChanges();

                            model.TRANSSL = maxSlCheck + 2;

                            model.TRANSDT = x.TRANSDT;
                            model.COMPID = x.COMPID;
                            model.TRANSTP = "JOUR";
                            model.TRANSMY = Month;
                            model.TRANSNO = x.TRANSNO;
                            model.TRANSFOR = "CARD";
                            model.TRANSMODE = "CASH";
                            model.COSTPID = x.CARDCID;
                            model.DEBITCD = Convert.ToInt64(Convert.ToString(x.COMPID) + "3020001"); ;
                            model.CREDITCD = debit;
                            model.CHEQUENO = null;
                            model.CHEQUEDT = null;
                            model.REMARKS = "REFUND - " + Convert.ToString(x.CARDID);

                            model.TRANSDRCR = "CREDIT";
                            model.TABLEID = "TAMS_REFUND";

                            model.DEBITAMT = 0;
                            model.CREDITAMT = x.REFUNDS;

                            model.USERPC = strHostName;
                            model.INSIPNO = ipAddress.ToString();
                            model.INSTIME = Convert.ToDateTime(td);

                            model.INSUSERID =
                                Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);
                            model.INSLTUDE = model.INSLTUDE;

                            db.GlMasterDbSet.Add(model);

                            //Insert_Process_LogData(model);
                            db.SaveChanges();
                        }

                    }


                }

            }
            else
            {
                var forremovedata = (from l in db.GlMasterDbSet
                                     where l.COMPID == compid && l.TRANSDT == TransDate && l.TABLEID == "TAMS_REFUND"
                                     select l).ToList();

                foreach (var VARIABLE in forremovedata)
                {

                    db.GlMasterDbSet.Remove(VARIABLE);

                }



                db.SaveChanges();

            }
            return null;

        }
    }
}