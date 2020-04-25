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

        public int addPassToDB(int userID, int passedUserID)
        {
            int result = 0;
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;


            PassedList tempPasses = new PassedList();
            string passList;
            User tempUser = new User();
            //int passedUserID = tempUser.getUserID(passedProfile);
            try
            {
                passList = tempPasses.getPasses(userID).List;
                passList += "|" + passedUserID + "|";

                objCmd.CommandText = "TP_ModifyPasses";
                objCmd.Parameters.AddWithValue("@passed", passList);
                objCmd.Parameters.AddWithValue("@passedBy", userID);
                result = objDB.DoUpdateUsingCmdObj(objCmd);
            }
            catch (NullReferenceException)
            {
                //list of passed list is empty, so add the first pass
                passList = passedUserID + "|";
                objCmd.CommandText = "TP_AddPass";
                objCmd.Parameters.AddWithValue("@passed", passList);
                objCmd.Parameters.AddWithValue("@passedBy", userID);
                result = objDB.DoUpdateUsingCmdObj(objCmd);
            }
            return result;
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
