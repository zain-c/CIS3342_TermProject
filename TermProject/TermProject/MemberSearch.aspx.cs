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
        /* FOR TESTING */
        //ArrayList testSearchResults = new ArrayList();
        //MemberSearchResults testAcct1 = new MemberSearchResults();




        protected void Page_Load(object sender, EventArgs e)
        {
            ResultsContainer.Visible = false;
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
                    displayedSearchResults = loadResults(txtLocationFilter.Text, ddStateFilter.SelectedValue, ddGenderFilter.SelectedValue, ddCommitmentFilter.SelectedValue, ddHaveKidsFilter.SelectedValue, ddWantKidsFilter.SelectedValue, blankOccupation);
                }
            }
            else if (validateFields())
            {
                displayedSearchResults = loadResults(txtLocationFilter.Text, ddStateFilter.SelectedValue, ddGenderFilter.SelectedValue, ddCommitmentFilter.SelectedValue, ddHaveKidsFilter.SelectedValue, ddWantKidsFilter.SelectedValue, txtOccupationFilter.Text);
            }

            ShowResults(displayedSearchResults);

            /* FOR TESTING */
            /*
            testAcct1.City = "Havertown";
            testAcct1.Commitment = "Casual";
            testAcct1.FirstName = "Alex";
            testAcct1.Gender = "Male";
            testAcct1.HaveKids = "No";
            testAcct1.LastName = "Derbs";
            testAcct1.Occupation = "Student";
            testAcct1.State = "PA";
            testAcct1.Title = "Sup";
            testAcct1.Username = "aderbs7";
            testAcct1.WantKids = "No";
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

        private ArrayList loadResults(string city, string state, string gender, string commitment, string haveKids, string wantKids, string occupation)
        {
            string url = "https://localhost:44369/api/DatingService/Search/LoadSearchResults/Member" + city + "/" + state + "/" + gender + "/" + commitment + "/" + haveKids + "/" + wantKids + "/" + occupation;

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            Stream theDataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(theDataStream);
            string data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            JavaScriptSerializer js = new JavaScriptSerializer();
            MemberSearchResults[] profileResults = js.Deserialize<MemberSearchResults[]>(data);

            ArrayList filteredProfileResults = new ArrayList();

            User currentUser = new User();
            int currentUserID = currentUser.getUserID(Session["Username"].ToString());

            PassedList currentUserPassedList = new PassedList();
            currentUserPassedList = currentUserPassedList.getPasses(currentUserID);

            foreach (MemberSearchResults result in profileResults)
            {
                User userResult = new User();
                int resultUserID = userResult.getUserID(Session["Username"].ToString());
                
                if (!currentUserPassedList.List.Contains(resultUserID.ToString()) && (resultUserID != currentUserID))
                {
                    filteredProfileResults.Add(result);
                }
            }

            return filteredProfileResults;

           

        }

        private void ShowResults(ArrayList searchResults)

        {

            ResultsContainer.Visible = true;
            rptSearchResults.DataSource = searchResults;

            rptSearchResults.DataBind();

        }


    }
}