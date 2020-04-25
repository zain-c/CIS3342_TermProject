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
    public class LikedList
    {
        string list;

        public LikedList()
        {

        }

        public string List
        {
            get { return list; }
            set { list = value; }
        }
        

        public int addLikeToDB(int userID, int likedUserID)
        {
            int result = 0;
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            
            
            LikedList tempLikes = new LikedList();
            string likeList;
            User tempUser = new User();
            //int likedUserID = tempUser.getUserID(likedProfile);
            try
            {
                likeList = tempLikes.getLikes(userID).List;
                likeList += "|" + likedUserID + "|";

                objCmd.CommandText = "TP_ModifyLikes";
                objCmd.Parameters.AddWithValue("@liked", likeList);
                objCmd.Parameters.AddWithValue("@likedBy", userID);
                result = objDB.DoUpdateUsingCmdObj(objCmd);
            }
            catch (NullReferenceException)
            {
                //list of likes is empty, so add the first like
                likeList = likedUserID + "|";
                objCmd.CommandText = "TP_AddLike";
                objCmd.Parameters.AddWithValue("@liked", likeList);
                objCmd.Parameters.AddWithValue("@likedBy", userID);
                result = objDB.DoUpdateUsingCmdObj(objCmd);
            }
            return result;
        }

        public LikedList getLikes(int userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetLikes";
            objCmd.Parameters.AddWithValue("@likedBy", userID);
            DataSet likesDS = objDB.GetDataSetUsingCmdObj(objCmd);

            LikedList likesList = new LikedList();
            if (likesDS.Tables[0].Rows.Count > 0)
            {
                likesList.List = likesDS.Tables[0].Rows[0]["Liked"].ToString().TrimEnd('|');
                return likesList;
            }
            else
            {
                return null;
            }
            
        }
    }
}
