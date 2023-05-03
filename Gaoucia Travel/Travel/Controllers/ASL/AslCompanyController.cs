using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Travel.Models;
using Travel.Models.ASL;

using Travel.Models.Account;

namespace Travel.Controllers
{
    public class AslCompanyController : Controller
    {
        private TravelDbContext db = new TravelDbContext();


        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        public AslCompanyController()
        {
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }

        //
        // GET: /AslCompany/

        public ActionResult Index()
        {
            return View(db.AslCompanyDbSet.ToList());
        }

        //
        // GET: /AslCompany/Details/5

        public ActionResult Details(short id = 0)
        {
            AslCompany aslcompany = db.AslCompanyDbSet.Find(id);
            if (aslcompany == null)
            {
                return HttpNotFound();
            }
            return View(aslcompany);
        }

        //
        // GET: /AslCompany/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AslCompany/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AslCompany aslcompany)
        {
            if (ModelState.IsValid)
            {
                AslUserco aslUserco = new AslUserco();
                Asl_Branch aslbranch=new Asl_Branch();
                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];


                aslcompany.USERPC = strHostName;
                aslcompany.INSIPNO = ipAddress.ToString();
                aslcompany.INSTIME = Convert.ToDateTime(td);

                var id = Convert.ToString(db.AslCompanyDbSet.Max(s => s.COMPID));

                if (id == "")
                {
                    aslcompany.COMPID = 101;

                    aslbranch.COMPID = Convert.ToInt16(aslcompany.COMPID);
                    aslbranch.ADDRESS = aslcompany.ADDRESS;
                    TempData["Branch"] = aslbranch;

                    aslUserco.COMPID = aslcompany.COMPID;
                    aslUserco.ADDRESS = aslcompany.ADDRESS;
                    aslUserco.MOBNO = aslcompany.CONTACTNO;
                    aslUserco.EMAILID = aslcompany.EMAILID;
                    aslUserco.LOGINPW = aslcompany.COMPNM.Substring(0, 2) + "asl" + "123%";
                    TempData["User"] = aslUserco;//pass the object(reference) to ' TepmData["User"] ' method and also pass TepmData["User"] value to "AslUserco" Create.Cshtml.(this is working same as session)
                    TempData["companyName"] = aslcompany.COMPNM.ToString();// pass this  TempData["COMPNM"] value to "AslUserco" Create.Cshtml.
                    aslcompany.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);//Log in Admin(Super Admin) userID save AslCompany attribute (INSUSERID) filed

                    db.AslCompanyDbSet.Add(aslcompany);
                    if (db.SaveChanges() > 0)
                    {
                        ViewBag.Message = "'" + aslcompany.COMPNM +
                                          "' successfully registered ";
                    }


             

                        // Auto Insert in GL_ACCHART table
                        GL_ACCHART obj = new GL_ACCHART();
                    //Asset
                        obj.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj.HEADTP = 1;
                        obj.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "100000000000"));
                        obj.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj.ACCOUNTNM = "ASSET";
                        obj.BRANCHID = 0;
                        obj.HLEVELCD = 1;
                        obj.HDRCRTP = "D";
                        obj.HPOSTTP = "N";
                        obj.HSTATUS = "A";

