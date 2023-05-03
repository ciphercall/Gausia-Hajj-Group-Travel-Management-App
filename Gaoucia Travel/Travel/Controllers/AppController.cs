using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.Controllers
{
    public class AppController : Controller
    {
      
        // GET: /App/
        TravelDbContext db = new TravelDbContext();

        public TravelDbContext DataContext
        {
            get { return db; }
        }

        public AppController()
        {
            //var forslide = 1;
        }
        public static class Global
        {
            public static Int64 GlobalVariable = 1;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
           
            try
            {
                var userid = Convert.ToInt64(Session["loggedUserID"]);
                var comid = Convert.ToInt64(Session["loggedCompID"]);

              

           
                //ViewData["validUserForm"] = from c in db.AslRoleDbSet
                //                       where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP=="F" && c.MODULEID=="01")
                //                       select c;

                //ViewData["validUserReports"] = from c in db.AslRoleDbSet
                //                            where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "R" && c.MODULEID=="01")
                //                            select c;

                //ViewData["validBillingForm"] = (from c in db.AslRoleDbSet
                //                            where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "F" && c.MODULEID == "02")
                //                               select c).OrderBy(x => x.SERIAL);

                //ViewData["validBillingReports"] = (from c in db.AslRoleDbSet
                //                               where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "R" && c.MODULEID == "02")
                //                                   select c).OrderBy(x => x.SERIAL);

                //ViewData["valid_HMS_Form"] = from c in db.AslRoleDbSet
                //                             where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "F" && c.MODULEID == "03")
                //                             select c;

                //ViewData["valid_HMS_Report"] = from c in db.AslRoleDbSet
                //                               where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "R" && c.MODULEID == "03")
                //                               select c;
                
                //ViewData["valid_GL_Form"] = from c in db.AslRoleDbSet
                //                               where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "F" && c.MODULEID == "04")
                //                               select c;

                //ViewData["valid_GL_Report"] = from c in db.AslRoleDbSet
                //                                  where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "R" && c.MODULEID == "04")
                //                                  select c;


                ViewData["Module"] = from c in db.AslMenumstDbSet select c;

                ViewData["Menu"] = from c in db.AslMenuDbSet select c;



                ViewData["validUserForm"] = (from c in db.AslRoleDbSet
                                            where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "F")
                                            select c);

                ViewData["validUserReports"] = (from c in db.AslRoleDbSet
                                               where (c.USERID == userid && c.COMPID == comid && c.STATUS == "A" && c.MENUTP == "R")
                                                select c);



                var findCompanyName = from m in db.AslCompanyDbSet where m.COMPID == comid select new { m.COMPNM };
                string Name = "";
                foreach (var name in findCompanyName)
                {
                    ViewData["CompanyName"] = name.COMPNM;
                }



                //get url
                string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                string[] multi = url.Split('/');

                string controllername = multi[3].ToString(), actionName = "";
                try
                {
                    if (multi[4] != null)
                    {
                        actionName = multi[4].ToString();
                    }
                }
                catch (Exception ex)
                {
                    actionName = "Index";
                }

                if (actionName != "")
                {
                    var getControllerName = (from menu in db.AslMenuDbSet
                                             join module in db.AslMenumstDbSet on menu.MODULEID equals module.MODULEID
                                             where menu.CONTROLLERNAME == controllername && actionName == menu.ACTIONNAME
                                             select new { module.MODULENM, menu.MENUTP }).ToList();
                    foreach (var menu in getControllerName)
                    {
                        ViewData["Highlight_Module"] = menu.MODULENM;
                        ViewData["Highlight_Module_Type"] = menu.MENUTP;
                        break;
                    }
                }

            }
            catch
            {

            }
        }

    }
}
