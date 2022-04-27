using System;

namespace Mediator
{
    public class ConcreteColleague : IColleague
    {
        private readonly IMessageWriter<Message> messageWriter;

        public ConcreteColleague(string name, IMessageWriter<Message> messageWriter)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            this.messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
        }

        public string Name { get; }
        public void ReceiveMessage(Message message)
        {
            messageWriter.Write(message);
        }
    }
}
