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

namespace TermProject
{
    public partial class VerifyAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["VerifyAccount"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                User tempUser = new User();
                HttpCookie cookie = Request.Cookies["LoginCookie"];
                if (tempUser.verifyAccount(cookie.Values["Username"].ToString()))
                {
                    lblVerify.Text = "Your account has been verified.";
                }
                else
                {
                    lblVerify.Text = "There was an error verifying your account";
                }
            }
        }
    }
}