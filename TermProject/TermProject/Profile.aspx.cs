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
                Response.Redirect("Login.aspx");
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

        private void loadPrivacySettings(string username)
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/LoadPrivacySettings/" + username;

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            UserPrivacySettings privacyObj = js.Deserialize<UserPrivacySettings>(data);

            ddPrivacyProfilePic.SelectedValue = privacyObj.ProfilePic;
            ddPrivacyFirstName.SelectedValue = privacyObj.FirstName;
            ddPrivacyLastName.SelectedValue = privacyObj.LastName;
            ddPrivacyTitle.SelectedValue = privacyObj.Title;
            ddPrivacyAge.SelectedValue = privacyObj.Age;
            ddPrivacyHeight.SelectedValue = privacyObj.Height;
            ddPrivacyWeight.SelectedValue = privacyObj.Weight;
            ddPrivacyOccupation.SelectedValue = privacyObj.Occupation;
            ddPrivacyCommitment.SelectedValue = privacyObj.Commitment;
            ddPrivacyHaveKids.SelectedValue = privacyObj.HaveKids;
            ddPrivacyWantKids.SelectedValue = privacyObj.WantKids;
            ddPrivacyInterests.SelectedValue = privacyObj.Interests;
            ddPrivacyDescription.SelectedValue = privacyObj.Description;
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

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            btnEditProfile.Visible = false;
            btnMemberView.Visible = false;
            btnSaveChanges.Visible = true;
            btnCancel.Visible = true;
            privacySettings.Visible = true;
            enableEdit();
        }

        private void enableEdit()
        {
            fileProfilePic.Visible = true;
            txtTitle.ReadOnly = false;
            txtAge.ReadOnly = false;
            txtHeightFeet.ReadOnly = false;
            txtHeightIn.ReadOnly = false;
            txtWeight.ReadOnly = false;
            txtOccupation.ReadOnly = false;

            lblCommitment.Visible = false;
            drpCommitment.Visible = true;
            drpCommitment.SelectedValue = lblCommitment.Text;

            lblHaveKids.Visible = false;
            drpHaveKids.Visible = true;
            drpHaveKids.SelectedValue = lblHaveKids.Text;

            lblWantKids.Visible = false;
            drpWantKids.Visible = true;
            drpWantKids.SelectedValue = lblWantKids.Text;

            txtInterests.ReadOnly = false;
            txtDescription.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtAddress.ReadOnly = false;
            txtCity.ReadOnly = false;

            lblState.Visible = false;
            ddState.Visible = true;
            ddState.SelectedValue = lblState.Text;

            txtZip.ReadOnly = false;
        }

        private void disableEdit()
        {
            fileProfilePic.Visible = false;
            txtTitle.ReadOnly = true;
            txtAge.ReadOnly = true;
            txtHeightFeet.ReadOnly = true;
            txtHeightIn.ReadOnly = true;
            txtWeight.ReadOnly = true;
            txtOccupation.ReadOnly = true;
            lblCommitment.Visible = true;
            drpCommitment.Visible = false;
            lblHaveKids.Visible = true;
            drpHaveKids.Visible = false;
            lblWantKids.Visible = true;
            drpWantKids.Visible = false;
            txtInterests.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtCity.ReadOnly = true;
            lblState.Visible = true;
            ddState.Visible = false;
            txtZip.ReadOnly = true;
            privacySettings.Visible = false;
        }

        protected void btnMemberView_Click(object sender, EventArgs e)
        {

        }

        protected void btnNormalView_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
             
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            disableEdit();
            loadProfile(Session["RequestedProfile"].ToString());
        }
    }
}