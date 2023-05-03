﻿using Travel.Models;
using Travel.Models.Account;
using Travel.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Travel.Controllers.Api
{
    public class ApiMultipleVoucherController : ApiController
    {

        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        TravelDbContext db = new TravelDbContext();

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;
        public ApiMultipleVoucherController()
        {
           
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
          
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }

        [System.Web.Http.Route("api/ApiMultipleVoucher/GetData")]
        [System.Web.Http.HttpGet]
        public IEnumerable<MTransDTO> Get(string Compid, string Branchid,string Transtp,string Transdt,string Month,string Tno, string InUserID, string InsLtude)
        {

            //Get Ip ADDRESS,Time & user PC Name

            if (Transtp != null && Transdt != null && Tno!=null)
            {
                Int64 InsUserID = Convert.ToInt64(InUserID);
                Int64 compid = Convert.ToInt64(Compid);
                Int64 branch = Convert.ToInt64(Branchid);
                Int64 transno = Convert.ToInt64(Tno);


                var ache_kina_data = (from n in db.MtransMasterdbSet where n.COMPID == compid && n.BRANCHID == branch && n.TRANSTP == Transtp && n.TRANSNO == transno select n).ToList();
                if (ache_kina_data.Count == 0)
                {
                    GL_MTRANSMST obj = new GL_MTRANSMST();
                    obj.COMPID = compid;
                    obj.BRANCHID = branch;
                    obj.TRANSTP = Transtp;
                    obj.TRANSDT = Convert.ToDateTime(Transdt);
                    obj.TRANSMY = Month;
                    obj.TRANSNO = transno;

                    obj.USERPC = strHostName;
                    obj.INSIPNO = ipAddress.ToString();
                    obj.INSTIME = Convert.ToDateTime(td);
                    obj.INSUSERID = InsUserID;
                    obj.INSLTUDE = InsLtude;

                    db.MtransMasterdbSet.Add(obj);
                    db.SaveChanges();

                    yield return new MTransDTO
                    {

                        CompanyID = compid,
                        BranchID = branch,
                        Transno = transno,
                        TransType = Transtp,
                        TransMonthYear = Month,
                        TransDate = Transdt,

                        TransSerial = 0



                    };








                }
                else
                {
                    var mtransData = (from t1 in db.MtransdbSet
                                      join t2 in db.MtransMasterdbSet on t1.TRANSNO equals t2.TRANSNO

                                      where t1.COMPID == compid && t1.TRANSTP == Transtp && t1.BRANCHID == branch && t1.TRANSNO == transno 
                                      && t2.TRANSTP == Transtp && t2.BRANCHID==branch && t2.TRANSNO==transno
                                      select new
                                      {
                                          Id = t1.ID,
                                          compid = t1.COMPID,
                                          branchid = t1.BRANCHID,
                                          transtype = t1.TRANSTP,
                                          transdate = t1.TRANSDT,
                                          transmonth = t1.TRANSMY,
                                          transNo = t1.TRANSNO,
                                          transserial = t1.TRANSSL,
                                          transfor = t1.TRANSFOR,
                                          transmode = t1.TRANSMODE,
                                          costpid = t1.COSTPID,
                                          creditcd = t1.CREDITCD,
                                          debitcd = t1.DEBITCD,
                                          chequeno = t1.CHEQUENO,
                                          chequedt = t1.CHEQUEDT,
                                          amount = t1.AMOUNT,
                                          remarks = t1.REMARKS,
                                          cardno=t1.CARDNO



                                      });

               

                    foreach (var item in mtransData)
                    {
                        string conv_chequedt = "";
                        if(item.chequedt!=null)
                        {
                            DateTime abcd = Convert.ToDateTime(item.chequedt);
                             conv_chequedt = abcd.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            conv_chequedt = "";
                        }
                        

                        DateTime abcd2 = Convert.ToDateTime(item.transdate);
                        string conv_transdt = abcd2.ToString("dd-MMM-yyyy");

                        var serchaccount_debit = (from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD == 5 && n.ACCOUNTCD==item.debitcd select n).ToList();
                        var serchaccount_credit = (from n in db.GlAcchartDbSet where n.COMPID == compid && n.HLEVELCD == 5 && n.ACCOUNTCD == item.creditcd select n).ToList();
                        string debitname = "", creditname = "";
                        foreach(var debit in serchaccount_debit)
                        {
                            debitname = debit.ACCOUNTNM;
                        }
                        foreach (var credit in serchaccount_credit)
                        {
                            creditname = credit.ACCOUNTNM;
                        }


                        var findpassengername =
                           (from n in db.passengerDbSet where n.COMPID == compid && n.CARDNO == item.cardno select new { n.PSGRNM })
                               .ToList();
                        string passenger = "";
                        foreach (var x in findpassengername)
                        {
                            passenger = x.PSGRNM;
                        }


                        var findcostpname =
                           (from n in db.GlCostPoolDbSet where n.COMPID == compid && n.COSTPID == item.costpid select new { n.COSTPNM })
                               .ToList();
                        string costpnm = "";
                        foreach (var x in findcostpname)
                        {
                            costpnm = x.COSTPNM;
                        }
                       
                        yield return new MTransDTO
                        {
                            ID = item.Id,
                            CompanyID = item.compid,
                            BranchID = item.branchid,
                            Transno = item.transNo,
                            TransType = item.transtype,
                            TransMonthYear = item.transmonth,
                            TransDate = conv_transdt,


                            TransSerial = item.transserial,
                            TransFor = item.transfor,
                            TransactionMode = item.transmode,
                            CostPoolID = item.costpid,
                            CostPoolName=costpnm,
                            CreditCode = item.creditcd,
                            DebitCode = item.debitcd,
                            DebitAccount=debitname,
                            CreditAccount=creditname,
                            CHEQUEDT = conv_chequedt,
                            CHEQUENO = item.chequeno,
                            Amount = item.amount,
                            REMARKS = item.remarks,
                            Cardno=item.cardno,
                            Passenger = passenger

                        };
                    }





                }

            }
            else
            {
                yield return new MTransDTO
                {
                    REMARKS="holo na"
                };
            }
          


        }

        [System.Web.Http.Route("apiMultipleVoucher/ChildData")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddChildData(MTransDTO model)
        {
            //DateTime efdt = Convert.ToDateTime(model.EFDT);

            var check_data = (from n in db.MtransdbSet
                              where n.COMPID == model.CompanyID && n.BRANCHID == model.BranchID && n.TRANSTP==model.TransType && n.TRANSNO==model.Transno
                             
                              select n).ToList();


            if (check_data.Count == 0)
            {



                GL_MTRANS childData = new GL_MTRANS();

                childData.COMPID = model.CompanyID;
                childData.BRANCHID = model.BranchID;
                childData.TRANSTP = model.TransType;
                childData.TRANSDT = Convert.ToDateTime(model.TransDate);
                childData.TRANSMY = model.TransMonthYear;
                childData.TRANSNO = model.Transno;
                childData.TRANSSL = 1;
                childData.TRANSFOR =model.TransFor;
                childData.TRANSMODE = model.TransactionMode;
                childData.COSTPID = model.CostPoolID;
                childData.CARDNO = model.Cardno;
                var cardid = (from n in db.passengerDbSet
                              where n.COMPID == model.CompanyID && n.CARDNO == model.Cardno
                              select new { n.CARDID }).ToList();
                foreach (var item in cardid)
                {
                    childData.CARDID = Convert.ToString(item.CARDID);
                }
                if (cardid.Count==0)
                {
                    childData.CARDNO = null;
                }
               

                childData.CREDITCD = model.CreditCode;
                childData.DEBITCD = model.DebitCode;
                if(model.CHEQUEDT!="")
                {
                    childData.CHEQUEDT = Convert.ToDateTime(model.CHEQUEDT);
                }
                else
                {
                    childData.CHEQUEDT = null;
                }
                childData.CHEQUENO = model.CHEQUENO;
                childData.AMOUNT = model.Amount;
                childData.REMARKS = model.REMARKS;


                childData.USERPC = strHostName;
                childData.INSIPNO = ipAddress.ToString();
                childData.INSTIME = Convert.ToDateTime(td);
                childData.INSUSERID = model.INSUSERID;
                childData.INSLTUDE = model.INSLTUDE;

                db.MtransdbSet.Add(childData);

                db.SaveChanges();

                //model.ID = childData.ID;
                //model.COMPID = model.COMPID;
                //model.PatientYear = model.PatientYear;
                //model.PatientType = model.PatientType;
                //model.PatientId = Patientid;
                //model.PatientYearId = model.PatientYearId;
                //model.TransactionDate = model.TransactionDate;
                //model.TransSerial = childData.TRANSSL;
                //model.Amount = model.Amount;
                //model.DoctorId = model.DoctorId;
                //model.DoctorName = model.DoctorName;

                //model.Remarks = model.Remarks;


                //model.USERPC = strHostName;
                //model.INSIPNO = ipAddress.ToString();
                //model.INSTIME = Convert.ToDateTime(td);
                //model.INSUSERID = model.INSUSERID;
                //model.INSLTUDE = model.INSLTUDE;

                //Insert_OpdLogData(model);






                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                //response.Headers.Location = new Uri(Url.Link("api/ApiFilterItemController", new { gid = model.FILTERGID }));

                return response;



            }
            else
            {

               
                Int64 findMaxTranssl = Convert.ToInt64((from n in db.MtransdbSet
                                                        where n.COMPID == model.CompanyID && n.BRANCHID == model.BranchID && n.TRANSTP == model.TransType && n.TRANSNO == model.Transno
                                                        select n.TRANSSL).Max());

                GL_MTRANS childData = new GL_MTRANS();

                childData.COMPID = model.CompanyID;
                childData.BRANCHID = model.BranchID;
                childData.TRANSTP = model.TransType;
                childData.TRANSDT = Convert.ToDateTime(model.TransDate);
                childData.TRANSMY = model.TransMonthYear;
                childData.TRANSNO = model.Transno;
                childData.TRANSSL = findMaxTranssl+1;
                childData.TRANSFOR = model.TransFor;
                childData.TRANSMODE = model.TransactionMode;
                childData.COSTPID = model.CostPoolID;

                var cardid = (from n in db.passengerDbSet
                              where n.COMPID == model.CompanyID && n.CARDNO == model.Cardno
                              select new { n.CARDID }).ToList();
                foreach (var item in cardid)
                {
                    childData.CARDID = Convert.ToString(item.CARDID);
                }
                childData.CARDNO = model.Cardno;

                childData.CREDITCD = model.CreditCode;
                childData.DEBITCD = model.DebitCode;
                if (model.CHEQUEDT != "")
                {
                    childData.CHEQUEDT = Convert.ToDateTime(model.CHEQUEDT);
                }
                else
                {
                    childData.CHEQUEDT = null;
                }
                childData.CHEQUENO = model.CHEQUENO;
                childData.AMOUNT = model.Amount;
                childData.REMARKS = model.REMARKS;


                childData.USERPC = strHostName;
                childData.INSIPNO = ipAddress.ToString();
                childData.INSTIME = Convert.ToDateTime(td);
                childData.INSUSERID = model.INSUSERID;
                childData.INSLTUDE = model.INSLTUDE;

                db.MtransdbSet.Add(childData);

                db.SaveChanges();

                //model.ID = childData.ID;
                //model.COMPID = model.COMPID;
                //model.PatientYear = model.PatientYear;
                //model.PatientType = model.PatientType;
                //model.PatientId = Patientid;
                //model.PatientYearId = model.PatientYearId;
                //model.TransactionDate = model.TransactionDate;
                //model.TransSerial = childData.TRANSSL;
                //model.Amount = model.Amount;
                //model.DoctorId = model.DoctorId;
                //model.DoctorName = model.DoctorName;

                //model.Remarks = model.Remarks;


                //model.USERPC = strHostName;
                //model.INSIPNO = ipAddress.ToString();
                //model.INSTIME = Convert.ToDateTime(td);
                //model.INSUSERID = model.INSUSERID;
                //model.INSLTUDE = model.INSLTUDE;

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);


                return response;
            }


        }


        [System.Web.Http.Route("api/ApiMultipleVoucher/SaveData")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveData(MTransDTO model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
           
            Int64 id = 0;
           
              
              

                    var data_find = (from n in db.MtransdbSet where n.ID == model.ID select n).ToList();
                    foreach (var item in data_find)
                    {
                        item.ID = model.ID;
                        item.COMPID = item.COMPID;
                        item.BRANCHID = item.BRANCHID;
                        item.TRANSTP = item.TRANSTP;
                        item.TRANSDT = item.TRANSDT;
                        item.TRANSMY = item.TRANSMY;
                        item.TRANSFOR = model.TransFor;
                        item.TRANSSL = item.TRANSSL;
                        item.COSTPID = model.CostPoolID;
                        item.TRANSMODE = model.TransactionMode;
                        item.DEBITCD = model.DebitCode;
                        item.CREDITCD = model.CreditCode;
                        if(model.CHEQUEDT!="")
                        {
                            item.CHEQUEDT = Convert.ToDateTime(model.CHEQUEDT);
                        }
                        else
                        {
                            item.CHEQUEDT = null;
                        }
                       
                        item.CHEQUENO = model.CHEQUENO;
                        item.AMOUNT = model.Amount;
                        item.REMARKS = model.REMARKS;
                        item.CARDNO = model.Cardno;
                        var cardid = (from n in db.passengerDbSet
                                      where n.COMPID == model.CompanyID && n.CARDNO == model.Cardno
                                      select new { n.CARDID }).ToList();
                        foreach (var foring in cardid)
                        {
                            item.CARDID = Convert.ToString(foring.CARDID);
                        }

                        item.USERPC = item.USERPC;
                        item.INSIPNO = item.INSIPNO;
                        item.INSLTUDE = item.INSLTUDE;
                        item.INSTIME = item.INSTIME;
                        item.INSUSERID = item.INSUSERID;


                        item.UPDIPNO = ipAddress.ToString();
                        item.UPDLTUDE = model.UPDLTUDE;
                        item.UPDUSERID = model.UPDUSERID;
                        item.UPDTIME = Convert.ToDateTime(td);

                    }


                    //db.Entry(cartFilter).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                    }

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);


                    return response;
               

           

          

        }

        [Route("api/ApiMultipleVoucher/DeleteData")]
        [HttpPost]
        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteData(MTransDTO model)
        {

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());
            string query = string.Format("DELETE FROM GL_MTRANS WHERE ID='{0}'", model.ID);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            conn.Close();
            MTransDTO testObj = new MTransDTO();






            return Request.CreateResponse(HttpStatusCode.OK, testObj);
        }



    }
}
