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
    public partial class LoginRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtRecoveryEmail.Focus();
        }

        protected void btnRecover_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            lblErrorMsg.Visible = false;

            if (validateRecoveryFields()) //if valid entries
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "TP_AccountRecovery";

                User tempUser = new User();
                int userID = tempUser.getUserIDByEmail(txtRecoveryEmail.Text);

                objCmd.Parameters.AddWithValue("@userID", userID);
                objCmd.Parameters.AddWithValue("@sq1", txtSQ1.Text);
                objCmd.Parameters.AddWithValue("@sq2", txtSQ2.Text);
                objCmd.Parameters.AddWithValue("@sq3", txtSQ3.Text);

                DataSet recoveryDS = objDB.GetDataSetUsingCmdObj(objCmd);
                if(recoveryDS.Tables.Count == 0)
                {
                    lblErrorMsg.Text += "We cannot verify that the account is yours. Please make sure your email and answers are correct.";
                    lblErrorMsg.Visible = true;
                }
                else
                {
                    recoveryDiv.Visible = false;
                    successDiv.Visible = true;
                    lblUsername.Text += recoveryDS.Tables[0].Rows[0]["Username"].ToString();
                    lblPassword.Text += recoveryDS.Tables[0].Rows[0]["Password"].ToString();
                }
            }
        }

        private bool validateRecoveryFields()
        {
            bool valid = true;

            //validate email
            if (string.IsNullOrWhiteSpace(txtRecoveryEmail.Text))
            {
                valid = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text += "*Please enter an email address. <br />";
            }
            else
            {
                if (!txtRecoveryEmail.Text.Contains("@"))
                {
                    valid = false;
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text += "*Please enter a valid email. <br />";
                }
                else
                {
                    if (!txtRecoveryEmail.Text.Split('@')[1].Contains("."))
                    {
                        valid = false;
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text += "*Please enter a valid email. <br />";
                    }
                }
            }

            //validate security questions
            if (string.IsNullOrWhiteSpace(txtSQ1.Text) || string.IsNullOrWhiteSpace(txtSQ2.Text) || string.IsNullOrWhiteSpace(txtSQ3.Text))
            {
                valid = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text += "*Please answer all security questions. <br />";
            }

            return valid;
        }
    }
}