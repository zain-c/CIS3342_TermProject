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
    public partial class Messaging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["Username"]))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                //Session.Add("MessageToUsername", "PamB"); //for testing
                //MessagingDisplay1.DataBind();
                loadConversations();
            }
        }
        
        private void loadConversations()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetUserMessages";
            objCmd.Parameters.AddWithValue("@username", Session["Username"].ToString());
            DataSet conversationsDS = objDB.GetDataSetUsingCmdObj(objCmd);
            string usernameOne;
            string usernameTwo;

            User tempUser = new User();
            int userID;
            ProfileDisplayClass profileDisplay;
            List <ProfileDisplayClass> userProfiles = new List<ProfileDisplayClass>();
            if(conversationsDS.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow row in conversationsDS.Tables[0].Rows)
                {
                    usernameOne = row["UserOneUsername"].ToString();
                    usernameTwo = row["UserTwoUsername"].ToString();
                    if (usernameOne.Equals(Session["Username"].ToString()))
                    {
                        userID = tempUser.getUserID(usernameTwo);
                        profileDisplay = new ProfileDisplayClass();
                        userProfiles.Add(profileDisplay.retreiveProfileDisplayFromDB(userID));
                    }
                    else if(usernameTwo.Equals(Session["Username"].ToString()))
                    {
                        userID = tempUser.getUserID(usernameOne);
                        profileDisplay = new ProfileDisplayClass();
                        userProfiles.Add(profileDisplay.retreiveProfileDisplayFromDB(userID));
                    }
                }

                Table tblConversations = generateConversationsTable(userProfiles);
                conversationsDiv.Controls.Add(tblConversations);
            }
            
        }

        private Table generateConversationsTable(List<ProfileDisplayClass> userProfiles)
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
                cell = new TableCell();
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfile_" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);
                cell.Controls.Add(profileDisplay);
                
                TableCell messageCell = new TableCell();
                Button btnMessage = new Button();
                btnMessage.ID = "btnMessage_" + i;
                btnMessage.Click += BtnMessage_Click;
                btnMessage.Text = "Message";
                btnMessage.CssClass = "btn btn-outline-success";
                messageCell.Controls.Add(btnMessage);

                TableCell deleteCell = new TableCell();
                Button btnDelete = new Button();
                btnDelete.ID = "btnDelete_" + i;
                btnDelete.Click += BtnDelete_Click;
                btnDelete.Text = "Delete Conversation";
                btnDelete.CssClass = "btn btn-outline-danger";
                deleteCell.Controls.Add(btnDelete);

                row.Cells.Add(cell);
                row.Cells.Add(messageCell);
                row.Cells.Add(deleteCell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;
            int rowNum = int.Parse(btnDelete.ID.Split('_')[1]);
            ProfileDisplay profileDisplay = ((ProfileDisplay)conversationsDiv.FindControl("pdProfile_" + rowNum));

            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_DeleteConversation";
            objCmd.Parameters.AddWithValue("@usernameOne", Session["Username"].ToString());
            objCmd.Parameters.AddWithValue("@usernameTwo", profileDisplay.Username);

            int result = objDB.DoUpdateUsingCmdObj(objCmd);
            if(result == 1)
            {
                conversationsDiv.Controls.RemoveAt(conversationsDiv.Controls.Count - 1);
                loadConversations();
            }
        }

        private void BtnMessage_Click(object sender, EventArgs e)
        {
            Button btnMessage = (Button)sender;
            int rowNum = int.Parse(btnMessage.ID.Split('_')[1]);
            ProfileDisplay profileDisplay = ((ProfileDisplay)conversationsDiv.FindControl("pdProfile_" + rowNum));

            Session.Add("MessageToUsername", profileDisplay.Username);

            MessagingDisplay1.To = profileDisplay.Username;
            MessagingDisplay1.DataBind();

            sendMsgDiv.Visible = true;
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