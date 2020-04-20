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
        //GET/api/DatingService/Profiles/LoadUserProfile/id
        [HttpGet("/LoadUserProfile/{id}")]
        public UserProfile loadProfile(string username)
        {
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "TP_LoadProfile";
            objCommand.Parameters.AddWithValue("@userID", username);

            DBConnect objDB = new DBConnect();
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            DataRow record;

            UserProfile profile = new UserProfile();

            if (ds.Tables[0].Rows.Count != 0)
            {
                record = ds.Tables[0].Rows[0];
                profile.Age = int.Parse(record["Age"].ToString());
                profile.Commitment = record["Commitment"].ToString();
                profile.Description = record["Description"].ToString();
                profile.Height = record["Height"].ToString();
                profile.Interests = record["Interests"].ToString();
                profile.Occupation = record["Occupation"].ToString();
                profile.PhoneNumber = record["Phone"].ToString();
                profile.HaveKids = record["haveKids"].ToString();
                profile.WantKids = record["wantKids"].ToString();
                profile.Title = record["Title"].ToString();
                profile.Weight = int.Parse(record["Weight"].ToString());
            }

            return profile;
        }
    }
}