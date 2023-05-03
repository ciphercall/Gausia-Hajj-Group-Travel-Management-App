using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Travel.Controllers.Account;

using Travel.Models;
using Travel.Models.Account;
using Travel.Models.DTO;


namespace Travel.Controllers.Api
{
    public class ApiAccountController : ApiController
    {
        TravelDbContext db = new TravelDbContext();

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        public ApiAccountController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }





        [System.Web.Http.Route("api/ApiAccountController/GetAccountData")]
        [System.Web.Http.HttpGet]
        public IEnumerable<GlAcchartDTO> GetAccountData(string Compid, string Type, string AccountCD)
        {
            Int64 compid = Convert.ToInt64(Compid);
            Int64 type = Convert.ToInt64(Type);
            Int64 accountCD = Convert.ToInt64(AccountCD);
            var find_GridData = (from account in db.GlAcchartDbSet
                                 //join branch in db.Asl_BranchDbSet on account.COMPID equals branch.COMPID
                                 where account.COMPID == compid && account.HEADTP == type && account.CONTROLCD == accountCD
                                 select new
                                 {
                                     account.ID,
                                     account.COMPID,
                                     account.HEADTP,
                                     account.ACCOUNTCD,
                                     account.CONTROLCD,
                                     account.ACCOUNTNM,
                                     account.HLEVELCD,
                                     account.BRANCHID,

                                     account.INSIPNO,
                                     account.INSLTUDE,
                                     account.INSTIME,
                                     account.INSUSERID,
                                 }).OrderBy(e => e.ACCOUNTCD).ToList();

            if (find_GridData.Count == 0)
            {
                yield return new GlAcchartDTO
                {
                    count = 1, //no data found
                };
            }
            else
            {
                foreach (var s in find_GridData)
                {
                    String branchNm = "";
                    var findBranchName = (from m in db.Asl_BranchDbSet where m.COMPID == compid && m.BRANCHID == s.BRANCHID select new { m.BRANCHNM }).ToList();
                    if (findBranchName.Count != 0)
                    {
                        foreach (var x in findBranchName)
                        {
                            branchNm = x.BRANCHNM;
                        }
                    }


                    yield return new GlAcchartDTO
                    {
                        ID=s.ID,
                        COMPID = Convert.ToInt64(s.COMPID),
                        ACCOUNTCD = Convert.ToInt64(s.ACCOUNTCD),
                        ACCOUNTNM = Convert.ToString(s.ACCOUNTNM),
                        CONTROLCD = s.CONTROLCD,
                        BRANCHID = Convert.ToInt64(s.BRANCHID),
                        BranchName = branchNm,
                        HEADTP = Convert.ToInt64(s.HEADTP),
                        HLEVELCD = s.HLEVELCD,

                        INSUSERID = s.INSUSERID,
                        INSLTUDE = s.INSLTUDE,
                        INSTIME = s.INSTIME,
                        INSIPNO = s.INSIPNO,
                    };
                }
            }
        }









