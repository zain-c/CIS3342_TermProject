using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace DatingSiteLibrary
{
    public class DateRequest
    {
        int userIDFrom;
        int userIDTo;
        string status;

        public DateRequest()
        {

        }

        public List<DateRequest> getSentRequests(int userIDFrom)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetSentDateRequests";
            objCmd.Parameters.AddWithValue("@requestFrom", userIDFrom);
            DataSet requestsDS = objDB.GetDataSetUsingCmdObj(objCmd);

            List<DateRequest> requestsList = new List<DateRequest>();
            if(requestsDS.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow row in requestsDS.Tables[0].Rows)
                {
                    DateRequest dateRequest = new DateRequest();
                    dateRequest.UserIDFrom = userIDFrom;
                    dateRequest.UserIDTo = int.Parse(row["RequestTo"].ToString());
                    dateRequest.Status = requestsDS.Tables[0].Rows[0]["Status"].ToString();
                    requestsList.Add(dateRequest);
                }
                return requestsList;
            }
            else
            {                
                return requestsList;
            }
        }

        public List<DateRequest> getReceivedRequests(int userIDTo)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetReceivedDateRequests";
            objCmd.Parameters.AddWithValue("@requestTo", userIDTo);
            DataSet requestsDS = objDB.GetDataSetUsingCmdObj(objCmd);

            List<DateRequest> requestsList = new List<DateRequest>();
            if (requestsDS.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in requestsDS.Tables[0].Rows)
                {
                    DateRequest dateRequest = new DateRequest();
                    dateRequest.UserIDTo = userIDTo;
                    dateRequest.UserIDFrom = int.Parse(row["RequestFrom"].ToString());
                    dateRequest.Status = requestsDS.Tables[0].Rows[0]["Status"].ToString();
                    requestsList.Add(dateRequest);
                }
                return requestsList;
            }
            else
            {
                return requestsList;
            }
        }

        public int UserIDTo
        {
            get { return userIDTo; }
            set { userIDTo = value; }
        }

        public int UserIDFrom
        {
            get { return userIDFrom; }
            set { userIDFrom = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
