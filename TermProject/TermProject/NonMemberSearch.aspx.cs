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
    public partial class NonMemberSearch : System.Web.UI.Page
    {
        /* FOR TESTING */
        //ArrayList testSearchResults = new ArrayList();
        //SearchResult testAcct1 = new SearchResult();

        protected void Page_Load(object sender, EventArgs e)
        {
            ResultsContainer.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ArrayList displayedSearchResults = new ArrayList();
            lblErrorMsg.Text = "";
            if (validateFields())
            {
                displayedSearchResults = loadResults(txtLocationFilter.Text, ddStateFilter.SelectedValue, ddGenderFilter.SelectedValue);
            }

            ShowResults(displayedSearchResults);
            

            /* FOR TESTING */
            /*
            testAcct1.City = "Havertown";          
            testAcct1.FirstName = "Alex";
            testAcct1.LastName = "Derbs";
            testAcct1.Gender = "Male";            
            testAcct1.State = "PA";            
            testAcct1.Username = "aderbs7";            
            testSearchResults.Add(testAcct1);
            ShowResults(testSearchResults);
            */

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

        private ArrayList loadResults(string city, string state, string gender)
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

            ArrayList profileResultsList = new ArrayList();

            foreach (SearchResult result in profileResults)
            {
               
                profileResultsList.Add(result);
                
            }

            return profileResultsList;
        }

        private void ShowResults(ArrayList searchResults)

        {

            ResultsContainer.Visible = true;
            rptSearchResults.DataSource = searchResults;

            rptSearchResults.DataBind();

        }
    }
}