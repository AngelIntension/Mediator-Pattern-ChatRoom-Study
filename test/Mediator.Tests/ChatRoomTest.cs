using Moq;
using Xunit;

namespace Mediator.Tests
{
    public class ChatRoomTest
    {
        public class Send : ChatRoomTest
        {
            [Fact]
            public void ShouldSendMessageToAllParticipants()
            {
                // arrange
                var participant1Mock = new Mock<IParticipant>();
                var participant2Mock = new Mock<IParticipant>();
                var participant3Mock = new Mock<IParticipant>();

                var sut = new ChatRoom();
                sut.Join(participant1Mock.Object);
                sut.Join(participant2Mock.Object);
                sut.Join(participant3Mock.Object);

                var message = new ChatMessage(participant1Mock.Object, "test message");

                // act
                sut.Send(message);

                // assert
                participant1Mock.Verify(p => p.ReceiveMessage(message));
                participant2Mock.Verify(p => p.ReceiveMessage(message));
                participant3Mock.Verify(p => p.ReceiveMessage(message));
            }
        }

        public class Join : ChatRoomTest
        {
            [Fact]
            public void ShouldSendParticipantJoinedMessageToAllParticipants()
            {
                // arrange
                var participant1Mock = new Mock<IParticipant>();
                participant1Mock.Setup(p => p.ReceiveMessage(It.IsAny<ChatMessage>()))
                    .Callback<ChatMessage>(message => Assert.Equal("Has joined the channel", message.Content));

                var participant2Mock = new Mock<IParticipant>();
                participant2Mock.Setup(p => p.ReceiveMessage(It.IsAny<ChatMessage>()))
                    .Callback<ChatMessage>(message => Assert.Equal("Has joined the channel", message.Content));

                var sut = new ChatRoom();

                // act
                sut.Join(participant1Mock.Object);
                participant1Mock.Verify(p => p.ReceiveMessage(It.IsAny<ChatMessage>()), Times.Once());
                sut.Join(participant2Mock.Object);

                // assert
                participant1Mock.Verify(p => p.ReceiveMessage(It.IsAny<ChatMessage>()), Times.Exactly(2));
                participant2Mock.Verify(p => p.ReceiveMessage(It.IsAny<ChatMessage>()), Times.Once());
            }

            [Fact]
            public void ShouldCauseParticipantToJoinChatRoom()
            {
                // arrange
                var participantMock = new Mock<IParticipant>();
                var sut = new ChatRoom();

                // act
                sut.Join(participantMock.Object);

                // assert
                participantMock.Verify(p => p.ChatRoomJoined(sut));
            }
        }
    }
}
