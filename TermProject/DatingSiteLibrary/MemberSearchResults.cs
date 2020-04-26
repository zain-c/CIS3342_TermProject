using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingSiteLibrary
{
    class MemberSearchResults : SearchResult
    {
        string title;
        string commitment;
        string haveKids;
        string wantKids;
        string interests;

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

        public string Interests
        {
            get { return interests; }
            set { interests = value; }
        }

        
    }
}
