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
    [Route("api/DatingService/Search")]
    [ApiController]
    public class SearchController : Controller
    {
        //searchParameters are passed in as a bar (|) delimited string. Search fields are 
        //ordered from left to right, top to bottom.


        [HttpGet("LoadSearchResults/Member")]
        public List<UserProfile> loadMemberSearch(string searchParameters)
        {
            //Parameters: City|State|Gender|Commitment|Have Kids|Want Kids|Interests
            List<UserProfile> profiles = new List<UserProfile>();

            string[] searchArray = searchParameters.Split('|');
            User tempUser = new User();
            //int userID = tempUser.getUserID(username);

            UserProfile profile = new UserProfile();
             

            return profiles;
        }

        [HttpGet("LoadSearchResults/Nonmember")]
        public UserProfile loadNonmemberSearch(string searchParameters)
        {
            //Parameters: City|State|Gender

            string[] searchArray = searchParameters.Split('|');
            User tempUser = new User();
            int userID = tempUser.getUserID("");

            UserProfile profile = new UserProfile();

            return profile.retreiveUserProfileFromDB(userID);
        }
    }
}