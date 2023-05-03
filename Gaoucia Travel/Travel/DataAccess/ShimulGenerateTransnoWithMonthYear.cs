using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Models;

namespace Travel.DataAccess
{
    public class ShimulGenerateTransnoWithMonthYear
    {


        public static List<string> DateChanged_getMonth(string changedtxt, string changedtxt2, string tablename)//with Trans No Generation
        {

            DateTime tarnsdt = Convert.ToDateTime(changedtxt);
            string converttoString = Convert.ToString(tarnsdt.ToString("dd-MMM-yyyy"));
            string getYear = converttoString.Substring(9, 2);
            string getMonth = converttoString.Substring(3, 3);
            string Month = getMonth + "-" + getYear;


            string converttoString1 = Convert.ToString(tarnsdt.ToString("dd-MM-yyyy"));
            string getyear = converttoString1.Substring(6, 4);
            string getmonth = converttoString1.Substring(3, 2);
            string halftransno = getyear + getmonth;

            TravelDbContext db = new TravelDbContext();

            Int64 compid = Convert.ToInt64(changedtxt2);

            Int64 maxvalue = 0, Trans = 0;
            //string aa = "from n in db.dbset where n.COMPID == compid select new { n.TRANSNO }";
            //var query = from n in db.PathologyMasterDbSet where n.COMPID == compid select new { n.TRANSNO };
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["TravelDbContext"].ToString());

            string query1 = string.Format("SELECT TRANSNO FROM {1} WHERE COMPID='{0}'", compid, tablename);

            System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand(query1, conn);
            conn.Open();

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);

            List<SelectListItem> Transno = new List<SelectListItem>();
            //Int64 i = 0;
            foreach (DataRow row in ds1.Rows)
            {
                string temp = Convert.ToString(row.ItemArray[0]);
                string substring = temp.Substring(0, 6);
                if (substring == halftransno)
                {
                    Transno.Add(new SelectListItem { Text = row.ItemArray[0].ToString(), Value = row.ItemArray[0].ToString() });

                }
            }




            string transno = "";
            if (Transno.Count == 0)
            {

                transno = halftransno + "0001";
            }
            else
            {
                maxvalue = Transno.Max(t => Convert.ToInt64(t.Text));
                Int64 temp = maxvalue + 1;
                transno = Convert.ToString(temp);

            }
            List<string> result = new List<string>();
            result.Add(Month);
            result.Add(transno);
            //var result = new { month = Month, TransNo = transno };
            conn.Close();
            return result;

        }

    }
}