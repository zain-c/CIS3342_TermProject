using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProject
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Session["RequestedProfile"] = Session["Username"].ToString();
            Response.Redirect("Profile.aspx");
        }

        protected void btnLikedPassList_Click(object sender, EventArgs e)
        {
            Response.Redirect("LikedPassList.aspx");
        }

        protected void btnSearchOnMainPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberSearch.aspx");
        }

        protected void btnDateRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("DateRequests.aspx");
        }

        protected void btnMessages_Click(object sender, EventArgs e)
        {
            Response.Redirect("Messaging.aspx");
        }
    }
}