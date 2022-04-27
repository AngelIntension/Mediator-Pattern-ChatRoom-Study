using System.Collections.Generic;

namespace Mediator
{
    public class ChatRoom : IChatRoom
    {
        private readonly List<IParticipant> participants = new List<IParticipant>();

        public void Join(IParticipant participant)
        {
            participants.Add(participant);
            participant.ChatRoomJoined(this);
            Send(new(participant, "Has joined the channel"));
        }

        public void Send(ChatMessage message)
        {
            participants.ForEach(p => p.ReceiveMessage(message));
        }
    }
}
