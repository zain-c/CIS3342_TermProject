using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingSiteLibrary
{
    public class Message
    {
        String fromUsername;
        String toUsername;
        String messageContent;

        public Message()
        {

        }

        public string FromUsername
        {
            get { return fromUsername; }
            set { fromUsername = value; }
        }

        public string ToUsername
        {
            get { return toUsername; }
            set { toUsername = value; }
        }
        public string MessageContent
        {
            get { return messageContent; }
            set { messageContent = value; }
        }


    }
}
