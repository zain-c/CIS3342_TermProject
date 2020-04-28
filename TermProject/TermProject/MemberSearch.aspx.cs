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
using System.Collections;

namespace TermProject
{
    public partial class Search : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["Username"]))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                resultsContainer.Visible = false;

                if(string.IsNullOrEmpty((string)Session["LocationFilter"]) && string.IsNullOrEmpty((string)Session["StateFilter"]) && string.IsNullOrEmpty((string)Session["GenderFilter"]) && 
                   string.IsNullOrEmpty((string)Session["CommitmentFilter"]) && string.IsNullOrEmpty((string)Session["HaveKidsWant"]) && string.IsNullOrEmpty((string)Session["WantKidsFilter"]))
                {
                    // do nothing
                }
                else
                {
                    loadResults(Session["LocationFilter"].ToString(), Session["StateFilter"].ToString(), Session["GenderFilter"].ToString(), Session["CommitmentFilter"].ToString(), Session["HaveKidsFilter"].ToString(),
                                Session["WantKidsFilter"].ToString(), Session["OccupationFilter"].ToString());
                    
                    Session["SearchClicked"] = "false";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ArrayList displayedSearchResults = new ArrayList();
            lblErrorMsg.Text = "";
            
            if (txtOccupationFilter.Text.CompareTo("") == 0)
            {
                string blankOccupation = "BLANKNONE";

                
                if (validateFields())
                {

                    Session.Add("LocationFilter", txtLocationFilter.Text);
                    Session.Add("StateFilter", ddStateFilter.SelectedValue);
                    Session.Add("GenderFilter", ddGenderFilter.SelectedValue);
                    Session.Add("CommitmentFilter", ddCommitmentFilter.SelectedValue);
                    Session.Add("HaveKidsFilter", ddHaveKidsFilter.SelectedValue);
                    Session.Add("WantKidsFilter", ddWantKidsFilter.SelectedValue);
                    Session.Add("OccupationFilter", blankOccupation);

                    resultsDiv.Controls.RemoveAt(resultsDiv.Controls.Count - 1);
                    loadResults(txtLocationFilter.Text, ddStateFilter.SelectedValue, ddGenderFilter.SelectedValue, ddCommitmentFilter.SelectedValue, ddHaveKidsFilter.SelectedValue, ddWantKidsFilter.SelectedValue, blankOccupation);
                    
                    
                }
            }
            else if (validateFields())
            {
                Session.Add("LocationFilter", txtLocationFilter.Text);
                Session.Add("StateFilter", ddStateFilter.SelectedValue);
                Session.Add("GenderFilter", ddGenderFilter.SelectedValue);
                Session.Add("CommitmentFilter", ddCommitmentFilter.SelectedValue);
                Session.Add("HaveKidsFilter", ddHaveKidsFilter.SelectedValue);
                Session.Add("WantKidsFilter", ddWantKidsFilter.SelectedValue);
                Session.Add("OccupationFilter", txtOccupationFilter.Text);

                resultsDiv.Controls.RemoveAt(resultsDiv.Controls.Count - 1);
                loadResults(txtLocationFilter.Text, ddStateFilter.SelectedValue, ddGenderFilter.SelectedValue, ddCommitmentFilter.SelectedValue, ddHaveKidsFilter.SelectedValue, ddWantKidsFilter.SelectedValue, txtOccupationFilter.Text);
                
            }


            Session.Add("SearchClicked", "true");

            

        }

        protected bool validateFields()
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtLocationFilter.Text) || (txtLocationFilter.Text.Length > 50))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid City. <br />";
                lblErrorMsg.Visible = true;
            }
            if (ddStateFilter.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select a state. <br />";
                lblErrorMsg.Visible = true;
            }
            if (ddGenderFilter.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select a gender preference. <br />";
                lblErrorMsg.Visible = true;
            }
            if (ddCommitmentFilter.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select a commitment level. <br />";
                lblErrorMsg.Visible = true;
            }
            if (ddHaveKidsFilter.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select whether or not you want your matches to have children. <br />";
                lblErrorMsg.Visible = true;
            }
            if (ddWantKidsFilter.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select whether or not you want your matches to want children. <br />";
                lblErrorMsg.Visible = true;
            }
            if ((txtOccupationFilter.Text.Length > 50))
            {
                valid = false;
                lblErrorMsg.Text += "*Please Limit Occupation to 50 characters. <br />";
                lblErrorMsg.Visible = true;
            }

            return valid;
        }

        private void loadResults(string city, string state, string gender, string commitment, string haveKids, string wantKids, string occupation)
        {     
            string url = "https://localhost:44369/api/DatingService/Search/LoadSearchResults/Member/" + GlobalData.APIKey + "/" + city + "/" + state + "/" + gender + "/" + commitment + "/" + haveKids + "/" + wantKids + "/" + occupation;

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            MemberSearchResults[] profileResults = js.Deserialize<MemberSearchResults[]>(data);
            List<ProfileDisplayClass> profileResultsList = new List<ProfileDisplayClass>();
            
            foreach (MemberSearchResults result in profileResults)
            {
                User userResult = new User();
                int resultUserID = userResult.getUserID(result.Username);
               

                ProfileDisplayClass profileDisplay = new ProfileDisplayClass();
                profileResultsList.Add(profileDisplay.retreiveProfileDisplayFromDB(resultUserID));
            }

            ArrayList filterPassedProfileResults = new ArrayList();

            User currentUser = new User();
            int currentUserID = currentUser.getUserID(Session["Username"].ToString());

            PassedList currentUserPassedList = new PassedList();
            currentUserPassedList = currentUserPassedList.getPasses(currentUserID);

            foreach (ProfileDisplayClass result in profileResultsList)//adds search results that are not currently in the users passed list
            {
                User userResult = new User();
                int resultUserID = userResult.getUserID(result.Username);

                if ((currentUserPassedList == null && resultUserID != currentUserID)){
                    filterPassedProfileResults.Add(result);
                }
                else if ((currentUserPassedList != null && !currentUserPassedList.List.Contains(resultUserID.ToString())) && (resultUserID != currentUserID))
                {
                    filterPassedProfileResults.Add(result);
                }
            }

            ArrayList filterUserBlockedProfileResults = new ArrayList();
            BlockedList currentUserBlockedList = new BlockedList();
            currentUserBlockedList = currentUserBlockedList.getBlocked(currentUserID);

            foreach (ProfileDisplayClass result in filterPassedProfileResults)//adds search results that are not in the users blocked list
            {
                User userResult = new User();
                int resultUserID = userResult.getUserID(result.Username);

                if (currentUserBlockedList == null)
                {
                    filterUserBlockedProfileResults.Add(result);
                }
                else if (!currentUserBlockedList.List.Contains(resultUserID.ToString()))
                {
                    filterUserBlockedProfileResults.Add(result);
                }
            }

            ArrayList filterBlockedByProfileResults = new ArrayList();

            foreach (ProfileDisplayClass result in filterUserBlockedProfileResults.ToArray())//adds search results that do not have the current user blocked
            {
                User userResult = new User();
                int resultUserID = userResult.getUserID(result.Username);

                BlockedList resultBlockedList = new BlockedList();
                resultBlockedList = resultBlockedList.getBlocked(resultUserID);

                if (resultBlockedList == null)
                {
                    filterBlockedByProfileResults.Add(result);
                }
                else if (!resultBlockedList.List.Contains(resultUserID.ToString()))
                {
                    filterBlockedByProfileResults.Add(result);
                }
            }

            //return filterBlockedByProfileResults;
            List<ProfileDisplayClass> filteredProfiles = filterBlockedByProfileResults.Cast<ProfileDisplayClass>().ToList();
            //Table tblResults = generateResultsTable(filteredProfiles);
            if (filteredProfiles.Count != 0)
            {
                Table tblResults = generateResultsTable(filteredProfiles);
                resultsDiv.Controls.Add(tblResults);
                resultsContainer.Visible = true;
            }
            else
            {
                Label lblNoResults = new Label();
                lblNoResults.ID = "lblNoResults";
                lblNoResults.Text = "There no profiles that meet your criteria, please search again.";
                resultsDiv.Controls.Add(lblNoResults);
                resultsContainer.Visible = true;
            }

            
        }

        private Table generateResultsTable(List<ProfileDisplayClass> userProfiles)
        {
            Table tblProfileList = new Table();
            tblProfileList.HorizontalAlign = HorizontalAlign.Center;
            tblProfileList.GridLines = GridLines.Horizontal;
            tblProfileList.BorderStyle = BorderStyle.None;
            tblProfileList.CellPadding = 5;
            tblProfileList.Style.Add("width", "70%");


            TableRow row = null;
            TableCell cell = null;
            ProfileDisplay profileDisplay = null;

            for (int i = 0; i < userProfiles.Count; i++)
            {
                row = new TableRow();
                row.CssClass = "text-center";
                cell = new TableCell();
                cell.CssClass = "text-center";
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfile_" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);
                cell.Controls.Add(profileDisplay);
                
                row.Cells.Add(cell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;
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
    }
}