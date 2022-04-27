using System;

namespace Mediator
{
    public class Message
    {
        public Message(IColleague sender, string content)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public IColleague Sender { get; }
        public string Content { get; }
    }
}
