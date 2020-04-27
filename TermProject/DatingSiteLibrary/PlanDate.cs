using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingSiteLibrary
{
    public class PlanDate
    {
        string date;
        string time;
        string description;

        public PlanDate()
        {

        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
