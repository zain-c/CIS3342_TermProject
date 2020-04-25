using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using System.Data;
using System.Data.SqlClient;

namespace DatingSiteLibrary
{
    public class PassedList
    {
        string list;

        public PassedList()
        {

        }

        public string List
        {
            get { return list; }
            set { list = value; }
        }

        public PassedList getPasses(int userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetPasses";
            objCmd.Parameters.AddWithValue("@passedBy", userID);
            DataSet passesDS = objDB.GetDataSetUsingCmdObj(objCmd);

            PassedList passedList = new PassedList();
            if (passesDS.Tables[0].Rows.Count > 0)
            {
                passedList.List = passesDS.Tables[0].Rows[0]["Passed"].ToString().TrimEnd('|');
                return passedList;
            }
            else
            {
                return null;
            }
            
        }
    }
}
