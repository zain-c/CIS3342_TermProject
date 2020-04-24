using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class NonMemberSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            


        }

        protected void validateFields()
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
        }
    }
}