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
    }
}