using System.Text;
using Xunit;

namespace Mediator.Tests
{
    public class ChatRoomIntegrationTest
    {
        [Fact]
        public void ChatRoomParticipantsShouldSendAndReceiveMessages()
        {
            // arrange
            var (kingChat, king) = CreateTestUser("King");
            var (kelleyChat, kelley) = CreateTestUser("Kelley");
            var (daveenChat, daveen) = CreateTestUser("Daveen");
            var (rutterChat, rutter) = CreateTestUser("Rutter");

            var sut = new ChatRoom();

            // act
            sut.Join(king);
            sut.Join(kelley);
            king.Send("Hey!");
            kelley.Send("What's up King?");
            sut.Join(daveen);
            king.Send("Everything is great, I joined the CrazyChatRoom!");
            daveen.Send("Hey King!");
            king.Send("Hey Daveen");

            // assert
            Assert.Empty(rutterChat.Output.ToString());
            Assert.Equal(
                "[King]: Has joined the channel\r\n" +
                "[Kelley]: Has joined the channel\r\n" +
                "[King]: Hey!\r\n" +
                "[Kelley]: What's up King?\r\n" +
                "[Daveen]: Has joined the channel\r\n" +
                "[King]: Everything is great, I joined the CrazyChatRoom!\r\n" +
                "[Daveen]: Hey King!\r\n" +
                "[King]: Hey Daveen\r\n",
                kingChat.Output.ToString()
            );

            Assert.Equal(
                "[Kelley]: Has joined the channel\r\n" +
                "[King]: Hey!\r\n" +
                "[Kelley]: What's up King?\r\n" +
                "[Daveen]: Has joined the channel\r\n" +
                "[King]: Everything is great, I joined the CrazyChatRoom!\r\n" +
                "[Daveen]: Hey King!\r\n" +
                "[King]: Hey Daveen\r\n",
                kelleyChat.Output.ToString()
            );

            Assert.Equal(
                "[Daveen]: Has joined the channel\r\n" +
                "[King]: Everything is great, I joined the CrazyChatRoom!\r\n" +
                "[Daveen]: Hey King!\r\n" +
                "[King]: Hey Daveen\r\n",
                daveenChat.Output.ToString()
            );
        }

        private (TestMessageWriter, User) CreateTestUser(string name)
        {
            var writer = new TestMessageWriter();
            var user = new User(writer, name);
            return (writer, user);
        }

        private class TestMessageWriter : IMessageWriter<ChatMessage>
        {
            public StringBuilder Output { get; } = new StringBuilder();

            public void Write(ChatMessage message)
            {
                Output.AppendLine($"[{message.Sender.Name}]: {message.Content}");
            }
        }
    }
}
