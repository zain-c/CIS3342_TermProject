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
    public partial class NonMemberSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateFields())
            {
                loadResults(txtLocationFilter.Text, ddStateFilter.SelectedValue, ddGenderFilter.SelectedValue);
            }


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

            return valid;
        }

        private void loadResults(string city, string state, string gender)
        {
            string url = "https://localhost:44369/api/DatingService/Search/LoadSearchResults/Nonmember" + city + "/" + state + "/" + gender;

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            SearchResult[] profileResults = js.Deserialize<SearchResult[]>(data);

            List<SearchResult> filteredProfileResults = new List<SearchResult>();

            User currentUser = new User();
            int currentUserID = currentUser.getUserID(Session["Username"].ToString());

            PassedList currentUserPassedList = new PassedList();
            currentUserPassedList = currentUserPassedList.getPasses(currentUserID);

            foreach (SearchResult result in profileResults)
            {
                User userResult = new User();
                int resultUserID = userResult.getUserID(Session["Username"].ToString());
                if (!currentUserPassedList.List.Contains(resultUserID.ToString()))
                {
                    filteredProfileResults.Add(result);
                }
            }
            
        }
    }
}