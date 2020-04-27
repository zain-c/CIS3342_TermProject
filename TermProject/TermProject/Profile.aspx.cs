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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (string.IsNullOrEmpty((string)Session["Username"]))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {                    
                    loadProfile(Session["RequestedProfile"].ToString());
                    loadPrivacySettings(Session["RequestedProfile"].ToString());

                    if (Session["RequestedProfile"].ToString() != Session["Username"].ToString())
                    {
                        //if the profile selected is from the search results 
                        //then display the profile layout for other members
                        otherMemberView();
                        UserProfile tempProfile = new UserProfile();
                        if(tempProfile.checkIfUsersLikeEachOther(Session["Username"].ToString(), Session["RequestedProfile"].ToString()))
                        {
                            btnDateRequest.Visible = true;

                            if (tempProfile.checkIfUserSentDateRequest(Session["Username"].ToString(), Session["RequestedProfile"].ToString()) ||
                                tempProfile.checkIfUserReceivedDateRequest(Session["RequestedProfile"].ToString(), Session["Username"].ToString()))
                            {
                                if(tempProfile.checkIfSentRequestAccepted(Session["Username"].ToString(), Session["RequestedProfile"].ToString()) ||
                                   tempProfile.checkIfReceivedRequestAccepted(Session["RequestedProfile"].ToString(), Session["Username"].ToString()))
                                {
                                    contactInfo.Visible = true;
                                }                                
                                btnDateRequest.Enabled = false;
                                btnDateRequest.ToolTip = "You've already sent or received a date request from this user.";
                            }
                            
                        }
                        else
                        {
                            btnLike.Visible = true;
                            btnPass.Visible = true;
                            btnBlock.Visible = true;

                            if (tempProfile.checkIfUserLikesOtherUser(Session["Username"].ToString(), Session["RequestedProfile"].ToString()) ||
                                tempProfile.checkIfUserPassedOtherUser(Session["Username"].ToString(), Session["RequestedProfile"].ToString()))
                            {
                                btnLike.Enabled = false;
                                btnLike.ToolTip = "See Manage Likes and Pass Lists to manage your likes and passes.";

                                btnPass.Enabled = false;
                                btnPass.ToolTip = "See Manage Likes and Pass Lists to manage your likes and passes.";
                            }
                            
                        }
                    }
                }
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

            txtTitle.Text = profileObj.Title;         
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
            string imageUrl;
            if (objDB.GetField("Photo", 0) == DBNull.Value)
            {
                imageUrl = null;
            }
            else
            {
                byte[] imageData = (byte[])objDB.GetField("Photo", 0);
                imageUrl = "data:image/jpg;base64," + Convert.ToBase64String(imageData);
                
            }
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
            btnNormalView.Visible = true;
            otherMemberView();
        }

        private void otherMemberView()
        {
            btnMemberView.Visible = false;
            btnEditProfile.Visible = false;            
            contactInfo.Visible = false;
            if (ddPrivacyProfilePic.SelectedValue == "Nonvisible")
            {
                imgProfilePic.Visible = false;
            }
            if (ddPrivacyFirstName.SelectedValue == "Nonvisible")
            {
                txtFirstName.Visible = false;
            }
            if (ddPrivacyLastName.SelectedValue == "Nonvisible")
            {
                txtLastName.Visible = false;
            }
            if (ddPrivacyTitle.SelectedValue == "Nonvisible")
            {
                txtTitle.Visible = false;
            }
            if (ddPrivacyGender.SelectedValue == "Nonvisible")
            {
                lblGender1.Visible = false;
                lblGender.Visible = false;
            }
            if (ddPrivacyAge.SelectedValue == "Nonvisible")
            {
                lblAge.Visible = false;
                txtAge.Visible = false;
            }
            if (ddPrivacyHeight.SelectedValue == "Nonvisible")
            {
                lblHeight.Visible = false;
                lblFeet.Visible = false;
                lblInches.Visible = false;
                txtHeightFeet.Visible = false;
                txtHeightIn.Visible = false;
            }
            if (ddPrivacyWeight.SelectedValue == "Nonvisible")
            {
                lblWeight.Visible = false;
                txtWeight.Visible = false;
            }
            if (ddPrivacyOccupation.SelectedValue == "Nonvisible")
            {
                lblOccupation.Visible = false;
                txtOccupation.Visible = false;
            }
            if (ddPrivacyCommitment.SelectedValue == "Nonvisible")
            {
                lblCommitment1.Visible = false;
                lblCommitment.Visible = false;
            }
            if (ddPrivacyHaveKids.SelectedValue == "Nonvisible")
            {
                lblHaveKids1.Visible = false;
                lblHaveKids.Visible = false;
            }
            if (ddPrivacyWantKids.SelectedValue == "Nonvisible")
            {
                lblWantKids1.Visible = false;
                lblWantKids.Visible = false;
            }
            if (ddPrivacyInterests.SelectedValue == "Nonvisible")
            {
                lblInterests.Visible = false;
                txtInterests.Visible = false;
            }
            if (ddPrivacyDescription.SelectedValue == "Nonvisible")
            {
                lblDescription.Visible = false;
                txtDescription.Visible = false;
            }
        }

        protected void btnNormalView_Click(object sender, EventArgs e)
        {
            btnMemberView.Visible = true;
            btnEditProfile.Visible = true;
            btnNormalView.Visible = false;
            contactInfo.Visible = true;

            imgProfilePic.Visible = true;
            txtFirstName.Visible = true;
            txtLastName.Visible = true;
            txtTitle.Visible = true;
            lblGender1.Visible = true;
            lblGender.Visible = true;
            lblAge.Visible = true;
            txtAge.Visible = true;
            lblHeight.Visible = true;
            lblFeet.Visible = true;
            lblInches.Visible = true;
            txtHeightFeet.Visible = true;
            txtHeightIn.Visible = true;
            lblWeight.Visible = true;
            txtWeight.Visible = true;
            lblOccupation.Visible = true;
            txtOccupation.Visible = true;
            lblCommitment1.Visible = true;
            lblCommitment.Visible = true;
            lblHaveKids1.Visible = true;
            lblHaveKids.Visible = true;
            lblWantKids1.Visible = true;
            lblWantKids.Visible = true;
            lblInterests.Visible = true;
            txtInterests.Visible = true;
            lblDescription.Visible = true;
            txtDescription.Visible = true;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            lblErrorMsg.Visible = false;

            if (validateFields())
            {
                disableEdit();
                modifyProfilePic();
                modifyProfile();
                modifyPrivacy();
                loadProfile(Session["RequestedProfile"].ToString());
                loadPrivacySettings(Session["RequestedProfile"].ToString());
                btnEditProfile.Visible = true;
                btnMemberView.Visible = true;
                btnSaveChanges.Visible = false;
                btnCancel.Visible = false;
            }
            
        }

        private void modifyProfilePic()
        {
            if (IsPostBack)
            {
                if (fileProfilePic.HasFile)
                {
                    int result = 0, imageSize;
                    string fileExt, imageName;
                    User tempUser = new User();
                    int userID = tempUser.getUserID(Session["Username"].ToString());

                    imageSize = fileProfilePic.PostedFile.ContentLength;
                    byte[] imageData = new byte[imageSize];
                    fileProfilePic.PostedFile.InputStream.Read(imageData, 0, imageSize);
                    imageName = fileProfilePic.PostedFile.FileName;
                    fileExt = imageName.Substring(imageName.LastIndexOf("."));
                    fileExt = fileExt.ToLower();

                    if (fileExt == ".jpg")
                    {
                        DBConnect objDB = new DBConnect();
                        SqlCommand objCmd = new SqlCommand();
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "TP_ModifyProfilePic";
                        objCmd.Parameters.AddWithValue("@imageData", imageData);
                        objCmd.Parameters.AddWithValue("@userID", userID);

                        result = objDB.DoUpdateUsingCmdObj(objCmd);

                        if (result != 1)
                        {
                            lblErrorMsg.Text += "*There was an error updating your profile picture. <br />";
                            lblErrorMsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblErrorMsg.Text += "*Only jpg file types supported. <br />";
                        lblErrorMsg.Visible = true;
                    }
                }
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
                request.Method = "POST";
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
                    lblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text += "*Error: " + ex.Message + "<br />";
                lblErrorMsg.Visible = true;
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
                request.Method = "POST";
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
                    lblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text += "*Error: " + ex.Message + "<br />";
                lblErrorMsg.Visible = true;
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

        protected void btnLike_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/AddLikes/" + Session["Username"].ToString() + "/" + Session["RequestedProfile"].ToString();
            WebRequest request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();
            
            if(data == "1")
            {
                lblErrorMsg.Text += "User successfully added to Likes. <br />";
                lblErrorMsg.Visible = true;

            }
        }

        protected void btnPass_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/AddPasses/" + Session["Username"].ToString() + "/" + Session["RequestedProfile"].ToString();
            WebRequest request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if (data == "1")
            {
                lblErrorMsg.Text += "User successfully added to Passes. <br />";
                lblErrorMsg.Visible = true;
            }
        }

        protected void btnDateRequest_Click(object sender, EventArgs e)
        {
            
            string url = "https://localhost:44369/api/DatingService/Profiles/SendDateRequest/" + Session["Username"].ToString() + "/" + Session["RequestedProfile"].ToString();
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if(data == "true")
            {
                lblErrorMsg.Text += "Date Request Sent <br />";
                lblErrorMsg.Visible = true;
                btnDateRequest.Enabled = false;
            }
            else
            {
                lblErrorMsg.Text += "*There was an error sending a date request. <br />";
                lblErrorMsg.Visible = true;
            }
        }

        protected void btnBlock_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/AddBlock/" + Session["Username"].ToString() + "/" + Session["RequestedProfile"].ToString();
            WebRequest request = WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if (data == "1")
            {
                lblErrorMsg.Text += "User successfully added to Blocked List. <br />";
                lblErrorMsg.Visible = true;
            }
        }
    }
}