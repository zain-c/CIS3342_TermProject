using DatingSiteLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;                        // needed for Stream and Stream Reader
using System.Net;                       // needed for the Web Request
using System.Web.Script.Serialization;  // needed for JSON serializers
using System.Web.UI;
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
            tblProfileList.BorderStyle = BorderStyle.None;
            tblProfileList.CellPadding = 5;
            tblProfileList.Style.Add("width", "95%");
            TableRow row = null;
            TableCell cell = null;
            ProfileDisplay profileDisplay = null;

            for (int i = 0; i < userProfiles.Count; i++)
            {
                row = new TableRow();
                cell = new TableCell();
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfileLikes_" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);
                cell.Controls.Add(profileDisplay);

                TableCell removeLikeCell = new TableCell();
                Button btnRemoveLike = new Button();
                btnRemoveLike.ID = "btnRemoveLike_" + i;
                btnRemoveLike.Text = "Remove from Likes";
                btnRemoveLike.CssClass = "btn btn-outline-danger";
                btnRemoveLike.Click += BtnRemoveLike_Click;
                removeLikeCell.Controls.Add(btnRemoveLike);
                
                row.Cells.Add(cell);
                row.Cells.Add(removeLikeCell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;        
        }

        private void BtnRemoveLike_Click(object sender, EventArgs e)
        {
            string rowClicked = ((Button)sender).ID.Split('_')[1];
            ProfileDisplay profileDisplay = ((ProfileDisplay)likes.FindControl("pdProfileLikes_" + rowClicked));
            string usernameToRemove = profileDisplay.Username;

            string url = "https://localhost:44369/api/DatingService/Profiles/RemoveFromLikes/" + Session["Username"].ToString() + "/" + usernameToRemove;
            WebRequest request = WebRequest.Create(url);
            request.Method = "DELETE";
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if(data != "true")
            {
                lblErrorMsg.Text += "*There was an error removing this user from your Likes. <br />";
                lblErrorMsg.Visible = true;
            }
            else
            {
                likes.Controls.RemoveAt(likes.Controls.Count - 1);
                loadLikes();
            }
        }

        private Table generatePassesListTable(List<ProfileDisplayClass> userProfiles)
        {
            Table tblProfileList = new Table();
            tblProfileList.HorizontalAlign = HorizontalAlign.Center;
            tblProfileList.GridLines = GridLines.Horizontal;
            tblProfileList.BorderStyle = BorderStyle.None;
            tblProfileList.CellPadding = 5;
            tblProfileList.Style.Add("width", "95%");
            TableRow row = null;
            TableCell cell = null;
            ProfileDisplay profileDisplay = null;

            for (int i = 0; i < userProfiles.Count; i++)
            {
                row = new TableRow();
                cell = new TableCell();
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfilePass_" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);                
                cell.Controls.Add(profileDisplay);

                TableCell likeUserCell = new TableCell();
                Button btnLikeUser = new Button();
                btnLikeUser.ID = "btnLikeUser_" + i;
                btnLikeUser.Text = "Like";
                btnLikeUser.CssClass = "btn btn-outline-success";
                btnLikeUser.Click += BtnLikeUser_Click;
                likeUserCell.Controls.Add(btnLikeUser);

                TableCell removePassCell = new TableCell();
                Button btnRemovePass = new Button();
                btnRemovePass.ID = "btnRemovePass_" + i;
                btnRemovePass.Text = "Remove from Passes";
                btnRemovePass.CssClass = "btn btn-outline-danger";
                btnRemovePass.Click += BtnRemovePass_Click;
                removePassCell.Controls.Add(btnRemovePass);

                row.Cells.Add(cell);
                row.Cells.Add(likeUserCell);
                row.Cells.Add(removePassCell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;
        }

        private void BtnLikeUser_Click(object sender, EventArgs e)
        {
            string rowClicked = ((Button)sender).ID.Split('_')[1];
            ProfileDisplay profileDisplay = ((ProfileDisplay)passes.FindControl("pdProfilePass_" + rowClicked));
            string usernameToTransfer = profileDisplay.Username; //get the username of the profile to remove from Passes and add to Likes

            //First remove the profile from Passes List
            string removeUrl = "https://localhost:44369/api/DatingService/Profiles/RemoveFromPasses/" + Session["Username"].ToString() + "/" + usernameToTransfer;
            WebRequest removeRequest = WebRequest.Create(removeUrl);
            removeRequest.Method = "DELETE";
            removeRequest.ContentLength = 0;

            WebResponse removeResponse = removeRequest.GetResponse();
            Stream removeDataStream = removeResponse.GetResponseStream();
            StreamReader removeReader = new StreamReader(removeDataStream);
            string removeData = removeReader.ReadToEnd();
            removeReader.Close();
            removeResponse.Close();

            //Now add that profile to Likes List
            string likeUrl = "https://localhost:44369/api/DatingService/Profiles/AddLikes/" + Session["Username"].ToString() + "/" + usernameToTransfer;
            WebRequest likeRequest = WebRequest.Create(likeUrl);
            likeRequest.Method = "PUT";
            likeRequest.ContentLength = 0;

            WebResponse likeResponse = likeRequest.GetResponse();
            Stream likeDataStream = likeResponse.GetResponseStream();
            StreamReader likeReader = new StreamReader(likeDataStream);
            string likeData = likeReader.ReadToEnd();
            likeReader.Close();
            likeResponse.Close();
                        
            if(removeData == "true" && likeData == "1") //if the transfer works, then reload Likes and Passes Lists
            {
                likes.Controls.RemoveAt(likes.Controls.Count - 1);
                loadLikes();

                passes.Controls.RemoveAt(passes.Controls.Count - 1);
                loadPasses();
            }
            else
            {
                lblErrorMsg.Text += "*There was an error moving this user to your Likes. <br />";
                lblErrorMsg.Visible = true;
            }
        }

        private void BtnRemovePass_Click(object sender, EventArgs e)
        {
            string rowClicked = ((Button)sender).ID.Split('_')[1];
            ProfileDisplay profileDisplay = ((ProfileDisplay)passes.FindControl("pdProfilePass_" + rowClicked));
            string usernameToRemove = profileDisplay.Username;

            string url = "https://localhost:44369/api/DatingService/Profiles/RemoveFromPasses/" + Session["Username"].ToString() + "/" + usernameToRemove;
            WebRequest request = WebRequest.Create(url);
            request.Method = "DELETE";
            request.ContentLength = 0;

            WebResponse response = request.GetResponse();
            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if (data != "true")
            {
                lblErrorMsg.Text += "*There was an error removing this user from your Passes. <br />";
                lblErrorMsg.Visible = true;
            }
            else
            {
                passes.Controls.RemoveAt(passes.Controls.Count - 1);
                loadPasses();
            }
        }
    }
}