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
                                       string _commitment, string _kids, string _interests, string _description, int _userID)
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
            objCmd.Parameters.AddWithValue("@kids", _kids);
            objCmd.Parameters.AddWithValue("@title", _title);
            objCmd.Parameters.AddWithValue("@userID", _userID);

            int result = objDB.DoUpdateUsingCmdObj(objCmd);
            return result;
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
            get { return height;}
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
            get { return commitment;}
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
