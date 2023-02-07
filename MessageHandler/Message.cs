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
            this.Title = title;
            this.Description = description;
            this.MessageTypes = messageTypes;

        }

        public string Title { get; }
        public MessageTypes MessageTypes { get; }
        public string Description { get; }

    }
}
