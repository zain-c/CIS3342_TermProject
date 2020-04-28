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
        string APIKey = "1234";

        [HttpGet("LoadSearchResults/Member/{APIKey}/{city}/{state}/{gender}/{commitment}/{haveKids}/{wantKids}/{occupation}")]
        public List<MemberSearchResults> loadMemberSearch(string APIKey, string city, string state, string gender, string commitment, string haveKids, string wantKids, string occupation)
        {
            if (APIKey == this.APIKey)
            {
                List<MemberSearchResults> profiles = new List<MemberSearchResults>();
                MemberSearchResults profile;


                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_MemberSearch";

                objCmd.Parameters.AddWithValue("@city", city);
                objCmd.Parameters.AddWithValue("@state", state);
                objCmd.Parameters.AddWithValue("@gender", gender);
                objCmd.Parameters.AddWithValue("@commitment", commitment);
                objCmd.Parameters.AddWithValue("@haveKids", haveKids);
                objCmd.Parameters.AddWithValue("@wantKids", wantKids);
                objCmd.Parameters.AddWithValue("@occupation", occupation);

                DataSet searchResultsDS = objDB.GetDataSetUsingCmdObj(objCmd);

                if (searchResultsDS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow result in searchResultsDS.Tables[0].Rows)
                    {
                        profile = new MemberSearchResults();
                        profile.Title = result["Title"].ToString();
                        profile.Username = result["Username"].ToString();
                        profile.FirstName = result["FirstName"].ToString();
                        profile.LastName = result["LastName"].ToString();
                        profile.City = result["HomeCity"].ToString();
                        profile.State = result["HomeState"].ToString();
                        profile.Gender = result["Gender"].ToString();
                        profile.Commitment = result["Commitment"].ToString();
                        profile.HaveKids = result["haveKids"].ToString();
                        profile.WantKids = result["wantKids"].ToString();
                        profile.Occupation = result["Occupation"].ToString();
                        profiles.Add(profile);
                    }
                    return profiles;
                }
                else
                {
                    return profiles;
                }
            }
            else
            {
                return new List<MemberSearchResults>();
            }
            
        }

        [HttpGet("LoadSearchResults/Nonmember/{APIKey}/{city}/{state}/{gender}")]
        public List<SearchResult> loadNonmemberSearch(string APIKey, string city, string state, string gender)
        {
            if (APIKey == this.APIKey)
            {
                List<SearchResult> profiles = new List<SearchResult>();
                SearchResult profile;


                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_NonMemberSearch";

                objCmd.Parameters.AddWithValue("@city", city);
                objCmd.Parameters.AddWithValue("@state", state);
                objCmd.Parameters.AddWithValue("@gender", gender);

                DataSet searchResultsDS = objDB.GetDataSetUsingCmdObj(objCmd);

                foreach (DataRow result in searchResultsDS.Tables[0].Rows)
                {
                    profile = new SearchResult();
                    profile.Username = result["Username"].ToString();
                    profile.FirstName = result["FirstName"].ToString();
                    profile.LastName = result["LastName"].ToString();
                    profile.City = result["HomeCity"].ToString();
                    profile.State = result["HomeState"].ToString();
                    profile.Gender = result["Gender"].ToString();
                    profiles.Add(profile);
                }

                return profiles;
            }
            else
            {
                return new List<SearchResult>();
            }
        }
    }
}