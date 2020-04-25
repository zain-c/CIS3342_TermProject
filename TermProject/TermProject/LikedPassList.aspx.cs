using DatingSiteLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;                        // needed for Stream and Stream Reader
using System.Net;                       // needed for the Web Request
using System.Web.Script.Serialization;  // needed for JSON serializers
using System.Web.UI.WebControls;
using Utilities;

namespace TermProject
{
    public partial class LikedPassList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["Username"]))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                loadLikes();
                loadPasses();
            }
        }

        private void loadLikes()
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/LoadLikedList/" + Session["Username"].ToString();
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if (string.IsNullOrEmpty(data))
            {
                Label lblNoLikes = new Label();
                lblNoLikes.Text = "You have not liked anyone yet";
                likes.Controls.Add(lblNoLikes);
            }
            else
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<ProfileDisplayClass> likedUserProfiles = js.Deserialize<List<ProfileDisplayClass>>(data);
                Table tblLikes = generateLikesListTable(likedUserProfiles);
                likes.Controls.Add(tblLikes);
            }            
        }

        private void loadPasses()
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/LoadPassList/" + Session["Username"].ToString();
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if (string.IsNullOrEmpty(data))
            {
                Label lblNoPasses = new Label();
                lblNoPasses.Text = "You have not passed on anyone yet";
                passes.Controls.Add(lblNoPasses);
            }
            else
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                List<ProfileDisplayClass> passedUserProfiles = js.Deserialize<List<ProfileDisplayClass>>(data);
                Table tblPasses = generatePassesListTable(passedUserProfiles);
                passes.Controls.Add(tblPasses);
            }
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

        private Table generateLikesListTable(List<ProfileDisplayClass> userProfiles)
        {
            Table tblProfileList = new Table();
            tblProfileList.HorizontalAlign = HorizontalAlign.Center;
            tblProfileList.GridLines = GridLines.Horizontal;
            TableRow row = null;
            TableCell cell = null;
            ProfileDisplay profileDisplay = null;

            for (int i = 0; i < userProfiles.Count; i++)
            {
                row = new TableRow();
                cell = new TableCell();
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfileLikes" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);
                Button viewProfile = (Button)profileDisplay.FindControl("btnViewProfile");
                cell.Controls.Add(profileDisplay);
                row.Cells.Add(cell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;        
        }

        private Table generatePassesListTable(List<ProfileDisplayClass> userProfiles)
        {
            Table tblProfileList = new Table();
            tblProfileList.HorizontalAlign = HorizontalAlign.Center;
            tblProfileList.GridLines = GridLines.Horizontal;
            TableRow row = null;
            TableCell cell = null;
            ProfileDisplay profileDisplay = null;

            for (int i = 0; i < userProfiles.Count; i++)
            {
                row = new TableRow();
                cell = new TableCell();
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfilePass" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);
                Button viewProfile = (Button)profileDisplay.FindControl("btnViewProfile");
                cell.Controls.Add(profileDisplay);
                row.Cells.Add(cell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;
        }
    }
}