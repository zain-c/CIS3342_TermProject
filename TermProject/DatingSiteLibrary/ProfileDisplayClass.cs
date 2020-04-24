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
    public class ProfileDisplayClass
    {
        string username;
        string firstName;
        string lastName;
        string title;
        int age;

        public ProfileDisplayClass()
        {

        }

        public ProfileDisplayClass retreiveProfileDisplayFromDB(int userID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCmd = new SqlCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "TP_GetProfileDisplay";
            objCmd.Parameters.AddWithValue("@userID", userID);

            DataSet profileDisplayDS = objDB.GetDataSetUsingCmdObj(objCmd);
            ProfileDisplayClass profileDisplay = new ProfileDisplayClass();
            if(profileDisplayDS.Tables[0].Rows.Count == 1)
            {
                DataTable profileDisplayDT = profileDisplayDS.Tables[0];
                profileDisplay.Username = profileDisplayDT.Rows[0]["Username"].ToString();
                profileDisplay.FirstName = profileDisplayDT.Rows[0]["FirstName"].ToString();
                profileDisplay.LastName = profileDisplayDT.Rows[0]["LastName"].ToString();
                profileDisplay.Title = profileDisplayDT.Rows[0]["Title"].ToString();
                profileDisplay.Age = int.Parse(profileDisplayDT.Rows[0]["Age"].ToString());
            }
            return profileDisplay;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
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

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}
