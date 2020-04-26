using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingSiteLibrary
{
    public class MemberSearchResults : SearchResult
    {
        string title;
        string commitment;
        string haveKids;
        string wantKids;
        string occupation;
        

        public MemberSearchResults()
        {

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

        public string Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        
    }
}