        [Route("api/ApiAccountController/grid/addData")]
        [HttpPost]
        public HttpResponseMessage AddData(GlAcchartDTO model)
        {
            GL_ACCHART glAccount = new GL_ACCHART();
            Int64 level = model.HLEVELCD + 1;
            string hpostTP = "";


            var check_data = (from n in db.GlAcchartDbSet
                              where n.COMPID == model.COMPID && n.HEADTP == model.HEADTP && n.ACCOUNTNM == model.ACCOUNTNM && n.HLEVELCD == level 
                              select n).ToList();
            if (check_data.Count == 0)
            {
                Int64 max_AccountCD = 0;
                try
                {
                    max_AccountCD = Convert.ToInt64((from m in db.GlAcchartDbSet
                                                           where m.COMPID == model.COMPID && m.HEADTP == model.HEADTP
                                                               && m.HLEVELCD == level && m.CONTROLCD == model.ACCOUNTCD
                                                           select m.ACCOUNTCD).Max());
                }
                catch
                {
                    max_AccountCD = model.ACCOUNTCD;
                }
                string accountCD = Convert.ToString(max_AccountCD);
                Int64 getAccountCD_increment = 0;
                
                if (level == 2)
                {
                    string convertAccountCD = accountCD.Substring(0, 4);
                    string convertAccountCD_increment = accountCD.Substring(4, 2);
                    getAccountCD_increment = Convert.ToInt64(convertAccountCD_increment);
                    getAccountCD_increment += 1;
                    string getAccount_Initialize = Convert.ToString(getAccountCD_increment);
                    if (getAccountCD_increment <= 9)
                    {
                        accountCD = convertAccountCD + "0" + getAccount_Initialize + "000000000";
                    }
                    else if (getAccountCD_increment > 9 && getAccountCD_increment <= 99)
                    {
                        accountCD = convertAccountCD + getAccount_Initialize + "000000000";
                    }
                    else
                    {
                        model.Limit = 0;
                        HttpResponseMessage limitResponse = Request.CreateResponse(HttpStatusCode.Created, model);
                        return limitResponse;
                    }
                    hpostTP = "N";
                }
                else if (level == 3)
                {
                    string convertAccountCD = accountCD.Substring(0, 6);
                    string convertAccountCD_increment = accountCD.Substring(6, 2);
                    getAccountCD_increment = Convert.ToInt64(convertAccountCD_increment);
                    getAccountCD_increment += 1;
                    string getAccount_Initialize = Convert.ToString(getAccountCD_increment);
                    if (getAccountCD_increment <= 9)
                    {
                        accountCD = convertAccountCD + "0" + getAccount_Initialize + "0000000";
                    }
                    else if (getAccountCD_increment > 9 && getAccountCD_increment <= 99)
                    {
                        accountCD = convertAccountCD + getAccount_Initialize + "0000000";
                    }
                    hpostTP = "N";
                }
                else if (level == 4)
                {
                    string convertAccountCD = accountCD.Substring(0, 8);
                    string convertAccountCD_increment = accountCD.Substring(8, 2);
                    getAccountCD_increment = Convert.ToInt64(convertAccountCD_increment);
                    getAccountCD_increment += 1;
                    string getAccount_Initialize = Convert.ToString(getAccountCD_increment);
                    if (getAccountCD_increment <= 9)
                    {
                        accountCD = convertAccountCD + "0" + getAccount_Initialize + "00000";
                    }
                    else if (getAccountCD_increment > 9 && getAccountCD_increment <= 99)
                    {
                        accountCD = convertAccountCD + getAccount_Initialize + "00000";
                    }
                    hpostTP = "N";
                }
                else if (level == 5)
                {
                    string convertAccountCD = accountCD.Substring(0, 10);
                    string convertAccountCD_increment = accountCD.Substring(10, 5);
                    getAccountCD_increment = Convert.ToInt64(convertAccountCD_increment);
                    getAccountCD_increment += 1;
                    string getAccount_Initialize = Convert.ToString(getAccountCD_increment);
                    if (getAccountCD_increment <= 9)
                    {
                        accountCD = convertAccountCD + "0000" + getAccount_Initialize;
                    }
                    else if (getAccountCD_increment > 9 && getAccountCD_increment <= 99)
                    {
                        accountCD = convertAccountCD + "000"+getAccount_Initialize;
                    }
                    else if (getAccountCD_increment > 99 && getAccountCD_increment <= 999)
                    {
                        accountCD = convertAccountCD + "00" + getAccount_Initialize;
                    }
                    else if (getAccountCD_increment > 999 && getAccountCD_increment <= 9999)
                    {
                        accountCD = convertAccountCD + "0" + getAccount_Initialize;
                    }
                    else if (getAccountCD_increment > 9999 && getAccountCD_increment <= 99999)
                    {
                        accountCD = convertAccountCD + getAccount_Initialize;
                    }
                    hpostTP = "P";
                }



                glAccount.COMPID = Convert.ToInt16(model.COMPID);
                glAccount.HEADTP = Convert.ToInt64(model.HEADTP);
                glAccount.CONTROLCD = Convert.ToInt64(model.ACCOUNTCD);
                glAccount.ACCOUNTCD = Convert.ToInt64(accountCD);
                glAccount.ACCOUNTNM = Convert.ToString(model.ACCOUNTNM);
                if (model.BRANCHID != 0 || model.BRANCHID != null)
                {
                    glAccount.BRANCHID = Convert.ToInt64(model.BRANCHID);
                }
                else
                {
                    glAccount.BRANCHID = 0;
                }
                glAccount.HLEVELCD = Convert.ToInt64(level);

                if (model.HEADTP == 1)
                {
                    glAccount.HDRCRTP = "D";
                }
                else if (model.HEADTP == 2)
                {
                    glAccount.HDRCRTP = "C";
                }
                else if (model.HEADTP == 3)
                {
                    glAccount.HDRCRTP = "C";
                }
                else if (model.HEADTP == 4)
                {
                    glAccount.HDRCRTP = "D";
                }
                glAccount.HPOSTTP = hpostTP;
                glAccount.HSTATUS = "A";

                glAccount.USERPC = strHostName;
                glAccount.INSIPNO = ipAddress.ToString();
                glAccount.INSTIME = Convert.ToDateTime(td);
                glAccount.INSUSERID = model.INSUSERID;
                glAccount.INSLTUDE = Convert.ToString(model.INSLTUDE);
                if((glAccount.ACCOUNTCD!=0 || glAccount.ACCOUNTCD!=null) && (glAccount.CONTROLCD!=0|| glAccount.CONTROLCD!=null))
                {
                    db.GlAcchartDbSet.Add(glAccount);
                    db.SaveChanges();
                }
              

                model.CONTROLCD = glAccount.CONTROLCD;
                model.ACCOUNTCD = glAccount.ACCOUNTCD;
                model.HLEVELCD = glAccount.HLEVELCD;
                model.HDRCRTP = glAccount.HDRCRTP;
                model.HPOSTTP = glAccount.HPOSTTP;
                model.HSTATUS = glAccount.HSTATUS;
                model.USERPC = glAccount.USERPC;
                model.INSIPNO = glAccount.INSIPNO;
                model.INSTIME = glAccount.INSTIME;
                model.INSUSERID = glAccount.INSUSERID;
                model.INSLTUDE = Convert.ToString(glAccount.INSLTUDE);

                //Log data save from GL_ACCHART Tabel
                AccountController controller = new AccountController();
                controller.Insert_Account_LogData(model);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                return response;
            }
            else
            {
                model.CONTROLCD = 0;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                return response;
            }
        }











