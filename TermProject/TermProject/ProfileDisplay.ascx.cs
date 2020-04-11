using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TermProject
{
    public partial class ProfileDisplay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [Category("Misc")]
        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        [Category("Misc")]
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

    }
}