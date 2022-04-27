namespace Mediator
{
    public interface IParticipant
    {
        string Name { get; }
        void ChatRoomJoined(IChatRoom chatRoom);
        void ReceiveMessage(ChatMessage chatMessage);
        void Send(string message);
    }
}