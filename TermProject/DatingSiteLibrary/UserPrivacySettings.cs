using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace DatingSiteLibrary
{
    public class UserPrivacySettings
    {
        string profilePic;
        string firstName;
        string lastName;
        string title;        
        string age;
        string height;
        string weight;
        string occupation;
        string commitment;
        string haveKids;
        string wantKids;
        string interests;
        string description;
        string gender;

        public UserPrivacySettings()
        {

        }

        public int addUserSettingsToDB(string _profilePic, string _firstName, string _lastName, string _title, string _age, string _height, string _weight,
                                       string _occupation, string _commitment, string _haveKids, string _wantKids, string _interests, string _description, string gender,
                                       int userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_AddUserPrivacySettings";

            objCmd.Parameters.AddWithValue("@profilePic", _profilePic);
            objCmd.Parameters.AddWithValue("@firstName", _firstName);
            objCmd.Parameters.AddWithValue("@lastName", _lastName);
            objCmd.Parameters.AddWithValue("@title", _title);
            objCmd.Parameters.AddWithValue("@age", _age);
            objCmd.Parameters.AddWithValue("@height", _height);
            objCmd.Parameters.AddWithValue("@weight", _weight);
            objCmd.Parameters.AddWithValue("@occupation", _occupation);
            objCmd.Parameters.AddWithValue("@commitment", _commitment);
            objCmd.Parameters.AddWithValue("@haveKids", _haveKids);
            objCmd.Parameters.AddWithValue("@wantKids", _wantKids);
            objCmd.Parameters.AddWithValue("@interests", _interests);
            objCmd.Parameters.AddWithValue("@description", _description);
            objCmd.Parameters.AddWithValue("@gender", gender);
            objCmd.Parameters.AddWithValue("@userID", userID);

            int result = objDB.DoUpdateUsingCmdObj(objCmd);
            return result;
        }

        public UserPrivacySettings retrievePrivacySettings(int userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetPrivacySettings";
            objCmd.Parameters.AddWithValue("@userID", userID);

            DataSet privacyDS = objDB.GetDataSetUsingCmdObj(objCmd);
            UserPrivacySettings privacySettings = new UserPrivacySettings();
            if(privacyDS.Tables[0].Rows.Count == 1)
            {
                DataTable privacyDT = privacyDS.Tables[0];
                privacySettings.ProfilePic = privacyDT.Rows[0]["ProfilePic"].ToString();
                privacySettings.FirstName = privacyDT.Rows[0]["FirstName"].ToString();
                privacySettings.LastName = privacyDT.Rows[0]["LastName"].ToString();
                privacySettings.Title = privacyDT.Rows[0]["Title"].ToString();
                privacySettings.Age = privacyDT.Rows[0]["Age"].ToString();
                privacySettings.Height = privacyDT.Rows[0]["Height"].ToString();
                privacySettings.Weight = privacyDT.Rows[0]["Weight"].ToString();
                privacySettings.Occupation = privacyDT.Rows[0]["Occupation"].ToString();
                privacySettings.Commitment = privacyDT.Rows[0]["Commitment"].ToString();
                privacySettings.HaveKids = privacyDT.Rows[0]["HaveKids"].ToString();
                privacySettings.WantKids = privacyDT.Rows[0]["WantKids"].ToString();
                privacySettings.Interests = privacyDT.Rows[0]["Interests"].ToString();
                privacySettings.Description = privacyDT.Rows[0]["Description"].ToString();
                privacySettings.Gender = privacyDT.Rows[0]["Gender"].ToString();
            }
            return privacySettings;
        }

        public string ProfilePic
        {
            get { return profilePic; }
            set { profilePic = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Height
        {
            get { return height; }
            set { height = value; }
        }

        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        public string Commitment
        {
            get { return commitment; }
            set { commitment = value; }
        }

        public string HaveKids
        {
            get { return haveKids; }
            set { haveKids = value; }
        }

        public string WantKids
        {
            get { return wantKids; }
            set { wantKids = value; }
        }

        public string Interests
        {
            get { return interests; }
            set { interests = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
    }
}
