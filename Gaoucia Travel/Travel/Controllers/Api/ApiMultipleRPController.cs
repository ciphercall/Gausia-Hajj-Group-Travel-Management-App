using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Travel.Models;
using Travel.Models.DTO;
using Travel.Models.Travel;

namespace Travel.Controllers.Api
{
    public class ApiMultipleRPController : ApiController
    {
        
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        TravelDbContext db = new TravelDbContext();

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;
        public ApiMultipleRPController()
        {
           
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
          
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }




        [System.Web.Http.Route("api/ApiMultipleRP/GetData")]
        [System.Web.Http.HttpGet]
        public IEnumerable<RPDTO> Get(string Compid, string Transfor, string Transdt, string Month, string Tno,string Cardcid,string Cardpid, string InUserID, string InsLtude)
        {

            //Get Ip ADDRESS,Time & user PC Name

            if (Transdt != null)
            {
                Int64 InsUserID = Convert.ToInt64(InUserID);
                Int64 compid = Convert.ToInt64(Compid);
              
                Int64 transno = Convert.ToInt64(Tno);
                Int64 cardcid = Convert.ToInt64(Cardcid);
                Int64 cardpid = Convert.ToInt64(Cardpid);
                DateTime tt = Convert.ToDateTime(Transdt);

                var ache_kina_data = (from n in db.RPDRCRDbSet where n.COMPID == compid && n.TRANSDT == tt && n.TRANSFOR == Transfor && n.CARDCID == cardcid && n.CARDPID == cardpid && n.TRANSNO == transno select n).ToList();
                if (ache_kina_data.Count == 0)
                {
                   

                    yield return new RPDTO
                    {

                        CompanyId = compid,
                       
                        TransactionNo = transno,
                   TransactionFor=Transfor,
                        Transmonthyear = Month,
                        TransactionDate = Transdt,
                        Cardcid=cardcid,
                        Cardpid=cardpid,
                        Remarks = "no data"



                    };








                }
                else
                {




                    foreach (var item in ache_kina_data)
                    {
                        string ticketdt = "";
                        if (item.TICKETDT != null)
                        {
                            DateTime abcd = Convert.ToDateTime(item.TICKETDT);
                            ticketdt = abcd.ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            ticketdt = "";
                        }
                        string cardno = item.CARDID.ToString().Substring(8, 7);
                        string psgname = "";
                        var passenger_find = (from n in db.passengerDbSet where n.COMPID == item.COMPID && n.CARDNO ==cardno select n).ToList();
                      foreach(var ss in passenger_find)
                      {
                          psgname = ss.PSGRNM;
                      }

                      var account_nm = (from n in db.GlAcchartDbSet where n.COMPID == item.COMPID && n.ACCOUNTCD == item.ACCOUNTCD select n).ToList();
                      string accnm = "";
                        foreach(var nn in account_nm)
                        {
                            accnm = nn.ACCOUNTNM;
                        }

                        yield return new RPDTO
                        {
                            Id = item.Id,
                            CompanyId = item.COMPID,
                            TransactionDate=Convert.ToString(item.TRANSDT),
                        Transmonthyear=item.TRANSMY,
                            TransactionNo=item.TRANSNO,
                            Cardcid=item.CARDCID,
                            Cardpid=item.CARDPID,
                            Cardyear=item.CARDYY,
                            Cardid=Convert.ToString(item.CARDID),
                            Cardno=cardno,
                            passenger=psgname,
                            TransactionFor=item.TRANSFOR,
                            Accountcd=item.ACCOUNTCD,
                            Accountnm=accnm,
                            Ticketno=item.TICKETNO,

                            TicketDate = ticketdt,
                            Amount=item.AMOUNT,
                            Cashamount=item.AMTCASH,
                            Creditamount=item.AMTCREDIT,
                            Remarks=item.REMARKS,
                            TransactionSerial=Convert.ToString(item.TRANSSL)
                          

                        };
                    }





                }

            }
            else
            {
                yield return new RPDTO
                {
                    Remarks = "holo na"
                };
            }



        }

