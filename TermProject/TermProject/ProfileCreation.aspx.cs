using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void validatePhone()
        {
            int phoneInt;
            try
            {
                phoneInt = Convert.ToInt32(txtPhone.Text);
            }
            catch (Exception e)
            {
                txtPhone.Text = "";
                lblErrorMessage.Text = "Phone Number must only include digits.";
                lblErrorMessage.Visible = true;
            }

            if (txtPhone.Text.Length != 10 && txtPhone.Text.Length != 0)
            {
                txtPhone.Text = "";
                lblErrorMessage.Text = "Phone Number must be 10 digits or blank.";
                lblErrorMessage.Visible = true;
            }

            
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
            lblErrorMessage.Visible = false;

            validatePhone();
        }
    }
}