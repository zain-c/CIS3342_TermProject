using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using DatingSiteLibrary;

namespace TP_WebAPI.Controllers
{
    [Route("api/DatingService/Profiles")]
    [ApiController]
    public class DatingProfileController : ControllerBase
    {
        String APIKey = "1234";
        [HttpGet]
        public string Test()
        {
            int value = 10;
            return "value: " + value;
        }

        [HttpGet("LoadUserProfile/{APIKey}/{username}")]
        public UserProfile loadProfile(string APIKey, string username)
        {
            if (APIKey == this.APIKey){
                User tempUser = new User();
                int userID = tempUser.getUserID(username);

                UserProfile profile = new UserProfile();

                return profile.retreiveUserProfileFromDB(userID);
            }
            else
            {
                return new UserProfile();
            }                     

        }

        [HttpGet("LoadPrivacySettings/{APIKey}/{username}")]
        public UserPrivacySettings loadPrivacySettings(string APIKey, string username)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);

                UserPrivacySettings privacySettings = new UserPrivacySettings();
                return privacySettings.retrievePrivacySettings(userID);
            }
            else
            {
                return new UserPrivacySettings();
            }
        }

        [HttpGet("LoadLikedList/{APIKey}/{username}")]
        public List<ProfileDisplayClass> loadLikedList(string APIKey, string username)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);

                LikedList tempList = new LikedList();
                try
                {
                    string listOfLikes = tempList.getLikes(userID).List;
                    int[] likedUserIDs = Array.ConvertAll(listOfLikes.Split('|'), int.Parse);

                    List<ProfileDisplayClass> likedUserProfiles = new List<ProfileDisplayClass>();
                    foreach (int id in likedUserIDs)
                    {
                        ProfileDisplayClass profileDisplay = new ProfileDisplayClass();
                        likedUserProfiles.Add(profileDisplay.retreiveProfileDisplayFromDB(id));
                    }
                    return likedUserProfiles;

                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            
        }

        [HttpGet("LoadPassList/{APIKey}/{username}")]
        public List<ProfileDisplayClass> loadPassList(string APIKey, string username)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);

                PassedList tempList = new PassedList();
                try
                {
                    string listOfPasses = tempList.getPasses(userID).List;
                    int[] passedUserIDs = Array.ConvertAll(listOfPasses.Split('|'), int.Parse);

                    List<ProfileDisplayClass> passedUserProfiles = new List<ProfileDisplayClass>();
                    foreach (int id in passedUserIDs)
                    {
                        ProfileDisplayClass profileDisplay = new ProfileDisplayClass();
                        passedUserProfiles.Add(profileDisplay.retreiveProfileDisplayFromDB(id));
                    }
                    return passedUserProfiles;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
            }
            else
            {
                return new List<ProfileDisplayClass>();
            }
        }

        [HttpPost("ModifyProfile/{APIKey}/{username}")]
        public bool modifyProfile(string APIKey, string username, [FromBody] UserProfile profile)
        {
            if (APIKey == this.APIKey)
            {
                if (profile != null)
                {
                    User tempUser = new User();
                    int userID = tempUser.getUserID(username);
                    UserProfile tempProfile = new UserProfile();
                    return tempProfile.modifyUserProfile(profile, userID);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [HttpPost("ModifyPrivacySettings/{APIKey}/{username}")]
        public bool modifyPrivacySettings(string APIKey, [FromBody] UserPrivacySettings settings, string username)
        {
            if (APIKey == this.APIKey)
            {
                if (settings != null)
                {
                    User tempUser = new User();
                    int userID = tempUser.getUserID(username);
                    UserPrivacySettings tempSettings = new UserPrivacySettings();
                    return tempSettings.modifyPrivacySettings(settings, userID);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [HttpPut("AddLikes/{APIKey}/{username}/{likedProfileUsername}")]
        public int addLikes(string APIKey, string username, string likedProfileUsername)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);
                int likedUserID = tempUser.getUserID(likedProfileUsername);

                LikedList tempLikes = new LikedList();
                return tempLikes.addLikeToDB(userID, likedUserID);
            }
            else {
                return 0;
            }
        }

        [HttpPut("AddPasses/{APIKey}/{username}/{passedProfileUsername}")]
        public int addPasses(string APIKey, string username, string passedProfileUsername)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);
                int passedUserID = tempUser.getUserID(passedProfileUsername);

                PassedList tempPasses = new PassedList();
                return tempPasses.addPassToDB(userID, passedUserID);
            }
            else{
                return 0;
            }
        }

        [HttpPut("AddBlock/{APIKey}/{username}/{blockedProfileUsername}")]
        public int addBlock(string APIKey, string username, string blockedProfileUsername)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);
                int blockedUserID = tempUser.getUserID(blockedProfileUsername);

                BlockedList tempBlock = new BlockedList();
                return tempBlock.addBlockToDB(userID, blockedUserID);
            }
            else {
                return 0;
            }
        }

        [HttpDelete("RemoveFromLikes/{APIKey}/{username}/{usernameToRemove}")]
        public bool removeFromLikes(string APIKey, string username, string usernameToRemove)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);
                int userIDToRemove = tempUser.getUserID(usernameToRemove);

                LikedList tempList = new LikedList();
                string likesList = tempList.getLikes(userID).List;
                int[] likedUserIDs = Array.ConvertAll(likesList.Split('|'), int.Parse);

                List<int> likes = new List<int>(likedUserIDs);
                string newLikesList;
                if (likes.Count == 1)
                {
                    //newLikesList = null;

                    DBConnect objDB = new DBConnect();
                    SqlCommand objCmd = new SqlCommand();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "TP_RemoveLikes";
                    objCmd.Parameters.AddWithValue("@likedBy", userID);
                    int result = objDB.DoUpdateUsingCmdObj(objCmd);
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    int index = likes.IndexOf(userIDToRemove);
                    likes.RemoveAt(index);
                    newLikesList = string.Join('|', likes);

                    DBConnect objDB = new DBConnect();
                    SqlCommand objCmd = new SqlCommand();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "TP_ModifyLikes";
                    objCmd.Parameters.AddWithValue("@liked", newLikesList);
                    objCmd.Parameters.AddWithValue("@likedBy", userID);
                    int result = objDB.DoUpdateUsingCmdObj(objCmd);

                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else{
                return false;
            }
        }

        [HttpDelete("RemoveFromPasses/{APIKey}/{username}/{usernameToRemove}")]
        public bool removeFromPasses(string APIKey, string username, string usernameToRemove)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userID = tempUser.getUserID(username);
                int userIDToRemove = tempUser.getUserID(usernameToRemove);

                PassedList tempList = new PassedList();
                string passesList = tempList.getPasses(userID).List;
                int[] passedUserIDs = Array.ConvertAll(passesList.Split('|'), int.Parse);

                List<int> passes = new List<int>(passedUserIDs);
                string newPassesList;
                if (passes.Count == 1)
                {
                    //newPassesList = null;

                    DBConnect objDB = new DBConnect();
                    SqlCommand objCmd = new SqlCommand();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "TP_RemovePasses";
                    objCmd.Parameters.AddWithValue("@passedBy", userID);
                    int result = objDB.DoUpdateUsingCmdObj(objCmd);
                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    int index = passes.IndexOf(userIDToRemove);
                    passes.RemoveAt(index);
                    newPassesList = string.Join('|', passes) + "|";

                    DBConnect objDB = new DBConnect();
                    SqlCommand objCmd = new SqlCommand();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "TP_ModifyPasses";
                    objCmd.Parameters.AddWithValue("@passed", newPassesList);
                    objCmd.Parameters.AddWithValue("@passedBy", userID);
                    int result = objDB.DoUpdateUsingCmdObj(objCmd);

                    if (result == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else{
                return false;
            }
        }
        
        [HttpPost("SendDateRequest/{APIKey}/{usernameFrom}/{usernameTo}")]
        public bool sendDateRequest(string APIKey, string usernameFrom, string usernameTo)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userIDFrom = tempUser.getUserID(usernameFrom);
                int userIDTo = tempUser.getUserID(usernameTo);

                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_SendDateRequest";
                objCmd.Parameters.AddWithValue("@requestTo", userIDTo);
                objCmd.Parameters.AddWithValue("@requestFrom", userIDFrom);

                int result = objDB.DoUpdateUsingCmdObj(objCmd);
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else{
                return false;
            }
        }

        [HttpGet("LoadSentDateRequests/{APIKey}/{usernameFrom}")]
        public List<DateRequest> loadSentDateRequests(string APIKey, string usernameFrom)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userIDFrom = tempUser.getUserID(usernameFrom);

                DateRequest tempRequest = new DateRequest();
                List<DateRequest> sentRequestList = tempRequest.getSentRequests(userIDFrom);
                return sentRequestList;
            }
            else{
                return new List<DateRequest>();
            }
        }

        [HttpGet("LoadReceivedDateRequests/{APIKey}/{usernameTo}")]
        public List<DateRequest> loadReceivedDateRequests(string APIKey, string usernameTo)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userIDTo = tempUser.getUserID(usernameTo);

                DateRequest tempRequest = new DateRequest();
                List<DateRequest> receivedRequestList = tempRequest.getReceivedRequests(userIDTo);
                return receivedRequestList;
            }
            else {
                return new List<DateRequest>();
            }
        }

        [HttpPut("UpdateDateRequestStatus/{APIKey}")]
        public bool updateDateRequestStatus(string APIKey, [FromBody]DateRequest dateRequest)
        {
            if (APIKey == this.APIKey)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_UpdateDateRequestStatus";
                objCmd.Parameters.AddWithValue("@status", dateRequest.Status);
                objCmd.Parameters.AddWithValue("@requestFrom", dateRequest.UserIDFrom);
                objCmd.Parameters.AddWithValue("@requestTo", dateRequest.UserIDTo);

                int result = objDB.DoUpdateUsingCmdObj(objCmd);
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else {
                return false;
            }
        }
        
        [HttpGet("GetDatePlanDetails/{APIKey}/{userIDFrom}/{userIDTo}")]
        public PlanDate getDatePlanDetails(string APIKey, int userIDFrom, int userIDTo)
        {
            if (APIKey == this.APIKey)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_GetDatePlanDetails";
                objCmd.Parameters.AddWithValue("@requestTo", userIDTo);
                objCmd.Parameters.AddWithValue("@requestFrom", userIDFrom);
                DataSet detailsDS = objDB.GetDataSetUsingCmdObj(objCmd);

                PlanDate planDate = new PlanDate();
                if (detailsDS.Tables[0].Rows.Count > 0)
                {
                    planDate.Date = detailsDS.Tables[0].Rows[0]["Date"].ToString();
                    planDate.Time = detailsDS.Tables[0].Rows[0]["Time"].ToString();
                    planDate.Description = detailsDS.Tables[0].Rows[0]["Description"].ToString();
                    return planDate;
                }
                else
                {
                    return planDate;
                }
            }
            else{
                return new PlanDate();
            }
        }

        [HttpPut("ModifyDatePlan/{APIKey}/{usernameTo}/{usernameFrom}")]
        public bool modifyDatePlan(string APIKey, string usernameTo, string usernameFrom, [FromBody]PlanDate planDate)
        {
            if (APIKey == this.APIKey)
            {
                User tempUser = new User();
                int userIDTo = tempUser.getUserID(usernameTo);
                int userIDFrom = tempUser.getUserID(usernameFrom);

                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_ModifyDatePlan";
                objCmd.Parameters.AddWithValue("@date", planDate.Date);
                objCmd.Parameters.AddWithValue("@time", planDate.Time);
                objCmd.Parameters.AddWithValue("@description", planDate.Description);
                objCmd.Parameters.AddWithValue("@requestFrom", userIDFrom);
                objCmd.Parameters.AddWithValue("@requestTo", userIDTo);

                int result = objDB.DoUpdateUsingCmdObj(objCmd);
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else{
                return false;
            }
        }
    }
}