using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Access : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchOnLogin_Click(Object sender,EventArgs e)
        {
            Response.Redirect("Search.aspx");
        }

        protected void btnRegister_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
    }
}