        [Route("api/ApiAccountController/grid/UpdateData")]
        [HttpPost]
        public HttpResponseMessage UpdateData(GlAcchartDTO model)
        {
            var check_data = (from n in db.GlAcchartDbSet
                              where n.COMPID == model.COMPID && n.HEADTP == model.HEADTP 
                                  && n.CONTROLCD == model.CONTROLCD && n.HLEVELCD == model.HLEVELCD && n.ACCOUNTNM==model.ACCOUNTNM 
                              select n).ToList();
            if (check_data.Count == 0)
            {
                var data_find = (from n in db.GlAcchartDbSet
                                 where n.COMPID == model.COMPID && n.HEADTP == model.HEADTP && n.ACCOUNTCD==model.ACCOUNTCD
                                     && n.CONTROLCD == model.CONTROLCD && n.HLEVELCD == model.HLEVELCD 
                                 select n).ToList();

                foreach (var item in data_find)
                {
                    item.COMPID = Convert.ToInt16(model.COMPID);

                    item.HEADTP = Convert.ToInt64(model.HEADTP);
                    item.ACCOUNTNM = Convert.ToString(model.ACCOUNTNM);
                    if (model.BRANCHID != 0 || model.BRANCHID!=null)
                    {
                        item.BRANCHID = Convert.ToInt64(model.BRANCHID);
                    }
                    else
                    {
                        item.BRANCHID = 0;
                    }
                  
                    item.USERPC = strHostName;
                    item.UPDIPNO = ipAddress.ToString();
                    item.UPDLTUDE = Convert.ToString(model.INSLTUDE);
                    item.UPDTIME = Convert.ToDateTime(td);
                    item.UPDUSERID = Convert.ToInt64(model.INSUSERID);

                }
                
                db.SaveChanges();

                //Log data save from GL_ACCHART Tabel
                AccountController controller = new AccountController();
                controller.Update_Account_LogData(model);

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                return response;
            }
            else
            {
                model.CONTROLCD = 0;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                return response;
            }
        }









        [Route("api/ApiAccountController/grid/DeleteData")]
        [HttpPost]
        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteData(GlAcchartDTO model)
        {
            GL_ACCHART deleteModel = new GL_ACCHART();
            deleteModel.ID = model.ID;
            deleteModel.COMPID = model.COMPID;
            deleteModel.HEADTP = model.HEADTP;
            deleteModel.ACCOUNTCD = model.ACCOUNTCD;

            deleteModel = db.GlAcchartDbSet.Find(deleteModel.ID, deleteModel.COMPID,deleteModel.HEADTP,deleteModel.ACCOUNTCD);
            var find_data_exists = (from n in db.GlAcchartDbSet where n.COMPID == model.COMPID && n.HEADTP == model.HEADTP && n.CONTROLCD == model.ACCOUNTCD select n).ToList();
            if(find_data_exists.Count>0)
            {
                model.ACCOUNTCD = 0;
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            else
            {
                db.GlAcchartDbSet.Remove(deleteModel);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
           

            //Log data save from GheadMst Tabel
            //MediController mediController = new MediController();
            //mediController.Delete_mediCare_LogData(model);
            //mediController.Delete_mediCare_LogDelete(model);


           
           
        }
    }
}
