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
    public class UserProfile
    {
        string firstName;
        string lastName;
        string address;
        string city;
        string state;
        int zip;
        string email;
        string phoneNumber;
        //byte[] imageData;
        //string imageDataAsString;
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
        string gender;

        public UserProfile()
        {

        }

        public int addUserProfileToDB(string _phoneNumber, byte[] _imageData, string _occupation, int _age, string _height, int _weight, string _title,
                                       string _commitment, string _haveKids, string _wantKids, string _interests, string _description, int _userID, string gender)
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
            objCmd.Parameters.AddWithValue("@gender", gender);

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
                profile.Email = profileDT.Rows[0]["Email"].ToString();
                profile.PhoneNumber = profileDT.Rows[0]["Phone"].ToString();
                profile.Occupation = profileDT.Rows[0]["Occupation"].ToString();
                profile.Age = int.Parse(profileDT.Rows[0]["Age"].ToString());
                profile.Height = profileDT.Rows[0]["Height"].ToString();
                profile.Weight = int.Parse(profileDT.Rows[0]["Weight"].ToString());
                //profile.ImageData = (byte[])profileDT.Rows[0]["Photo"].ToString();
                profile.Interests = profileDT.Rows[0]["Interests"].ToString();
                profile.Description = profileDT.Rows[0]["Description"].ToString();
                profile.Commitment = profileDT.Rows[0]["Commitment"].ToString();
                profile.HaveKids = profileDT.Rows[0]["haveKids"].ToString();
                profile.WantKids = profileDT.Rows[0]["wantKids"].ToString();
                profile.Title = profileDT.Rows[0]["Title"].ToString();
                profile.Gender = profileDT.Rows[0]["Gender"].ToString();

            }
            return profile;
        }

        public bool modifyUserProfile(UserProfile profile, int userID)
        {
            if (profile != null)
            {
                int result = 0;
                DBConnect objDB = new DBConnect();

                SqlCommand profileCmd = new SqlCommand();
                profileCmd.CommandType = CommandType.StoredProcedure;
                profileCmd.CommandText = "TP_ModifyProfile";
                profileCmd.Parameters.AddWithValue("@userID", userID);
                profileCmd.Parameters.AddWithValue("@phone", profile.PhoneNumber);
                profileCmd.Parameters.AddWithValue("@occupation", profile.Occupation);
                profileCmd.Parameters.AddWithValue("@age", profile.Age);
                profileCmd.Parameters.AddWithValue("@height", profile.Height);
                profileCmd.Parameters.AddWithValue("@weight", profile.Weight);
                profileCmd.Parameters.AddWithValue("@interests", profile.Interests);
                profileCmd.Parameters.AddWithValue("@description", profile.Description);
                profileCmd.Parameters.AddWithValue("@commitment", profile.Commitment);
                profileCmd.Parameters.AddWithValue("@wantKids", profile.WantKids);
                profileCmd.Parameters.AddWithValue("@haveKids", profile.HaveKids);
                profileCmd.Parameters.AddWithValue("@title", profile.Title);
                profileCmd.Parameters.AddWithValue("@gender", profile.Gender);
                result += objDB.DoUpdateUsingCmdObj(profileCmd);

                SqlCommand userCmd = new SqlCommand();
                userCmd.CommandType = CommandType.StoredProcedure;
                userCmd.CommandText = "TP_ModifyUser";
                userCmd.Parameters.AddWithValue("@userID", userID);
                userCmd.Parameters.AddWithValue("@firstName", profile.FirstName);
                userCmd.Parameters.AddWithValue("@lastName", profile.LastName);
                userCmd.Parameters.AddWithValue("@email", profile.Email);
                result += objDB.DoUpdateUsingCmdObj(userCmd);

                SqlCommand addressCmd = new SqlCommand();
                addressCmd.CommandType = CommandType.StoredProcedure;
                addressCmd.CommandText = "TP_ModifyAddress";
                addressCmd.Parameters.AddWithValue("@userID", userID); 
                addressCmd.Parameters.AddWithValue("@address", profile.Address);
                addressCmd.Parameters.AddWithValue("@city", profile.City);
                addressCmd.Parameters.AddWithValue("@state", profile.State);
                addressCmd.Parameters.AddWithValue("@zip", profile.Zip);
                result += objDB.DoUpdateUsingCmdObj(addressCmd);

                if (result == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
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

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        //public byte[] ImageData
        //{
        //    get { return imageData; }
        //    set { imageData = value; }
        //}

        //public string ImageDataAsString
        //{
        //    get { return imageDataAsString; }
        //    set { imageDataAsString = value; }
        //}

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

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
    }
}
