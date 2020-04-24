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

            

            

            UserProfile profile = new UserProfile();
             

            return profiles;
        }

        [HttpGet("LoadSearchResults/Nonmember/{searchParameters}")]
        public List<UserProfile> loadNonmemberSearch(string searchParameters)
        {
            //Parameters: City|State|Gender
            List<UserProfile> profiles = new List<UserProfile>();
            UserProfile profile;

            string[] searchArray = searchParameters.Split('|');


            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_NonMemberSearch";

            objCmd.Parameters.AddWithValue("@city", searchArray[0]);
            objCmd.Parameters.AddWithValue("@state", searchArray[1]);
            objCmd.Parameters.AddWithValue("@gender", searchArray[2]);

            DataSet searchResultsDS = objDB.GetDataSetUsingCmdObj(objCmd);
            
            foreach(DataRow result in searchResultsDS.Tables[0].Rows)
            {
                profile = new UserProfile();
                profile.FirstName = result["FirstName"].ToString();
                profile.LastName = result["LastName"].ToString();
                profile.City = result["HomeCity"].ToString();
                profile.State = result["HomeState"].ToString();
                profile.Gender = result["Gender"].ToString();
                //add photo
                profiles.Add(profile);
            }



            return profiles;
        }
    }
}