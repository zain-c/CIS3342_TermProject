using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DatingSiteLibrary;
using Utilities;

namespace TermProject
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Form.DefaultButton = btnLogin.UniqueID;

           

            txtLoginUsername.Focus();

            //Check if Remember Me cookie has saved credentials
            if(!IsPostBack && Request.Cookies["LoginCookie"] != null)
            {
                HttpCookie cookie = Request.Cookies["LoginCookie"];
                txtLoginUsername.Text = cookie.Values["Username"].ToString();
                txtLoginPwd.Text = cookie.Values["Password"].ToString();
                txtLoginPwd.Attributes["type"] = "password";
                chkRememberInfo.Checked = true;
            }
            else
            {
                txtLoginPwd.Attributes["type"] = "password";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            lblErrorMsg.Visible = false;

            if (validateLogin())  //if login valid
            {
                if (chkRememberInfo.Checked) //create http cookie for login credentials if Remember Me is checked
                {
                    HttpCookie myCookie = new HttpCookie("LoginCookie");
                    myCookie.Values["Username"] = txtLoginUsername.Text;
                    myCookie.Values["Password"] = txtLoginPwd.Text;
                    myCookie.Expires = new DateTime(2025, 1, 1);
                    Response.Cookies.Add(myCookie);
                }
                else
                {
                    if(Request.Cookies["LoginCookie"] != null)
                    {
                        Response.Cookies["LoginCookie"].Expires = DateTime.Now.AddDays(-1);
                    }
                }
                
                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_LoginCheck";

                objCmd.Parameters.AddWithValue("@username", txtLoginUsername.Text);
                objCmd.Parameters.AddWithValue("@password", txtLoginPwd.Text);

                DataSet loginDS = objDB.GetDataSetUsingCmdObj(objCmd);

                if(loginDS.Tables[0].Rows.Count == 0)
                {
                    lblErrorMsg.Text = "*Invalid username or password";
                    lblErrorMsg.Visible = true;
                    txtLoginUsername.Focus();
                }
                else
                {
                    Session.Add("Username", txtLoginUsername.Text);
                    Session.Add("RequestedProfile", Session["Username"].ToString());
                    //Successful login attempt
                    Response.Redirect("Profile.aspx"); //redirect to search page?
                }
                
            }
            
        }

        private bool validateLogin()
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtLoginUsername.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a username. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtLoginPwd.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a password.";
                lblErrorMsg.Visible = true;
            }
            return valid;
        }
    }
}