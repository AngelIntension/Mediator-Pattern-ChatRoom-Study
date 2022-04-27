using System;

namespace Mediator
{
    public class ChatMessage
    {
        public IParticipant Sender { get; }
        public string Content { get; }

        public ChatMessage(IParticipant from, string content)
        {
            Sender = from ?? throw new ArgumentNullException(nameof(from));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
