using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace TermProject
{
    public partial class ProfileDisplay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnViewProfile.Click += new EventHandler(btnViewProfile_Click);
        }

        public string Username
        {
            get { return lblUsername.Text; }
            set { lblUsername.Text = value; }
        }

        public string FirstName
        {
            get { return lblFirstName.Text; }
            set { lblFirstName.Text = value + " "; }
        }

        public string LastName
        {
            get { return lblLastName.Text; }
            set { lblLastName.Text = value; }
        }

        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public int Age
        {
            get { return int.Parse(lblAge.Text); }
            set { lblAge.Text += value; }
        }

        public string ImageUrl
        {
            get { return imgProfilePic.ImageUrl; }
            set { imgProfilePic.ImageUrl = value; }
        }

        protected void btnViewProfile_Click(object sender, EventArgs e)
        {
            Session["RequestedProfile"] = lblUsername.Text;
            Page.Response.Redirect("Profile.aspx", true);
        }
    }
}