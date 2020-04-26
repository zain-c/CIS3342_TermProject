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
    public partial class DateRequests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["Username"]))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                loadSentRequests();
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

        private void loadSentRequests()
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/LoadSentDateRequests/" + Session["Username"].ToString();
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<DateRequest> sentDateRequests = js.Deserialize<List<DateRequest>>(data);
            if(sentDateRequests.Count == 0)
            {
                Label lblNoSentRequests = new Label();
                lblNoSentRequests.Text = "You have not sent any date requests yet.";
                sent.Controls.Add(lblNoSentRequests);
            }
            else
            {
                List<ProfileDisplayClass> sentRequestProfiles = new List<ProfileDisplayClass>();
                foreach (DateRequest dateRequest in sentDateRequests)
                {
                    int userIDTo = dateRequest.UserIDTo;

                    ProfileDisplayClass profileDisplay = new ProfileDisplayClass();
                    sentRequestProfiles.Add(profileDisplay.retreiveProfileDisplayFromDB(userIDTo));
                }
                Table tblSentDateRequests = generateSentRequestsTable(sentDateRequests, sentRequestProfiles);
                sent.Controls.Add(tblSentDateRequests);
            }
        }

        private Table generateSentRequestsTable(List<DateRequest> sentDateRequests, List<ProfileDisplayClass> userProfiles)
        {
            Table tblProfileList = new Table();
            tblProfileList.HorizontalAlign = HorizontalAlign.Center;
            tblProfileList.GridLines = GridLines.Horizontal;
            tblProfileList.BorderStyle = BorderStyle.None;
            tblProfileList.CellPadding = 5;
            tblProfileList.Style.Add("width", "95%");

            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headerCellSentTo = new TableHeaderCell();
            headerCellSentTo.ID = "headerCellSentTo";
            headerCellSentTo.Text = "Sent To";
            TableHeaderCell headerCellStatus = new TableHeaderCell();
            headerCellStatus.ID = "headerCellSentTo";
            headerCellStatus.Text = "Sent To";

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

                TableCell statusCell = new TableCell();
                Label lblStatus = new Label();
                lblStatus.ID = "lblStatus_" + i;
                lblStatus.Text = sentDateRequests[i].Status;
                statusCell.Controls.Add(lblStatus);

                row.Cells.Add(cell);
                row.Cells.Add(statusCell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;
        }


    }
}