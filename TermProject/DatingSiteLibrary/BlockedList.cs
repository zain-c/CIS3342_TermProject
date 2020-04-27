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
    public class BlockedList
    {

        string list;

        public BlockedList()
        {

        }
        public string List
        {
            get { return list; }
            set { list = value; }
        }

        public int addBlockToDB(int userID, int blockedUserID)
        {
            int result = 0;
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;


            BlockedList tempBlock = new BlockedList();
            string blockList;
            User tempUser = new User();
            //int passedUserID = tempUser.getUserID(passedProfile);
            try
            {
                blockList = tempBlock.getBlocked(userID).List;
                blockList += "|" + blockedUserID + "|";

                objCmd.CommandText = "TP_ModifyBlocked";
                objCmd.Parameters.AddWithValue("@blocked", blockList);
                objCmd.Parameters.AddWithValue("@blockedBy", userID);
                result = objDB.DoUpdateUsingCmdObj(objCmd);
            }
            catch (NullReferenceException)
            {
                //list of passed list is empty, so add the first pass
                blockList = blockedUserID + "|";
                objCmd.CommandText = "TP_AddBlock";
                objCmd.Parameters.AddWithValue("@blocked", blockList);
                objCmd.Parameters.AddWithValue("@blockedBy", userID);
                result = objDB.DoUpdateUsingCmdObj(objCmd);
            }
            return result;
        }

        public BlockedList getBlocked(int userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetBlocked";
            objCmd.Parameters.AddWithValue("@blockedBy", userID);
            DataSet passesDS = objDB.GetDataSetUsingCmdObj(objCmd);

            BlockedList blockedList = new BlockedList();
            if (passesDS.Tables[0].Rows.Count > 0)
            {
                if (objDB.GetField("Blocked", 0) == DBNull.Value)
                {
                    return null;
                }
                else
                {
                    blockedList.List = passesDS.Tables[0].Rows[0]["Blocked"].ToString().TrimEnd('|');
                    return blockedList;
                }
            }
            else
            {
                return null;
            }

        }



    }
}
