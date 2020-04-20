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
using System.Drawing.Imaging;

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
                loadProfile(Session["RequestedProfile"].ToString());
            }
            
        }
        
        private void loadProfile(string username)
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/LoadUserProfile/" + username;

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            UserProfile profileObj = js.Deserialize<UserProfile>(data);

            imgProfilePic.ImageUrl = convertByteArrayToImage(username);
            lblFirstName.Text = profileObj.FirstName;
            lblLastName.Text = profileObj.LastName;
            txtAge.Text = profileObj.Age.ToString();
            txtHeightFeet.Text = profileObj.Height.Split('|')[0];
            txtHeightIn.Text = profileObj.Height.Split('|')[1];
            txtWeight.Text = profileObj.Weight.ToString();
            txtOccupation.Text = profileObj.Occupation;
            lblCommitment.Text = profileObj.Commitment;
            lblHaveKids.Text = profileObj.HaveKids;
            lblWantKids.Text = profileObj.WantKids;
            txtInterests.Text = profileObj.Interests;
            txtDescription.Text = profileObj.Description;
            txtPhone.Text = profileObj.PhoneNumber;
            txtEmail.Text = profileObj.Email;
            txtAddress.Text = profileObj.Address;
            txtCity.Text = profileObj.City;
            lblState.Text = profileObj.State;
            txtZip.Text = profileObj.Zip.ToString();
        }

        private string convertByteArrayToImage(string username)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetProfilePic";

            User tempUser = new User();
            int userID = tempUser.getUserID(username);

            objCmd.Parameters.AddWithValue("@userID", userID);
            DataSet profilePicDS = objDB.GetDataSetUsingCmdObj(objCmd);
            byte[] imageData = (byte[])objDB.GetField("Photo", 0);

            string imageUrl = "data:image/jpg;base64," + Convert.ToBase64String(imageData);
            return imageUrl;
        }
    }
}