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
        [HttpGet]
        public string Test()
        {
            int value = 10;
            return "value: " + value;
        }

        [HttpGet("LoadUserProfile/{username}")]
        public UserProfile loadProfile(string username)
        {
            User tempUser = new User();
            int userID = tempUser.getUserID(username);

            UserProfile profile = new UserProfile(); 

            return profile.retreiveUserProfileFromDB(userID);
        }

        [HttpGet("LoadPrivacySettings/{username}")]
        public UserPrivacySettings loadPrivacySettings(string username)
        {
            User tempUser = new User();
            int userID = tempUser.getUserID(username);

            UserPrivacySettings privacySettings = new UserPrivacySettings();
            return privacySettings.retrievePrivacySettings(userID);
        }

        [HttpGet("LoadLikedList/{username}")]
        public List<ProfileDisplayClass> loadLikedList(string username)
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
            catch(NullReferenceException)
            {
                return null;
            }
            
        }

        [HttpGet("LoadPassList/{username}")]
        public List<ProfileDisplayClass> loadPassList(string username)
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
            catch(NullReferenceException)
            {
                return null;
            }
        }

        [HttpPost("ModifyProfile/{username}")]
        public bool modifyProfile(string username, [FromBody] UserProfile profile)
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

        [HttpPost("ModifyPrivacySettings/{username}")]
        public bool modifyPrivacySettings([FromBody] UserPrivacySettings settings, string username)
        {
            if(settings != null)
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

        [HttpPut("AddLikes/{username}/{likedProfileUsername}")]
        public int addLikes(string username, string likedProfileUsername)
        {
            User tempUser = new User();
            int userID = tempUser.getUserID(username);
            int likedUserID = tempUser.getUserID(likedProfileUsername);

            LikedList tempLikes = new LikedList();
            return tempLikes.addLikeToDB(userID, likedUserID);
        }

        [HttpPut("AddPasses/{username}/{passedProfileUsername}")]
        public int addPasses(string username, string passedProfileUsername)
        {
            User tempUser = new User();
            int userID = tempUser.getUserID(username);
            int passedUserID = tempUser.getUserID(passedProfileUsername);

            PassedList tempPasses = new PassedList();  
            return tempPasses.addPassToDB(userID, passedUserID);
        }

        [HttpDelete("RemoveFromLikes/{username}/{usernameToRemove}")]
        public bool removeFromLikes(string username, string usernameToRemove)
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

        [HttpDelete("RemoveFromPasses/{username}/{usernameToRemove}")]
        public bool removeFromPasses(string username, string usernameToRemove)
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
                if(result == 1)
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
        
        [HttpPost("SendDateRequest/{usernameFrom}/{usernameTo}")]
        public bool sendDateRequest(string usernameFrom, string usernameTo)
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
            if(result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet("LoadSentDateRequests/{usernameFrom}")]
        public List<DateRequest> loadSentDateRequests(string usernameFrom)
        {
            User tempUser = new User();
            int userIDFrom = tempUser.getUserID(usernameFrom);

            DateRequest tempRequest = new DateRequest();
            List<DateRequest> sentRequestList = tempRequest.getSentRequests(userIDFrom);
            return sentRequestList;            
        }

        [HttpGet("LoadReceivedDateRequests/{usernameTo}")]
        public List<DateRequest> loadReceivedDateRequests(string usernameTo)
        {
            User tempUser = new User();
            int userIDTo = tempUser.getUserID(usernameTo);

            DateRequest tempRequest = new DateRequest();
            List<DateRequest> receivedRequestList = tempRequest.getReceivedRequests(userIDTo);
            return receivedRequestList;
        }

        [HttpPut("UpdateDateRequestStatus")]
        public bool updateDateRequestStatus([FromBody]DateRequest dateRequest)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_UpdateDateRequestStatus";
            objCmd.Parameters.AddWithValue("@status", dateRequest.Status);
            objCmd.Parameters.AddWithValue("@requestFrom", dateRequest.UserIDFrom);
            objCmd.Parameters.AddWithValue("@requestTo", dateRequest.UserIDTo);

            int result = objDB.DoUpdateUsingCmdObj(objCmd);
            if(result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}