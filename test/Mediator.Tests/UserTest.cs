using Moq;
using System;
using Xunit;

namespace Mediator.Tests
{
    public class UserTest
    {
        public class Constructor : UserTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullMessageWriter()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new User(null, "some name"));

                // assert
                Assert.Equal("messageWriter", exception.ParamName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullName()
            {
                // arrange
                var messageWriter = new Mock<IMessageWriter<ChatMessage>>();

                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new User(messageWriter.Object, null));

                // assert
                Assert.Equal("name", exception.ParamName);
            }
        }

        public class ReceiveMessage : UserTest
        {
            [Fact]
            public void ShouldWriteReceivedMessage()
            {
                // arrange
                var chatMessageWriterMock = new Mock<IMessageWriter<ChatMessage>>();

                var participantMock = new Mock<IParticipant>();

                var message = new ChatMessage(participantMock.Object, "some message");

                var sut = new User(chatMessageWriterMock.Object, "User Name");

                // act
                sut.ReceiveMessage(message);

                // assert
                chatMessageWriterMock.Verify(c => c.Write(message), Times.Once());
            }
        }

        public class Send : UserTest
        {
            [Fact]
            public void ShouldSendMessageToUsersCurrentChatRoom()
            {
                // arrange
                var messageWriter = new Mock<IMessageWriter<ChatMessage>>();
                var chatRoom = new Mock<IChatRoom>();
                chatRoom.Setup(c => c.Send(It.IsAny<ChatMessage>()))
                    .Callback<ChatMessage>(message => Assert.Equal("test message", message.Content));

                var sut = new User(messageWriter.Object, "some name");
                sut.ChatRoomJoined(chatRoom.Object);

                // act
                sut.Send("test message");

                // assert
                chatRoom.Verify(c => c.Send(It.IsAny<ChatMessage>()), Times.Once());
            }
        }
    }
}