                        obj.USERPC = strHostName;
                        obj.INSIPNO = ipAddress.ToString();
                        obj.INSTIME = Convert.ToDateTime(td);
                        obj.INSLTUDE = aslcompany.INSLTUDE;
                        obj.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj);
                        db.SaveChanges();
                        GL_ACCHART obj2 = new GL_ACCHART();
                        //Liability
                        obj2.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj2.HEADTP = 2;
                        obj2.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "200000000000"));
                        obj2.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj2.ACCOUNTNM = "LIABILITY";
                        obj2.BRANCHID = 0;
                        obj2.HLEVELCD = 1;
                        obj2.HDRCRTP = "C";
                        obj2.HPOSTTP = "N";
                        obj2.HSTATUS = "A";

                        obj2.USERPC = strHostName;
                        obj2.INSIPNO = ipAddress.ToString();
                        obj2.INSTIME = Convert.ToDateTime(td);
                        obj2.INSLTUDE = aslcompany.INSLTUDE;
                        obj2.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj2);
                        db.SaveChanges();
                    //INCOME
                        GL_ACCHART obj3 = new GL_ACCHART();
                        obj3.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj3.HEADTP = 3;
                        obj3.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "300000000000"));
                        obj3.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj3.ACCOUNTNM = "INCOME";
                        obj3.BRANCHID = 0;
                        obj3.HLEVELCD = 1;
                        obj3.HDRCRTP = "C";
                        obj3.HPOSTTP = "N";
                        obj3.HSTATUS = "A";

                        obj3.USERPC = strHostName;
                        obj3.INSIPNO = ipAddress.ToString();
                        obj3.INSTIME = Convert.ToDateTime(td);
                        obj3.INSLTUDE = aslcompany.INSLTUDE;
                        obj3.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj3);
                        db.SaveChanges();
                    //EXPENDITURE
                        GL_ACCHART obj4 = new GL_ACCHART();
                        obj4.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj4.HEADTP = 4;
                        obj4.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "400000000000"));
                        obj4.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj4.ACCOUNTNM = "EXPENDITURE";
                        obj4.BRANCHID = 0;
                        obj4.HLEVELCD = 1;
                        obj4.HDRCRTP = "D";
                        obj4.HPOSTTP = "N";
                        obj4.HSTATUS = "A";

                        obj4.USERPC = strHostName;
                        obj4.INSIPNO = ipAddress.ToString();
                        obj4.INSTIME = Convert.ToDateTime(td);
                        obj4.INSLTUDE = aslcompany.INSLTUDE;
                        obj4.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj4);

                        db.SaveChanges();
                //    }

                }
                else if (id != null)
                {
                    Int64 nid = Int64.Parse(id);
                    if (nid < 200)
                    {
                        aslcompany.COMPID = nid + 1;

                        aslbranch.COMPID = Convert.ToInt16(aslcompany.COMPID);
                        aslbranch.ADDRESS = aslcompany.ADDRESS;
                        TempData["Branch"] = aslbranch;


                        aslUserco.COMPID = aslcompany.COMPID;
                        aslUserco.ADDRESS = aslcompany.ADDRESS;
                        aslUserco.MOBNO = aslcompany.CONTACTNO;
                        aslUserco.EMAILID = aslcompany.EMAILID;
                        aslUserco.LOGINPW = aslcompany.COMPNM.Substring(0, 3) + "asl" + "123%";
                        TempData["User"] = aslUserco;
                            //pass the object(reference) to ' TepmData["User"] ' method and also pass TepmData["User"] value to "AslUserco" Create.Cshtml.(this is working same as session)

                        TempData["companyName"] = aslcompany.COMPNM.ToString();
                            // pass this  TempData["COMPNM"] value to "AslUserco" Create.Cshtml.

                        //Log in Admin(Super Admin) userID save AslCompany attribute (INSUSERID) filed
                        aslcompany.INSUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);

                        db.AslCompanyDbSet.Add(aslcompany);
                        if (db.SaveChanges() > 0)
                        {
                            ViewBag.Message = "'" + aslcompany.COMPNM +
                                              "' successfully registered! ";
                        }



                        // Auto Insert in GL_ACCHART table
                        GL_ACCHART obj = new GL_ACCHART();
                        //Asset
                        obj.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj.HEADTP = 1;
                        obj.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "100000000000"));
                        obj.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj.ACCOUNTNM = "ASSET";
                        obj.BRANCHID = 0;
                        obj.HLEVELCD = 1;
                        obj.HDRCRTP = "D";
                        obj.HPOSTTP = "N";
                        obj.HSTATUS = "A";

                        obj.USERPC = strHostName;
                        obj.INSIPNO = ipAddress.ToString();
                        obj.INSTIME = Convert.ToDateTime(td);
                        obj.INSLTUDE = aslcompany.INSLTUDE;
                        obj.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj);
                        db.SaveChanges();
                        GL_ACCHART obj2 = new GL_ACCHART();
                        //Liability
                        obj2.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj2.HEADTP = 2;
                        obj2.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "200000000000"));
                        obj2.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj2.ACCOUNTNM = "LIABILITY";
                        obj2.BRANCHID = 0;
                        obj2.HLEVELCD = 1;
                        obj2.HDRCRTP = "C";
                        obj2.HPOSTTP = "N";
                        obj2.HSTATUS = "A";

                        obj2.USERPC = strHostName;
                        obj2.INSIPNO = ipAddress.ToString();
                        obj2.INSTIME = Convert.ToDateTime(td);
                        obj2.INSLTUDE = aslcompany.INSLTUDE;
                        obj2.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj2);
                        db.SaveChanges();
                        //INCOME
                        GL_ACCHART obj3 = new GL_ACCHART();
                        obj3.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj3.HEADTP = 3;
                        obj3.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "300000000000"));
                        obj3.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj3.ACCOUNTNM = "INCOME";
                        obj3.BRANCHID = 0;
                        obj3.HLEVELCD = 1;
                        obj3.HDRCRTP = "C";
                        obj3.HPOSTTP = "N";
                        obj3.HSTATUS = "A";

                        obj3.USERPC = strHostName;
                        obj3.INSIPNO = ipAddress.ToString();
                        obj3.INSTIME = Convert.ToDateTime(td);
                        obj3.INSLTUDE = aslcompany.INSLTUDE;
                        obj3.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj3);
                        db.SaveChanges();
                        //EXPENDITURE
                        GL_ACCHART obj4 = new GL_ACCHART();
                        obj4.COMPID = Convert.ToInt64(aslcompany.COMPID);
                        obj4.HEADTP = 4;
                        obj4.ACCOUNTCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "400000000000"));
                        obj4.CONTROLCD = Convert.ToInt64(Convert.ToString(aslcompany.COMPID + "000000000000"));
                        obj4.ACCOUNTNM = "EXPENDITURE";
                        obj4.BRANCHID = 0;
                        obj4.HLEVELCD = 1;
                        obj4.HDRCRTP = "D";
                        obj4.HPOSTTP = "N";
                        obj4.HSTATUS = "A";

                        obj4.USERPC = strHostName;
                        obj4.INSIPNO = ipAddress.ToString();
                        obj4.INSTIME = Convert.ToDateTime(td);
                        obj4.INSLTUDE = aslcompany.INSLTUDE;
                        obj4.INSUSERID = Convert.ToInt64(aslcompany.INSUSERID);

                        db.GlAcchartDbSet.Add(obj4);
                        db.SaveChanges();

                    }
                }
                else
                {
                    ViewBag.Message = " Company entry not possible!";
                    return View(aslcompany);
                }
              

                //Send Company Information to Company Admin Mail Address
                MailMessage mail = new MailMessage();
                mail.To.Add(aslcompany.EMAILID);
                mail.From = new MailAddress("admin@alchemy-bd.com");
                mail.Subject = "Mail Confirmation";
                mail.Body = "Alchemy Restaurant Management Online Registration System.\n\n" + Environment.NewLine + "Hi, " + aslcompany.COMPNM + Environment.NewLine + "This Company successfully created our management system. " + Environment.NewLine
                            + "Company Address: " + aslcompany.ADDRESS + Environment.NewLine + "Company Contact No: " + aslcompany.CONTACTNO + Environment.NewLine + "Company EmailID: " + aslcompany.EMAILID + Environment.NewLine
                            + "Company Web ID: " + aslcompany.WEBID + Environment.NewLine + "\nStay with us," + Environment.NewLine + "Alchemy Software";
                mail.Priority = MailPriority.Normal;

                SmtpClient client = new SmtpClient();
                client.Host = "mail.alchemy-bd.com";
                client.Credentials = new NetworkCredential("admin@alchemy-bd.com", "Asl.admin@&123%");
                client.EnableSsl = false;
                client.Send(mail);

               

                //return RedirectToAction("Create", "Asl_Branch", TempData["Branch"]);
                return RedirectToAction("Create");
            }

            return View(aslcompany);
        }


        //
        // GET: /AslCompany/Edit/5

        public ActionResult Edit(short id = 0)
        {
            AslCompany aslcompany = db.AslCompanyDbSet.Find(id);
            if (aslcompany == null)
            {
                return HttpNotFound();
            }
            return View(aslcompany);
        }

        //
        // POST: /AslCompany/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AslCompany aslcompany)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aslcompany).State = EntityState.Modified;

                //Get Ip ADDRESS,Time & user PC Name
                string strHostName = System.Net.Dns.GetHostName();
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];

                aslcompany.USERPC = strHostName;
                aslcompany.UPDIPNO = ipAddress.ToString();
                aslcompany.UPDTIME = Convert.ToDateTime(td);
                aslcompany.UPDUSERID = Convert.ToInt64(System.Web.HttpContext.Current.Session["loggedUserID"]);


                //if (aslcompany.STATUS == "I")
                //{
                //    //User database list Update
                //    var companAllInfo = (from user in db.AslUsercoDbSet
                //        select new {user})
                //        .Where(e => e.user.COMPID == aslcompany.COMPID);



                //    foreach (var update in companAllInfo)
                //    {
                //        update.user.STATUS = "I";
                //        db.Entry(update.user).State = EntityState.Modified;

                //    }
                //    db.SaveChanges();
                //}
                //else
                //{

                //}

                db.SaveChanges();
                ViewBag.Message = "'" + aslcompany.COMPNM + "' successfully updated ";
                return View(aslcompany);
            }
            return View(aslcompany);
        }

        //
        // GET: /AslCompany/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AslCompany aslcompany = db.AslCompanyDbSet.Find(id);
            if (aslcompany == null)
            {
                return HttpNotFound();
            }
            return View(aslcompany);
        }

        //
        // POST: /AslCompany/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AslCompany aslcompany = db.AslCompanyDbSet.Find(id);

            ////User database list Delete
            //var companAllInfo = (from user in db.AslUsercoDbSet
            //                     from role in db.AslRoleDbSet
            //                     from catagoryName in db.PosItemMstDbSet
            //                     from catagoryItem in db.RmsItemDbSet
            //                     select new { user, role, catagoryName, catagoryItem })
            //    .Where(e => e.user.COMPID == aslcompany.COMPID && e.role.COMPID == aslcompany.COMPID && e.catagoryName.COMPID == aslcompany.COMPID && e.catagoryItem.COMPID == aslcompany.COMPID);



            //foreach (var remove in companAllInfo)
            //{
            //    db.AslUsercoDbSet.Remove(remove.user);
            //    db.AslRoleDbSet.Remove(remove.role);
            //    db.PosItemMstDbSet.Remove(remove.catagoryName);
            //    db.RmsItemDbSet.Remove(remove.catagoryItem);
            //}
            //db.SaveChanges();

            //db.AslCompanyDbSet.Remove(aslcompany);
            //db.SaveChanges();

            return RedirectToAction("Index");
        }


        public JsonResult Check_Compnm(string compnm)
        {
            var result = db.AslCompanyDbSet.Count(d => d.COMPNM == compnm) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Check_ContactNo(string contactNo)
        {
            var result = db.AslCompanyDbSet.Count(d => d.CONTACTNO == contactNo) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Check_EmailId(string emailId)
        {
            var result = db.AslCompanyDbSet.Count(d => d.EMAILID == emailId) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}