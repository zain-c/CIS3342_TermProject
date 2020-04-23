using System;
using System.Collections.Generic;
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

        [HttpPut("ModifyProfile/{username}")]
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

        [HttpPut("ModifyPrivacySettings/{username}")]
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
    }
}