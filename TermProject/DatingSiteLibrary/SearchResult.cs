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
    public class SearchResult
    {
        string username;
        string firstName;
        string lastName;
        string city;
        string state;
        string gender;
        

        public SearchResult()
        {

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

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

    }
}