        [System.Web.Http.Route("apiMultipleRP/ChildData")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddChildData(RPDTO model)
        {

            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            TAMS_RPDRCR obj = new TAMS_RPDRCR();

            obj.USERPC = strHostName;
            obj.INSIPNO = ipAddress.ToString();
            obj.INSTIME = Convert.ToDateTime(td);
            obj.INSUSERID = model.INSUSERID;
            obj.INSLTUDE = model.INSLTUDE;

            obj.COMPID = model.CompanyId;
            obj.TRANSDT = Convert.ToDateTime(model.TransactionDate);
            obj.TRANSMY = model.Transmonthyear;
            obj.TRANSNO = model.TransactionNo;
            obj.CARDCID = model.Cardcid;
            obj.CARDPID = model.Cardpid;
            obj.TRANSFOR = model.TransactionFor;

            var cardnodata = from n in db.passengerDbSet
                             where n.COMPID == model.CompanyId && n.CARDNO == model.Cardno
                             select new { n.CARDYY, n.CARDID };
            foreach (var l in cardnodata)
            {
                obj.CARDID = Convert.ToInt64(l.CARDID);
                obj.CARDYY = Convert.ToInt64(l.CARDYY);
            }


            var check_for_duplicate =
                                  (from n in db.RPDRCRDbSet
                                   where n.COMPID == model.CompanyId && n.TRANSNO == model.TransactionNo
                                   select n).ToList();
            if (check_for_duplicate.Count == 0)
            {

                obj.ACCOUNTCD = model.Accountcd;
                obj.TICKETDT = Convert.ToDateTime(model.TicketDate);
                obj.TICKETNO = model.Ticketno;
                obj.AMOUNT = model.Amount;
                obj.AMTCASH = model.Cashamount;
                obj.AMTCREDIT = model.Creditamount;
                obj.REMARKS = model.Remarks;
                obj.TRANSSL = Convert.ToInt64(Convert.ToString(model.TransactionNo) + "01");

                db.RPDRCRDbSet.Add(obj);
                db.SaveChanges();
            }
            else
            {
                Int64 find_maxtranssl = Convert.ToInt64((from n in db.RPDRCRDbSet where n.COMPID == model.CompanyId && n.TRANSNO == model.TransactionNo select n.TRANSSL).Max());
                if(find_maxtranssl<Convert.ToInt64(Convert.ToString(model.TransactionNo) + "99"))
                {
                    obj.ACCOUNTCD = model.Accountcd;
                    obj.TICKETDT = Convert.ToDateTime(model.TicketDate);
                    obj.TICKETNO = model.Ticketno;
                    obj.AMOUNT = model.Amount;
                    obj.AMTCASH = model.Cashamount;
                    obj.AMTCREDIT = model.Creditamount;
                    obj.REMARKS = model.Remarks;
                    obj.TRANSSL = find_maxtranssl + 1;

                    db.RPDRCRDbSet.Add(obj);
                    db.SaveChanges();
                }
                  
              
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
            //response.Headers.Location = new Uri(Url.Link("api/ApiFilterItemController", new { gid = model.FILTERGID }));

            return response;


        }

        [System.Web.Http.Route("api/ApiMultipleRP/SaveData")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveData(RPDTO model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Int64 id = 0;




            var data_find = (from n in db.RPDRCRDbSet where n.Id == model.Id select n).ToList();
            foreach (var item in data_find)
            {
                item.Id = model.Id;
                item.COMPID = item.COMPID;
                item.TRANSDT = item.TRANSDT;
                item.TRANSMY = item.TRANSMY;
                item.TRANSNO = item.TRANSNO;
              
                item.TRANSFOR = item.TRANSFOR;
                item.CARDCID = item.CARDCID;
                item.CARDPID = item.CARDPID;

                item.TRANSSL = item.TRANSSL;
                //if (model.CHEQUEDT != "")
                //{
                //    item.CHEQUEDT = Convert.ToDateTime(model.CHEQUEDT);
                //}
                //else
                //{
                //    item.CHEQUEDT = null;
                //}

            
                var cardid = (from n in db.passengerDbSet
                              where n.COMPID == model.CompanyId && n.CARDNO == model.Cardno
                              select new { n.CARDID,n.CARDYY }).ToList();
                foreach (var foring in cardid)
                {
                    item.CARDID = Convert.ToInt64(foring.CARDID);
                    item.CARDYY = Convert.ToInt64(foring.CARDYY);
                }

                item.ACCOUNTCD = model.Accountcd;

                var tickdt_find = (from n in db.RPDRCRDbSet where n.COMPID == model.CompanyId && n.TICKETNO == model.Ticketno select n).ToList();
                foreach(var ff in tickdt_find)
                {
                    item.TICKETDT = ff.TICKETDT;
                    break;
                }

                item.ACCOUNTCD = model.Accountcd;
                item.TICKETNO = model.Ticketno;
                item.AMOUNT = model.Amount;
                item.AMTCREDIT = model.Amount - model.Cashamount; ;
                item.AMTCASH = model.Cashamount;
                item.REMARKS = model.Remarks;

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

        [Route("api/ApiMultipleRP/DeleteData")]
        [HttpPost]
        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteData(RPDTO model)
        {

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());
            string query = string.Format("DELETE FROM TAMS_RPDRCR WHERE ID='{0}'", model.Id);
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

        [System.Web.Http.Route("apiMultipleRP/CardNo")]
        [System.Web.Http.HttpGet]
        public IEnumerable<RPDTO> GetCardno(string query, string query2)
        {
            using (var db = new TravelDbContext())
            {
                Int64 compid = Convert.ToInt64(query2);
                return String.IsNullOrEmpty(query) ? db.passengerDbSet.AsEnumerable().Select(b => new RPDTO { }).ToList() :
                db.passengerDbSet.Where(p => p.CARDNO.StartsWith(query) && p.COMPID == compid).Select(
                          x =>
                          new
                          {
                              cardno = x.CARDNO,
                              passenger = x.PSGRNM,


                          })
                .AsEnumerable().Select(a => new RPDTO
                {
                    Cardno = a.cardno,
                    passenger = a.passenger

                }).ToList();
            }
        }

    }
}
