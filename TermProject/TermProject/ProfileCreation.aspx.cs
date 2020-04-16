﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DatingSiteLibrary;
using Utilities;

namespace TermProject
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty((string)Session["Username"]))
            //{
            //    Response.Redirect("Search.aspx");
            //}
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (validateRequiredFields())
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                int result = 0, imageSize;
                string fileExt, imageType, imageName, imageTitle;

                try
                {
                    if (fileProfilePic.HasFile)
                    {
                        imageSize = fileProfilePic.PostedFile.ContentLength;
                        byte[] imageData = new byte[imageSize];

                        fileProfilePic.PostedFile.InputStream.Read(imageData, 0, imageSize);
                        imageName = fileProfilePic.PostedFile.FileName;
                        imageType = fileProfilePic.PostedFile.ContentType;

                        fileExt = imageName.Substring(imageName.LastIndexOf("."));
                        fileExt = fileExt.ToLower();

                        if(fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png")
                        {
                            string kids = drpHaveKids.SelectedValue + "|" + drpWantKids.SelectedValue;
                            string commitment = drpCommitment.SelectedValue;
                            

                            UserProfile profile = new UserProfile();

                        }
                        else
                        {
                            lblErrorMsg.Text += "*Only jpg, jpeg, and png file types supported. <br />";
                            lblErrorMsg.Visible = true;
                        }
                    }
                }
                catch(Exception ex)
                {
                    lblErrorMsg.Text += "Error occurred: [" + ex.Message + "] cmd=" + result;
                    lblErrorMsg.Visible = true;
                }
            }
        }

        private bool validateRequiredFields()
        {
            bool valid = true;
            Int64 i;
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || (txtPhone.Text.Length != 10) || !Int64.TryParse(txtPhone.Text, out i))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid phone number. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtOccupation.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter your occupation. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter your age. <br />";
                lblErrorMsg.Visible = true;
            }
            int heightFt, heightIn;
            if(string.IsNullOrWhiteSpace(txtHeightFeet.Text) || string.IsNullOrWhiteSpace(txtHeightIn.Text) || int.TryParse(txtHeightFeet.Text, out heightFt) || 
               int.TryParse(txtHeightIn.Text, out heightIn))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid height. <br />";
                lblErrorMsg.Visible = true;
            }
            int weight;
            if(string.IsNullOrWhiteSpace(txtWeight.Text) || int.TryParse(txtWeight.Text, out weight))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid weight. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a title. <br />";
                lblErrorMsg.Visible = true;
            }
            if(drpCommitment.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select your commitment preference. <br />";
                lblErrorMsg.Visible = true;
            }
            if((drpHaveKids.SelectedIndex == 0) || (drpWantKids.SelectedIndex == 0))
            {
                valid = false;
                lblErrorMsg.Text += "*Please select your kids preference. <br />";
            }
            if (string.IsNullOrWhiteSpace(txtInterests.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter your interests. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a description. <br />";
                lblErrorMsg.Visible = true;
            }

            return valid;
        }
    }
}