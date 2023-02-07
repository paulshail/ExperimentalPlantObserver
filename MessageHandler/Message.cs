using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageHandler
{
    public class Message
    {

        public Message(string title, string description, MessageTypes messageTypes)
        {
            title = title;
        }

        public string Title { get; }
        public MessageTypes MessageTypes { get; }
        public string Description { get; }

    }
}
