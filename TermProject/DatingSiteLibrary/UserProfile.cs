using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace DatingSiteLibrary
{
    public class UserProfile
    {
        string firstName;
        string lastName;
        string address;
        string city;
        string state;
        int zip;
        string phoneNumber;
        byte[] imageData;
        string occupation;
        int age;
        string height;
        int weight;
        string title;
        string commitment;
        string haveKids;
        string wantKids;
        string interests;
        string description;

        public UserProfile()
        {

        }

        public int addUserProfileToDB(string _phoneNumber, byte[] _imageData, string _occupation, int _age, string _height, int _weight, string _title,
                                       string _commitment, string _haveKids, string _wantKids, string _interests, string _description, int _userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_AddUserProfile";

            objCmd.Parameters.AddWithValue("@phone", _phoneNumber);
            objCmd.Parameters.AddWithValue("@occupation", _occupation);
            objCmd.Parameters.AddWithValue("@age", _age);
            objCmd.Parameters.AddWithValue("@height", _height);
            objCmd.Parameters.AddWithValue("@weight", _weight);
            objCmd.Parameters.AddWithValue("@photo", _imageData);
            objCmd.Parameters.AddWithValue("@interests", _interests);
            objCmd.Parameters.AddWithValue("@description", _description);
            objCmd.Parameters.AddWithValue("@commitment", _commitment);
            objCmd.Parameters.AddWithValue("@wantKids", _wantKids);
            objCmd.Parameters.AddWithValue("@haveKids", _haveKids);
            objCmd.Parameters.AddWithValue("@title", _title);
            objCmd.Parameters.AddWithValue("@userID", _userID);

            int result = objDB.DoUpdateUsingCmdObj(objCmd);
            return result;
        }

        public UserProfile retreiveUserProfileFromDB(int userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_LoadProfile";
            objCmd.Parameters.AddWithValue("@userID", userID);

            DataSet profileDS = objDB.GetDataSetUsingCmdObj(objCmd);
            UserProfile profile = new UserProfile();
            if (profileDS.Tables[0].Rows.Count == 1)
            {
                DataTable profileDT = profileDS.Tables[0];
                profile.FirstName = profileDT.Rows[0]["FirstName"].ToString();
                profile.LastName = profileDT.Rows[0]["LastName"].ToString();
                profile.Address = profileDT.Rows[0]["HomeAddress"].ToString();
                profile.City = profileDT.Rows[0]["HomeCity"].ToString();
                profile.State = profileDT.Rows[0]["HomeState"].ToString();
                profile.Zip = int.Parse(profileDT.Rows[0]["HomeZip"].ToString());
                profile.PhoneNumber = profileDT.Rows[0]["Phone"].ToString();
                profile.Occupation = profileDT.Rows[0]["Occupation"].ToString();
                profile.Age = int.Parse(profileDT.Rows[0]["Age"].ToString());
                profile.Height = profileDT.Rows[0]["Height"].ToString();
                profile.Weight = int.Parse(profileDT.Rows[0]["Weight"].ToString());
                profile.ImageData = (byte[])profileDT.Rows[0]["Photo"];
                profile.Interests = profileDT.Rows[0]["Interests"].ToString();
                profile.Description = profileDT.Rows[0]["Description"].ToString();
                profile.Commitment = profileDT.Rows[0]["Commitment"].ToString();
                profile.HaveKids = profileDT.Rows[0]["wantKids"].ToString();
                profile.WantKids = profileDT.Rows[0]["haveKids"].ToString();
                profile.Title = profileDT.Rows[0]["Title"].ToString();

            }
            return profile;
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


        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public int Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public byte[] ImageData
        {
            get { return imageData; }
            set { imageData = value; }
        }

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
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
    }
}
