using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Utilities;

namespace TermProject
{
    public partial class ProfileDisplay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public string Age
        {
            get { return lblAge.Text; }
            set { lblAge.Text = value; }
        }

        public Image ProfilePic
        {
            get { return imgProfilePic; }
            set { imgProfilePic = value; }
        }

        protected void btnViewProfile_Click(object sender, EventArgs e)
        {

        }
    }
}