using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using DatingSiteLibrary;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace TermProject
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
            lblErrorMessage.Visible = false;

            validateLogin();
            validateEmail();
            validateName();
            validateAddress(txtAddress.Text, txtCity.Text, ddState.SelectedValue, txtZip.Text);
            validateBillingAddress();
            validateSecurityQuestions();

            if (lblErrorMessage.Visible == false)
            {
                Response.Write("LOGIN VALID");
                User newUser = new User();
                try
                {
                    newUser.addUserToDB(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtFirstName.Text, txtLastName.Text);
                    int userID = newUser.getUserID(txtUsername.Text);
                    newUser.addAddressToDB(userID, txtAddress.Text, txtCity.Text, ddState.SelectedValue, int.Parse(txtZip.Text), txtBillingAddress.Text,
                                           txtBillingCity.Text, ddBillingState.SelectedValue, int.Parse(txtBillingZip.Text));
                    newUser.addSecurityQuestionsToDB(txtSecurityQuestion1.Text, txtSecurityQuestion2.Text, txtSecurityQuestion3.Text, userID);

                    Session.Add("Username", txtUsername.Text);
                }
                catch
                {

                }
            }
        }

        private void sendVerificationEmail(string recipient)
        {
            MailMessage objMail = new MailMessage();
            MailAddress toAddress = new MailAddress(recipient);
            MailAddress fromAddress = new MailAddress("tug85523@temple.edu");
            string subject = "Verify your account";
            string body = "Click the link below to verify your account <br />";

        }

        private int getUserID(string username)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetUserID";

            objCmd.Parameters.AddWithValue("@username", username);

            SqlParameter outputUserID = new SqlParameter("@userID", 0);
            outputUserID.Direction = ParameterDirection.Output;
            outputUserID.SqlDbType = SqlDbType.Int;
            objCmd.Parameters.Add(outputUserID);

            objDB.GetDataSetUsingCmdObj(objCmd);
            int userID = int.Parse(objCmd.Parameters["@userID"].Value.ToString());

            return userID;
        }

        private void validateLogin()
        {
            //bool valid = true;
            DBConnect objDb = new DBConnect();
            SqlCommand objCmd = new SqlCommand();

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_ValidateUsername";

            objCmd.Parameters.AddWithValue("@username", txtUsername.Text);
            DataTable usernameTbl = objDb.GetDataSetUsingCmdObj(objCmd).Tables[0];

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                //valid = false;
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please enter a username. <br />";
            }
            else
            {
                if (usernameTbl.Rows.Count != 0)
                {
                    //valid = false;
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text += "*Username already exists. <br />";
                }
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                //valid = false;
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please enter a password. <br />";
            }
        }

        private void validateName()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please enter first and last name. <br />";
            }
        }

        private void validateEmail()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please enter an email address. <br />";
            }
            else
            {
                if (!txtEmail.Text.Contains("@"))
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text += "*Please enter a valid email. <br />";
                }
                else
                {
                    if (!txtEmail.Text.Split('@')[1].Contains("."))
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text += "*Please enter a valid email. <br />";
                    }
                    else
                    {
                        DBConnect objDb = new DBConnect();
                        SqlCommand objCmd = new SqlCommand();

                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandText = "TP_ValidateEmail";

                        objCmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        DataTable emailTbl = objDb.GetDataSetUsingCmdObj(objCmd).Tables[0];
                        if (emailTbl.Rows.Count != 0)
                        {
                            lblErrorMessage.Visible = true;
                            lblErrorMessage.Text += "*An account with this email already exists. <br />";
                        }
                    }
                }
            }
        }

        private void validateAddress(string address, string city, string state, string zip)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please enter an address. <br />";
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please enter a city. <br />";
            }

            if (state == "Select")
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please select a state. <br />";
            }

            if (string.IsNullOrWhiteSpace(zip))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please enter a zip code. <br />";
            }
            else
            {
                if (zip.Length != 5)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text += "*Please enter a valid zip code. <br />";
                }
                else
                {
                    if (!int.TryParse(zip, out int zipCode))
                    {
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text += "*Please enter a valid zip code. <br />";
                    }
                }
            }
        }

        private void validateBillingAddress()
        {
            if (chkBillingAddress.Checked)
            {
                txtBillingAddress.Text = txtAddress.Text;
                txtBillingCity.Text = txtCity.Text;
                ddBillingState.SelectedIndex = ddState.SelectedIndex;
                txtBillingZip.Text = txtZip.Text;
            }
            else
            {
                validateAddress(txtBillingAddress.Text, txtBillingCity.Text, ddBillingState.SelectedValue, txtBillingZip.Text);
            }
        }

        private void validateSecurityQuestions()
        {
            if (string.IsNullOrWhiteSpace(txtSecurityQuestion1.Text) || string.IsNullOrWhiteSpace(txtSecurityQuestion2.Text) || string.IsNullOrWhiteSpace(txtSecurityQuestion3.Text))
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text += "*Please answer all security questions. <br />";
            }
        }
    }
}