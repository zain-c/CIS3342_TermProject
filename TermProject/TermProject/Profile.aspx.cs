using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using DatingSiteLibrary;
using System.Web.Script.Serialization;  // needed for JSON serializers
using System.IO;                        // needed for Stream and Stream Reader
using System.Net;                       // needed for the Web Request

namespace TermProject
{
    public partial class Profile1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["Username"]))
            {
                Response.Redirect("Search.aspx");
            }
            else
            {
                //loadProfile(Session["RequestedProfile"].ToString());
            }
            
        }
        
        /*private void loadProfile(string username)
        {
            string url = "http://cis-iis2.temple.edu/Spring2020/CIS3342_tug85523/WebAPITest/api/DatingService/Profiles/LoadUserProfile/" + username;

            WebRequest request = WebRequest.Create(url);
            we
        }*/
    }
}