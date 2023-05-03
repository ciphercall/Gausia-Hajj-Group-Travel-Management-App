using Travel.Models;
using Travel.Models.ASL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Travel.Controllers.Api
{
    public class ApiBranchController : ApiController
    {
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        TravelDbContext db = new TravelDbContext();

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        public ApiBranchController()
        {

            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];

            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }


        [System.Web.Http.Route("apiBranch/GetAllData")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Asl_Branch> GetAll(string CompID)
        {

            //Get Ip ADDRESS,Time & user PC Name


            int compid = Convert.ToInt16(CompID);




            var all_branchdata = (from n in db.Asl_BranchDbSet select n).ToList();
            foreach (var aslBranch in all_branchdata)
            {
                yield return new Asl_Branch
                {

                    COMPID = aslBranch.COMPID,
                    BRANCHID = aslBranch.BRANCHID,
                    BRANCHNM = aslBranch.BRANCHNM,
                    ADDRESS = aslBranch.ADDRESS,
                    CONTACTNO = aslBranch.CONTACTNO,
                    EMAILID = aslBranch.EMAILID,
                    STATUS = aslBranch.STATUS,
                    USERPC = aslBranch.USERPC,
                    INSUSERID = aslBranch.INSUSERID,
                    INSTIME = aslBranch.INSTIME,
                    INSIPNO = aslBranch.INSIPNO,
                    INSLTUDE = aslBranch.INSLTUDE,
                };
            }


        }









        [System.Web.Http.Route("api/ApiBranch/GetData")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Asl_Branch> Get(string CompID, string BranchNm, string Address, string Contactno, string Email, string Status, string InUserID, string InsLtude)
        {

            //Get Ip ADDRESS,Time & user PC Name


            Int64 InsUserID = Convert.ToInt64(InUserID);
            int compid = Convert.ToInt16(CompID);


            string findMaxBrID = "0";
            int companyid = Convert.ToInt16(compid);

            var checkBrIDList = (from branch in db.Asl_BranchDbSet where branch.COMPID == companyid select branch.BRANCHID).ToList();
            Int64 maxBrID = 0;
            if (checkBrIDList.Count() == 0)
            {
                findMaxBrID = Convert.ToString(companyid) + "01";
            
            }
            else
            {
                maxBrID = (from branch in db.Asl_BranchDbSet where branch.COMPID == companyid select branch.BRANCHID).Max();
                var brID = Convert.ToInt16(maxBrID.ToString().Substring(3, 2));
                if (brID < 99)
                {
                    findMaxBrID = (maxBrID + 1).ToString();
                }
            }

            Int64 brid = Convert.ToInt64(findMaxBrID);

            var findData = (from n in db.Asl_BranchDbSet where n.COMPID == compid && n.BRANCHID == brid select n).ToList();
            if (findData.Count == 0)
            {
                Asl_Branch obj = new Asl_Branch
                {
                    COMPID = compid,
                    BRANCHID = brid,
                    BRANCHNM = BranchNm,
                    ADDRESS = Address,
                    CONTACTNO = Contactno,
                    EMAILID = Email,
                    STATUS = Status,
                    USERPC = strHostName,
                    INSUSERID = InsUserID,
                    INSTIME = td,
                    INSIPNO = ipAddress.ToString(),
                    INSLTUDE = InsLtude,

                };

                Insert_BranchLogData(obj);

                db.Asl_BranchDbSet.Add(obj);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
                var all_branchdata = (from n in db.Asl_BranchDbSet select n).ToList();
                foreach (var aslBranch in all_branchdata)
                {
                    yield return new Asl_Branch
                    {

                        COMPID = aslBranch.COMPID,
                        BRANCHID = aslBranch.BRANCHID,
                        BRANCHNM = aslBranch.BRANCHNM,
                        ADDRESS = aslBranch.ADDRESS,
                        CONTACTNO = aslBranch.CONTACTNO,
                        EMAILID = aslBranch.EMAILID,
                        STATUS = aslBranch.STATUS,
                        USERPC = aslBranch.USERPC,
                        INSUSERID = aslBranch.INSUSERID,
                        INSTIME = aslBranch.INSTIME,
                        INSIPNO = aslBranch.INSIPNO,
                        INSLTUDE = aslBranch.INSLTUDE,
                    };
                }


            }
        }


        public ASL_LOG aslLog = new ASL_LOG();
        public void Insert_BranchLogData(Asl_Branch model)
        {
            var date = Convert.ToString(td.ToString("dd-MMM-yyyy"));
            var time = Convert.ToString(td.ToString("hh:mm:ss tt"));

            Int64 maxSerialNo = Convert.ToInt64((from n in db.AslLogDbSet where n.COMPID == model.COMPID && n.BRANCHID == model.BRANCHID && n.USERID == model.UPDUSERID select n.LOGSLNO).Max());
            if (maxSerialNo == 0)
            {
                aslLog.LOGSLNO = Convert.ToInt64("1");
            }
            else
            {
                aslLog.LOGSLNO = maxSerialNo + 1;
            }

            aslLog.COMPID = Convert.ToInt64(model.COMPID);
            aslLog.USERID = Convert.ToInt64(model.INSUSERID);
            aslLog.BRANCHID = model.BRANCHID;
            aslLog.LOGTYPE = "INSERT";
            aslLog.LOGSLNO = aslLog.LOGSLNO;
            aslLog.LOGDATE = Convert.ToDateTime(date);
            aslLog.LOGTIME = Convert.ToString(time);
            aslLog.LOGIPNO = model.INSIPNO;
            aslLog.LOGLTUDE = model.INSLTUDE;




            aslLog.LOGDATA = Convert.ToString("Branch ID: " + model.BRANCHID + ",\nBranch Name: " + model.BRANCHNM + ",\nAddress: " + model.ADDRESS + ",\nContact No: " + model.CONTACTNO + ",\nEmail: " + model.EMAILID + ",\nStatus: " +
                model.STATUS);
            aslLog.TABLEID = "Asl_Branch";
            aslLog.USERPC = model.USERPC;

            db.AslLogDbSet.Add(aslLog);
        }



        [System.Web.Http.Route("api/ApiBranch/SaveData")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveData(Asl_Branch model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var duplicate_data_test =
                    (from n in db.Asl_BranchDbSet
                     where n.COMPID == model.COMPID && n.BRANCHID == model.BRANCHID
&& n.BRANCHNM == model.BRANCHNM
                     select n).ToList();

            if (duplicate_data_test.Count == 1)
            {



                var data_find = (from n in db.Asl_BranchDbSet
                                 where n.COMPID == model.COMPID && n.BRANCHID == model.BRANCHID
                                     && n.BRANCHNM == model.BRANCHNM
                                 select n).ToList();
                foreach (var item in data_find)
                {

                    item.COMPID = item.COMPID;
                    item.BRANCHID = item.BRANCHID;
                    item.BRANCHNM = item.BRANCHNM;
                    item.ADDRESS = model.ADDRESS;
                    item.CONTACTNO = model.CONTACTNO;
                    item.EMAILID = model.EMAILID;
                    item.STATUS = model.STATUS;


                    item.USERPC = item.USERPC;
                    item.INSIPNO = item.INSIPNO;
                    item.INSLTUDE = item.INSLTUDE;
                    item.INSTIME = item.INSTIME;
                    item.INSUSERID = item.INSUSERID;

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

            else if (duplicate_data_test.Count == 0)
            {
                var data_find = (from n in db.Asl_BranchDbSet
                                 where n.COMPID == model.COMPID && n.BRANCHID == model.BRANCHID

                                 select n).ToList();
                foreach (var item in data_find)
                {

                    item.COMPID = item.COMPID;
                    item.BRANCHID = item.BRANCHID;
                    item.BRANCHNM = model.BRANCHNM;
                    item.ADDRESS = model.ADDRESS;
                    item.CONTACTNO = model.CONTACTNO;
                    item.EMAILID = model.EMAILID;
                    item.STATUS = model.STATUS;


                    item.USERPC = item.USERPC;
                    item.INSIPNO = item.INSIPNO;
                    item.INSLTUDE = item.INSLTUDE;
                    item.INSTIME = item.INSTIME;
                    item.INSUSERID = item.INSUSERID;

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
            else
            {
                model.BRANCHID = 0;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);


                return response;
            }



        }

        [Route("api/ApiBranch/DeleteData")]
        [HttpPost]
        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteData(Asl_Branch model)
        {

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());
            string query = string.Format("DELETE FROM Asl_branch WHERE COMPID='{0}' AND BRANCHID='{1}'", model.COMPID, model.BRANCHID);
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
            Asl_Branch testObj = new Asl_Branch();






            return Request.CreateResponse(HttpStatusCode.OK, testObj);
        }

    }
}
