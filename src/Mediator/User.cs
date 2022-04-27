using System;

namespace Mediator
{
    public class User : IParticipant
    {
        private readonly IMessageWriter<ChatMessage> messageWriter;
        private IChatRoom chatRoom;

        public User(string name, IMessageWriter<ChatMessage> messageWriter)
        {
            this.messageWriter = messageWriter ?? throw new ArgumentNullException(nameof(messageWriter));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public void ReceiveMessage(ChatMessage chatMessage)
        {
            messageWriter.Write(chatMessage);
        }

        public void Send(string message)
        {
            chatRoom.Send(new(this, message));
        }

        public void ChatRoomJoined(IChatRoom chatRoom)
        {
            this.chatRoom = chatRoom;
        }
    }
}
