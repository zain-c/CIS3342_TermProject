using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using System.Data;
using System.Data.SqlClient;

namespace DatingSiteLibrary
{
    public class User
    {
        string username;
        string email;
        string firstName;
        string lastName;
        string homeAddress;
        string homeCity;
        string homeState;
        int homeZip;
        string billingAddress;
        string billingCity;
        string billingState;
        int billingZip;
        string securityQuestion1;
        string securityQuestion2;
        string securityQuestion3;

        public User()
        {

        }

        public void addUserToDB(string _username, string _password, string _email, string _firstName, string _lastName)
        {
            DBConnect objDb = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_CreateUser";

            objCmd.Parameters.AddWithValue("@username", _username);
            objCmd.Parameters.AddWithValue("@password", _password);
            objCmd.Parameters.AddWithValue("@email", _email);
            objCmd.Parameters.AddWithValue("@firstName", _firstName);
            objCmd.Parameters.AddWithValue("@lastName", _lastName);

            objDb.DoUpdateUsingCmdObj(objCmd);
        }

        public void addAddressToDB(int userID, string _homeAddress, string _homeCity, string _homeState, int _homeZip, string _billingAddress, string _billingCity,
                                   string _billingState, int _billingZip)
        {
            DBConnect objDb = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_AddAddresses";

            objCmd.Parameters.AddWithValue("@homeAddress", _homeAddress);
            objCmd.Parameters.AddWithValue("@homeCity", _homeCity);
            objCmd.Parameters.AddWithValue("@homeState", _homeState);
            objCmd.Parameters.AddWithValue("@homeZip", _homeZip);
            objCmd.Parameters.AddWithValue("@billingAddress", _billingAddress);
            objCmd.Parameters.AddWithValue("@billingCity", _billingCity);
            objCmd.Parameters.AddWithValue("@billingState", _billingState);
            objCmd.Parameters.AddWithValue("@billingZip", _billingZip);
            objCmd.Parameters.AddWithValue("@userID", userID);

            objDb.DoUpdateUsingCmdObj(objCmd);
        }

        public void addSecurityQuestionsToDB(string sq1, string sq2, string sq3, int userID)
        {
            DBConnect objDb = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_AddSecurityQuestions";

            objCmd.Parameters.AddWithValue("@sq1", sq1);
            objCmd.Parameters.AddWithValue("@sq2", sq2);
            objCmd.Parameters.AddWithValue("@sq3", sq3);
            objCmd.Parameters.AddWithValue("@userID", userID);

            objDb.DoUpdateUsingCmdObj(objCmd);
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
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
        public string HomeAddress
        {
            get { return homeAddress; }
            set { homeAddress = value; }
        }
        public string HomeCity
        {
            get { return homeCity; }
            set { homeCity = value; }
        }
        public string HomeState
        {
            get { return homeState; }
            set { homeState = value; }
        }
        public int HomeZip
        {
            get { return homeZip; }
            set { homeZip = value; }
        }
        public string BillingAddress
        {
            get { return billingAddress; }
            set { billingAddress = value; }
        }
        public string BillingCity
        {
            get { return billingCity; }
            set { billingCity = value; }
        }
        public string BillingState
        {
            get { return billingState; }
            set { billingState = value; }
        }
        public int BillingZip
        {
            get { return billingZip; }
            set { billingZip = value; }
        }
        public string SecurityQ1
        {
            get { return securityQuestion1; }
            set { securityQuestion1 = value; }
        }
        public string SecurityQ2
        {
            get { return securityQuestion2; }
            set { securityQuestion2 = value; }
        }
        public string SecurityQ3
        {
            get { return securityQuestion3; }
            set { securityQuestion3 = value; }
        }
    }
}
