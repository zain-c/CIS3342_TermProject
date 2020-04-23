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
                if (Session["RequestedProfile"].ToString() != Session["Username"].ToString())
                {
                    btnEditProfile.Visible = false;
                    btnMemberView.Visible = false;
                }
                loadProfile(Session["RequestedProfile"].ToString());
                loadPrivacySettings(Session["RequestedProfile"].ToString());
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

            txtFirstName.Text = profileObj.FirstName;
            int width = 28 * (txtFirstName.Text.Length + 1) / 2;
            txtFirstName.Style.Add("width", width + "px");
            
            txtLastName.Text = profileObj.LastName;
            width = 28 * (txtLastName.Text.Length + 1) / 2;
            txtLastName.Style.Add("width", width + "px");

            lblGender.Text = profileObj.Gender;

            txtAge.Text = profileObj.Age.ToString();
            width = 16 * (txtAge.Text.Length + 1) / 2;
            txtAge.Style.Add("width", width + "px");

            txtHeightFeet.Text = profileObj.Height.Split('|')[0];
            width = 16 * (txtHeightFeet.Text.Length + 1) / 2;
            txtHeightFeet.Style.Add("width", width + "px");

            txtHeightIn.Text = profileObj.Height.Split('|')[1];
            width = 16 * (txtHeightIn.Text.Length + 1) / 2;
            txtHeightIn.Style.Add("width", width + "px");

            txtWeight.Text = profileObj.Weight.ToString();
            width = 16 * (txtWeight.Text.Length + 1) / 2;
            txtWeight.Style.Add("width", width + "px");

            txtOccupation.Text = profileObj.Occupation;
            width = 16 * (txtOccupation.Text.Length + 1) / 2;
            txtOccupation.Style.Add("width", width + "px");

            lblCommitment.Text = profileObj.Commitment;
            lblHaveKids.Text = profileObj.HaveKids;
            lblWantKids.Text = profileObj.WantKids;
            txtInterests.Text = profileObj.Interests;
            txtDescription.Text = profileObj.Description;

            txtPhone.Text = profileObj.PhoneNumber;
            width = 16 * (txtPhone.Text.Length + 1) / 2;
            txtPhone.Style.Add("width", width + "px");

            txtEmail.Text = profileObj.Email;
            width = 16 * (txtEmail.Text.Length + 1) / 2;
            txtEmail.Style.Add("width", width + "px");

            txtAddress.Text = profileObj.Address;
            width = 16 * (txtAddress.Text.Length + 1) / 2;
            txtAddress.Style.Add("width", width + "px");

            txtCity.Text = profileObj.City;
            width = 16 * (txtCity.Text.Length + 1) / 2;
            txtCity.Style.Add("width", width + "px");

            lblState.Text = profileObj.State;

            txtZip.Text = profileObj.Zip.ToString();
            width = 16 * (txtZip.Text.Length + 1) / 2;
            txtZip.Style.Add("width", width + "px");
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
            ddPrivacyGender.SelectedValue = privacyObj.Gender;
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
            txtFirstName.ReadOnly = false;
            txtLastName.ReadOnly = false;
            txtTitle.ReadOnly = false;
            txtAge.ReadOnly = false;
            txtHeightFeet.ReadOnly = false;
            txtHeightIn.ReadOnly = false;
            txtWeight.ReadOnly = false;
            txtOccupation.ReadOnly = false;

            lblGender.Visible = false;
            drpGender.Visible = true;
            drpGender.SelectedValue = lblGender.Text;

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
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtTitle.ReadOnly = true;
            txtAge.ReadOnly = true;
            txtHeightFeet.ReadOnly = true;
            txtHeightIn.ReadOnly = true;
            txtWeight.ReadOnly = true;
            txtOccupation.ReadOnly = true;
            lblGender.Visible = true;
            drpGender.Visible = false;
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
            lblErrorMsg.Text = string.Empty;
            lblErrorMsg.Visible = false;
            if (validateFields())
            {
                disableEdit();
                modifyProfile();
                modifyPrivacy();
                loadProfile(Session["RequestedProfile"].ToString());
                loadPrivacySettings(Session["RequestedProfile"].ToString());
            }
        }

        private void modifyProfile()
        {
            UserProfile profileObj = new UserProfile();
            profileObj.FirstName = txtFirstName.Text;
            profileObj.LastName = txtLastName.Text;
            profileObj.Title = txtTitle.Text;
            profileObj.Gender = drpGender.SelectedValue;
            profileObj.Age = int.Parse(txtAge.Text);
            profileObj.Height = txtHeightFeet.Text + "|" + txtHeightIn.Text;
            profileObj.Weight = int.Parse(txtWeight.Text);
            profileObj.Occupation = txtOccupation.Text;
            profileObj.Commitment = drpCommitment.SelectedValue;
            profileObj.HaveKids = drpHaveKids.SelectedValue;
            profileObj.WantKids = drpWantKids.SelectedValue;
            profileObj.Interests = txtInterests.Text;
            profileObj.Description = txtDescription.Text;
            profileObj.PhoneNumber = txtPhone.Text;
            profileObj.Email = txtEmail.Text;
            profileObj.Address = txtAddress.Text;
            profileObj.City = txtCity.Text;
            profileObj.State = ddState.SelectedValue;
            profileObj.Zip = int.Parse(txtZip.Text);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonProfileObj = js.Serialize(profileObj);

            try
            {
                string url = "https://localhost:44369/api/DatingService/Profiles/ModifyProfile/" + Session["Username"].ToString();
                WebRequest request = WebRequest.Create(url);
                request.Method = "PUT";
                request.ContentLength = jsonProfileObj.Length;
                request.ContentType = "application/json";

                //Write the JSON data to the Web Request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonProfileObj);
                writer.Flush();
                writer.Close();

                //Read the data from the Web Response
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                if (data != "true")
                {
                    lblErrorMsg.Text += "*A problem occured while updating your profile. <br />";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text += "*Error: " + ex.Message + "<br />";
            }
        }

        private void modifyPrivacy()
        {
            UserPrivacySettings settings = new UserPrivacySettings();
            settings.ProfilePic = ddPrivacyProfilePic.SelectedValue;
            settings.FirstName = ddPrivacyFirstName.SelectedValue;
            settings.LastName = ddPrivacyLastName.SelectedValue;
            settings.Title = ddPrivacyTitle.SelectedValue;
            settings.Age = ddPrivacyAge.SelectedValue;
            settings.Height = ddPrivacyHeight.SelectedValue;
            settings.Weight = ddPrivacyWeight.SelectedValue;
            settings.Occupation = ddPrivacyOccupation.SelectedValue;
            settings.Commitment = ddPrivacyCommitment.SelectedValue;
            settings.HaveKids = ddPrivacyHaveKids.SelectedValue;
            settings.WantKids = ddPrivacyWantKids.SelectedValue;
            settings.Interests = ddPrivacyInterests.SelectedValue;
            settings.Description = ddPrivacyDescription.SelectedValue;
            settings.Gender = ddPrivacyGender.SelectedValue;

            JavaScriptSerializer js = new JavaScriptSerializer();
            string jsonPrivacyObj = js.Serialize(settings);

            try
            {
                string url = "https://localhost:44369/api/DatingService/Profiles/ModifyPrivacySettings/" + Session["Username"].ToString();
                WebRequest request = WebRequest.Create(url);
                request.Method = "PUT";
                request.ContentLength = jsonPrivacyObj.Length;
                request.ContentType = "application/json";

                //Write the JSON data to the Web Request
                StreamWriter writer = new StreamWriter(request.GetRequestStream());
                writer.Write(jsonPrivacyObj);
                writer.Flush();
                writer.Close();

                //Read the data from the Web Response
                WebResponse response = request.GetResponse();
                Stream theDataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(theDataStream);
                string data = reader.ReadToEnd();
                reader.Close();
                response.Close();

                if (data != "true")
                {
                    lblErrorMsg.Text += "*A problem occured while updating your privacy settings. <br />";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text += "*Error: " + ex.Message + "<br />";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnEditProfile.Visible = true;
            btnMemberView.Visible = true;
            btnSaveChanges.Visible = false;
            btnCancel.Visible = false;
            disableEdit();
            loadProfile(Session["RequestedProfile"].ToString());
            loadPrivacySettings(Session["RequestedProfile"].ToString());
        }

        private bool validateFields()
        {
            bool valid = true;
            if(string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid name. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a title. <br />";
                lblErrorMsg.Visible = true;
            }
            if(drpGender.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select a gender. <br />";
                lblErrorMsg.Visible = true;
            }
            int age;
            if (string.IsNullOrWhiteSpace(txtAge.Text) || !int.TryParse(txtAge.Text, out age) || age <= 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid age. <br />";
                lblErrorMsg.Visible = true;
            }
            int heightFt, heightIn;
            if (string.IsNullOrWhiteSpace(txtHeightFeet.Text) || string.IsNullOrWhiteSpace(txtHeightIn.Text) || !int.TryParse(txtHeightFeet.Text, out heightFt) ||
               !int.TryParse(txtHeightIn.Text, out heightIn) || heightIn > 11 || heightIn < 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid height. <br />";
                lblErrorMsg.Visible = true;
            }
            int weight;
            if (string.IsNullOrWhiteSpace(txtWeight.Text) || !int.TryParse(txtWeight.Text, out weight) || weight <= 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid weight. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtOccupation.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter an occupation. <br />";
                lblErrorMsg.Visible = true;
            }            
            if (drpCommitment.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select your commitment preference. <br />";
                lblErrorMsg.Visible = true;
            }
            if ((drpHaveKids.SelectedIndex == 0) || (drpWantKids.SelectedIndex == 0))
            {
                valid = false;
                lblErrorMsg.Text += "*Please select your kids preference. <br />";
            }
            if (string.IsNullOrWhiteSpace(txtInterests.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter your interests. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a description. <br />";
                lblErrorMsg.Visible = true;
            }
            Int64 i;
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || (txtPhone.Text.Length != 10) || !Int64.TryParse(txtPhone.Text, out i))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid phone number. <br />";
                lblErrorMsg.Visible = true;
            }
            if(string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@") || !txtEmail.Text.Split('@')[1].Contains("."))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid email. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter an address. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a city. <br />";
                lblErrorMsg.Visible = true;
            }
            if(ddState.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select a state. <br />";
                lblErrorMsg.Visible = true;
            }
            int zip;
            if(string.IsNullOrWhiteSpace(txtZip.Text) || !int.TryParse(txtZip.Text, out zip) || txtZip.Text.Length != 5)
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid zip code. <br />";
                lblErrorMsg.Visible = true;
            }

            return valid;
        }
    }
}