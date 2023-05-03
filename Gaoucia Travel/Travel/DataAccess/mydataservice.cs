using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Travel.Models;

namespace Travel.DataAccess
{
    public class mydataservice
    {

        public IEnumerable TopcategoriesListdata(Int64 loggedcompid, string todate, string frdate)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());

            var query = string.Format(@"select COSTCNM CARDTP, COUNT(*) QTY from TAMS_PASSENGER Inner Join
GL_COSTPMST on TAMS_PASSENGER.COMPID=GL_COSTPMST.COMPID and TAMS_PASSENGER.CARDCID=GL_COSTPMST.COSTCID
where TAMS_PASSENGER.COMPID='{0}' and TAMS_PASSENGER.CARDDT between '{1}' and '{2}'
GROUP by COSTCNM ORDER BY QTY DESC",loggedcompid,todate,frdate);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            IList<Outputclass> transList = new List<Outputclass>();
            // var GetList = ds.ToList();

            foreach (DataRow row in ds.Rows)
            {
                transList.Add(new Outputclass()
                {
                    PlanName = row["CARDTP"].ToString(),
                    PaymentAmount = Convert.ToInt64(row["QTY"])

                });
            }
            // var list = con.Query<Outputclass>("Usp_Getdata").AsEnumerable();
            // List of type Outputclass which it will return .
            return transList;
        }



        public IEnumerable TopItemsByQtyListdata(Int64 loggedcompid, string todate, string frdate)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());

            var query = string.Format(@"SELECT ACCOUNTNM AS AGENTNM, COUNT(*) QTY 
                     FROM  TAMS_PASSENGER INNER JOIN
                      GL_ACCHART ON TAMS_PASSENGER.COMPID = GL_ACCHART.COMPID AND TAMS_PASSENGER.AGENTID = GL_ACCHART.ACCOUNTCD 
                      WHERE TAMS_PASSENGER.COMPID='{0}' AND TAMS_PASSENGER.CARDDT  BETWEEN '{1}' AND  '{2}' 
                      GROUP BY GL_ACCHART.ACCOUNTNM 
                       ORDER BY QTY DESC",loggedcompid,todate,frdate);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            IList<Outputclass> transList = new List<Outputclass>();
            // var GetList = ds.ToList();

            foreach (DataRow row in ds.Rows)
            {
                transList.Add(new Outputclass()
                {
                    PlanName = row["AGENTNM"].ToString(),
                    PaymentAmount = Convert.ToInt64(row["QTY"])

                });
            }
            // var list = con.Query<Outputclass>("Usp_Getdata").AsEnumerable();
            // List of type Outputclass which it will return .
            return transList;
        }




        public IEnumerable TopItemsByValueListdata(Int64 loggedcompid, string todate, string frdate)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());

            var query = string.Format(@"SELECT ACCOUNTNM AS ITEMNM, SUM(AMOUNT) VALUE 
                    FROM TAMS_RPDRCR A INNER JOIN 
                     TAMS_PASSENGER B ON A.COMPID = B.COMPID AND A.CARDID = B.CARDID 
					 INNER JOIN GL_ACCHART C ON B.COMPID = C.COMPID AND B.AGENTID = C.ACCOUNTCD
                     WHERE TRANSFOR = 'RECEIVABLE' AND A.COMPID='{0}' AND A.TRANSDT  BETWEEN '{1}' AND  '{2}'
                     GROUP BY ACCOUNTNM
                     ORDER BY VALUE DESC",loggedcompid,todate,frdate);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            IList<Outputclass> transList = new List<Outputclass>();
            // var GetList = ds.ToList();

            foreach (DataRow row in ds.Rows)
            {
                transList.Add(new Outputclass()
                {
                    PlanName = row["ITEMNM"].ToString(),
                    PaymentAmount = Convert.ToInt64(row["VALUE"])

                });
            }
            // var list = con.Query<Outputclass>("Usp_Getdata").AsEnumerable();
            // List of type Outputclass which it will return .
            return transList;
        }




        public IEnumerable CollectionDataListdata(Int64 loggedcompid, string todate, string frdate)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());

            var query = string.Format(@"SELECT CONVERT(NVARCHAR(20),TRANSDT,103) AS TRANSDT, SUM(DEBITAMT) COLLECT 
                FROM GL_MASTER 
                WHERE TRANSTP='MREC' AND COMPID='{0}' AND TRANSDRCR='DEBIT' AND TRANSDT  BETWEEN '{1}' AND  '{2}' 
                GROUP BY TRANSDT",loggedcompid,todate,frdate);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            IList<Outputclass> transList = new List<Outputclass>();
            // var GetList = ds.ToList();

            foreach (DataRow row in ds.Rows)
            {
                transList.Add(new Outputclass()
                {
                    PlanName = row["TRANSDT"].ToString(),
                    PaymentAmount = Convert.ToInt64(row["COLLECT"])

                });
            }
            // var list = con.Query<Outputclass>("Usp_Getdata").AsEnumerable();
            // List of type Outputclass which it will return .
            return transList;
        }



        public IEnumerable TimeWiseSellDataListdata(Int64 loggedcompid, string todate, string frdate)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());

            var query = string.Format(@"SELECT DISTINCT CONVERT(NVARCHAR(20),dateadd(hour, datediff(hour, 0, dateadd(mi, 30, INSTIME)), 0) ,108) AS INSTIME, COUNT(*) QTY 
                FROM TAMS_PASSENGER 
                WHERE COMPID='{0}' AND CARDDT  BETWEEN '{1}' AND  '{2}'
               GROUP BY CONVERT(NVARCHAR(20),dateadd(hour, datediff(hour, 0, dateadd(mi, 30, INSTIME)), 0) ,108)",loggedcompid,todate,frdate);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            IList<Outputclass> transList = new List<Outputclass>();
            // var GetList = ds.ToList();

            foreach (DataRow row in ds.Rows)
            {
                transList.Add(new Outputclass()
                {
                    PlanName = row["INSTIME"].ToString(),
                    PaymentAmount = Convert.ToInt64(row["QTY"])

                });
            }
            // var list = con.Query<Outputclass>("Usp_Getdata").AsEnumerable();
            // List of type Outputclass which it will return .
            return transList;
        }

    }
}