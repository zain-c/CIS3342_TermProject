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

namespace TermProject
{
    public partial class Profile1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty((string)Session["Username"]))
            //{
            //    Response.Redirect("Search.aspx");
            //}
            loadProfile("zain");
        }
        
        private void loadProfile(string username)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_LoadProfile";

            User tempUser = new User();
            int userID = tempUser.getUserID(username);

            objCmd.Parameters.AddWithValue("@userID", userID);
            GridView1.DataSource = objDB.GetDataSetUsingCmdObj(objCmd);
            GridView1.DataBind();
        }
    }
}