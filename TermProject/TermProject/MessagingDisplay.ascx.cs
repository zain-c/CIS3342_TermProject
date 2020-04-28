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
    public partial class MessagingDisplay : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnSendMessage_Click(object sender, EventArgs e)
        {            
            Timer1.Enabled = true;

            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetConversation";
            objCmd.Parameters.AddWithValue("@usernameOne", Session["Username"].ToString());
            objCmd.Parameters.AddWithValue("@usernameTwo", Session["MessageToUsername"].ToString());
            DataSet conversationDS = objDB.GetDataSetUsingCmdObj(objCmd);

            string conversation = objDB.GetField("Content", 0).ToString();
            int messageID = int.Parse(objDB.GetField("MessageID", 0).ToString());

            String messageText = txtSendMessage.Text;
            
            String updateConversation = conversation + "<br />" + Session["Username"].ToString() + ": " + messageText;
            
            DBConnect objDBConn = new DBConnect();
            SqlCommand objCmdConn = new SqlCommand();
            objCmdConn.CommandType = CommandType.StoredProcedure;
            objCmdConn.CommandText = "TP_UpdateConversation";
            objCmdConn.Parameters.AddWithValue("@messageID", messageID);
            objCmdConn.Parameters.AddWithValue("@content", updateConversation);
            int result = objDBConn.DoUpdateUsingCmdObj(objCmdConn);
            if(result == 1)
            {
                DataBind();

                User tempUser = new User();
                string recipient = tempUser.getEmailByUsername(Session["MessageToUsername"].ToString());
                Email emailObj = new Email();
                string to = recipient;
                string from = "atozdatingsite@gmail.com";
                string subject = "New Message";
                string message = "You have a new message from " + Session["Username"].ToString() + ". Visit the website to view the message.";
                try
                {
                    emailObj.SendMail(to, from, subject, message);
                }
                catch (Exception ex)
                {

                }
            }
            txtSendMessage.Text = string.Empty;
        }

        public string To
        {
            get { return lblTo.Text.Split(':')[1].Trim(); }
            set { lblTo.Text += value; }
        }       
        

        public string SendMessageText
        {
            get { return txtSendMessage.Text; }
            set { txtSendMessage.Text = value; }
        }        

        public override void DataBind()
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetConversation";
            objCmd.Parameters.AddWithValue("@usernameOne", Session["Username"].ToString());
            objCmd.Parameters.AddWithValue("@usernameTwo", Session["MessageToUsername"].ToString());
            DataSet conversationDS = objDB.GetDataSetUsingCmdObj(objCmd);

            string conversation = objDB.GetField("Content", 0).ToString();
            lblMessages.Text = conversation;
            
        }        

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            DataBind();
            txtSendMessage.Focus();
        }

        protected void txtSendMessage_TextChanged(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
        }
    }
}