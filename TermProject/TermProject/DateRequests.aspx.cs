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
                loadReceivedRequests();
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

        private void loadReceivedRequests()
        {
            string url = "https://localhost:44369/api/DatingService/Profiles/LoadReceivedDateRequests/" + Session["Username"].ToString();
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            List<DateRequest> receivedDateRequests = js.Deserialize<List<DateRequest>>(data);
            if (receivedDateRequests.Count == 0)
            {
                Label lblNoReceivedRequests = new Label();
                lblNoReceivedRequests.Text = "You have not received any date requests yet.";
                received.Controls.Add(lblNoReceivedRequests);
            }
            else
            {
                List<ProfileDisplayClass> receivedRequestProfiles = new List<ProfileDisplayClass>();
                foreach (DateRequest dateRequest in receivedDateRequests)
                {
                    int userIDFrom = dateRequest.UserIDFrom;

                    ProfileDisplayClass profileDisplay = new ProfileDisplayClass();
                    receivedRequestProfiles.Add(profileDisplay.retreiveProfileDisplayFromDB(userIDFrom));
                }
                Table tblReceivedDateRequests = generateReceivedRequestsTable(receivedDateRequests, receivedRequestProfiles);
                received.Controls.Add(tblReceivedDateRequests);
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
            headerRow.Cells.Add(headerCellSentTo);

            TableHeaderCell headerCellStatus = new TableHeaderCell();
            headerCellStatus.ID = "headerCellStatusSent";
            headerCellStatus.Text = "Status";
            headerRow.Cells.Add(headerCellStatus);

            TableHeaderCell headerCellPlanDate = new TableHeaderCell();
            headerCellPlanDate.ID = "headerCellPlanDateSent";
            headerCellPlanDate.Text = "Plan Date";
            headerRow.Cells.Add(headerCellPlanDate);

            tblProfileList.Rows.Add(headerRow);

            TableRow row = null;
            TableCell cell = null;
            ProfileDisplay profileDisplay = null;

            for (int i = 0; i < userProfiles.Count; i++)
            {
                row = new TableRow();
                cell = new TableCell();
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfileSent_" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);
                cell.Controls.Add(profileDisplay);

                TableCell statusCell = new TableCell();
                Label lblStatus = new Label();
                lblStatus.ID = "lblStatusSent_" + i;
                lblStatus.Text = sentDateRequests[i].Status;
                statusCell.Controls.Add(lblStatus);

                TableCell planDateCell = new TableCell();
                Button btnPlanDate = new Button();
                btnPlanDate.ID = "btnPlanDateSent_" + i;
                btnPlanDate.Click += BtnPlanDate_Click;
                btnPlanDate.Text = "Plan Date";
                btnPlanDate.CssClass = "btn btn-outline-success";
                if (sentDateRequests[i].Status.Equals("Accepted"))
                {
                    btnPlanDate.Enabled = true;
                }
                else
                {
                    btnPlanDate.Enabled = false;
                }
                planDateCell.Controls.Add(btnPlanDate);

                row.Cells.Add(cell);
                row.Cells.Add(statusCell);
                row.Cells.Add(planDateCell);
                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;
        }

        private void BtnPlanDate_Click(object sender, EventArgs e)
        {
            Button btnPlanDate = (Button)sender;
            int rowNum = int.Parse(btnPlanDate.ID.Split('_')[1]);

            ProfileDisplay profileDisplay;
            if (btnPlanDate.ID.Equals("btnPlanDateSent_" + rowNum))
            {
                profileDisplay = ((ProfileDisplay)sent.FindControl("pdProfileSent_" + rowNum));
                Session.Add("PlanDateUsernameTo", profileDisplay.Username);
                Session.Add("PlanDateUsernameFrom", Session["Username"].ToString());
            }
            else if(btnPlanDate.ID.Equals("btnPlanDateReceived_" + rowNum))
            {
                profileDisplay = ((ProfileDisplay)received.FindControl("pdProfileReceived_" + rowNum));
                Session.Add("PlanDateUsernameFrom", profileDisplay.Username);
                Session.Add("PlanDateUsernameTo", Session["Username"].ToString());
            }
            Response.Redirect("");
        }

        private Table generateReceivedRequestsTable(List<DateRequest> receivedDateRequests, List<ProfileDisplayClass> userProfiles)
        {
            Table tblProfileList = new Table();
            tblProfileList.HorizontalAlign = HorizontalAlign.Center;
            tblProfileList.GridLines = GridLines.Horizontal;
            tblProfileList.BorderStyle = BorderStyle.None;
            tblProfileList.CellPadding = 5;
            tblProfileList.Style.Add("width", "95%");

            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell headerCellReceivedFrom = new TableHeaderCell();
            headerCellReceivedFrom.ID = "headerCellReceivedFrom";
            headerCellReceivedFrom.Text = "Received From";
            headerRow.Cells.Add(headerCellReceivedFrom);

            TableHeaderCell headerCellStatus = new TableHeaderCell();
            headerCellStatus.ID = "headerCellStatusReceived";
            headerCellStatus.Text = "Status";
            headerRow.Cells.Add(headerCellStatus);

            TableHeaderCell headerCellChoose = new TableHeaderCell();
            headerCellChoose.ID = "headerCellChoose";
            headerCellChoose.Text = "Choose";
            headerRow.Cells.Add(headerCellChoose);

            TableHeaderCell headerCellPlanDate = new TableHeaderCell();
            headerCellPlanDate.ID = "headerCellPlanDateReceived";
            headerCellPlanDate.Text = "Plan Date";
            headerRow.Cells.Add(headerCellPlanDate);

            tblProfileList.Rows.Add(headerRow);

            TableRow row = null;
            TableCell cell = null;
            ProfileDisplay profileDisplay = null;

            for (int i = 0; i < userProfiles.Count; i++)
            {
                row = new TableRow();
                cell = new TableCell();
                profileDisplay = (ProfileDisplay)LoadControl("ProfileDisplay.ascx");
                profileDisplay.ID = "pdProfileReceived_" + i;
                profileDisplay.Username = userProfiles[i].Username;
                profileDisplay.FirstName = userProfiles[i].FirstName;
                profileDisplay.LastName = userProfiles[i].LastName;
                profileDisplay.Title = userProfiles[i].Title;
                profileDisplay.Age = userProfiles[i].Age;
                profileDisplay.ImageUrl = convertByteArrayToImage(userProfiles[i].Username);
                cell.Controls.Add(profileDisplay);

                TableCell statusCell = new TableCell();
                Label lblStatus = new Label();
                lblStatus.ID = "lblStatusReceived_" + i;
                lblStatus.Text = receivedDateRequests[i].Status;
                statusCell.Controls.Add(lblStatus);                

                TableCell chooseActionCell = new TableCell();
                DropDownList ddChooseAction = new DropDownList();
                ddChooseAction.AutoPostBack = true;
                ddChooseAction.ID = "ddChooseAction_" + i;
                ddChooseAction.CssClass = "form-control";
                ddChooseAction.SelectedIndexChanged += DdChooseAction_SelectedIndexChanged;
                ddChooseAction.Items.Add(new ListItem("", "Select"));
                ddChooseAction.Items.Add(new ListItem("Accept", "Accepted"));
                ddChooseAction.Items.Add(new ListItem("Decline", "Declined"));
                ddChooseAction.Items.Add(new ListItem("Ignore", "Ignored"));                
                ddChooseAction.SelectedIndex = 0;
                if (receivedDateRequests[i].Status.Equals("Accepted"))
                {
                    ddChooseAction.Enabled = false;
                }
                else
                {
                    ddChooseAction.Enabled = true;
                }
                chooseActionCell.Controls.Add(ddChooseAction);

                TableCell planDateCell = new TableCell();
                Button btnPlanDate = new Button();
                btnPlanDate.ID = "btnPlanDateReceived_" + i;
                btnPlanDate.Click += BtnPlanDate_Click;
                btnPlanDate.Text = "Plan Date";
                btnPlanDate.CssClass = "btn btn-outline-success";
                if (receivedDateRequests[i].Status.Equals("Accepted"))
                {
                    btnPlanDate.Enabled = true;
                }
                else
                {
                    btnPlanDate.Enabled = false;
                }
                planDateCell.Controls.Add(btnPlanDate);                

                row.Cells.Add(cell);
                row.Cells.Add(statusCell);
                row.Cells.Add(chooseActionCell);
                row.Cells.Add(planDateCell);

                tblProfileList.Rows.Add(row);
            }
            return tblProfileList;
        }

        private void DdChooseAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddChooseAction = (DropDownList)sender;

            if (ddChooseAction.SelectedIndex == 0)
            {
                lblErrorMsg.Text += "*Please select a valid response to the date request. <br />";
                lblErrorMsg.Visible = true;
            }
            else
            {
                int rowNum = int.Parse(ddChooseAction.ID.Split('_')[1]);
                ProfileDisplay profileDisplay = ((ProfileDisplay)received.FindControl("pdProfileReceived_" + rowNum));

                User tempUser = new User();
                int userIDFrom = tempUser.getUserID(profileDisplay.Username);
                int userIDTo = tempUser.getUserID(Session["Username"].ToString());

                DateRequest dateRequest = new DateRequest();
                dateRequest.UserIDFrom = userIDFrom;
                dateRequest.UserIDTo = userIDTo;
                dateRequest.Status = ddChooseAction.SelectedValue;

                JavaScriptSerializer js = new JavaScriptSerializer();
                string jsonDateRequestObj = js.Serialize(dateRequest);

                try
                {
                    string url = "https://localhost:44369/api/DatingService/Profiles/UpdateDateRequestStatus/";
                    WebRequest request = WebRequest.Create(url);
                    request.Method = "PUT";
                    request.ContentLength = jsonDateRequestObj.Length;
                    request.ContentType = "application/json";

                    //Write the JSON data to the Web Request
                    StreamWriter writer = new StreamWriter(request.GetRequestStream());
                    writer.Write(jsonDateRequestObj);
                    writer.Flush();
                    writer.Close();

                    //Read the data from the Web Response
                    WebResponse response = request.GetResponse();
                    Stream theDataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(theDataStream);
                    string data = reader.ReadToEnd();
                    reader.Close();
                    response.Close();

                    if (data == "true")
                    {
                        received.Controls.RemoveAt(received.Controls.Count - 1);
                        loadReceivedRequests();
                    }
                    else
                    {
                        lblErrorMsg.Text += "*A problem occured while updating the status of the date request. <br />";
                        lblErrorMsg.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text += "*Error: " + ex.Message + "<br />";
                    lblErrorMsg.Visible = true;
                }
            }
            
        }
    }
}