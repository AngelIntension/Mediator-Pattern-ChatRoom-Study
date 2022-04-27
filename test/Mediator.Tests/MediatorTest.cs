using System.Text;
using Xunit;

namespace Mediator.Tests
{
    public class MediatorTest
    {
        [Fact]
        public void SendAMessageToAllColleagues()
        {
            // arrange
            var (millerWriter, miller) = CreateConcreteColleague("Miller");
            var (orazioWriter, orazio) = CreateConcreteColleague("Orazio");
            var (fletcherWriter, fletcher) = CreateConcreteColleague("Fletcher");

            var mediator = new ConcreteMediator(miller, orazio, fletcher);
            var expectedOutput = "[Miller]: Hey everyone!\r\n[Orazio]: What's up Miller?\r\n[Fletcher]: Hey Miller!\r\n";

            // act
            mediator.Send(new Message(
                sender: miller,
                content: "Hey everyone!"
            ));
            mediator.Send(new Message(
                sender: orazio,
                content: "What's up Miller?"
            ));
            mediator.Send(new Message(
                sender: fletcher,
                content: "Hey Miller!"
            ));

            // assert
            Assert.Equal(expectedOutput, millerWriter.Output.ToString());
            Assert.Equal(expectedOutput, orazioWriter.Output.ToString());
            Assert.Equal(expectedOutput, fletcherWriter.Output.ToString());
        }

        private (TestMessageWriter, ConcreteColleague) CreateConcreteColleague(string name)
        {
            var messageWriter = new TestMessageWriter();
            var concreteColleague = new ConcreteColleague(name, messageWriter);
            return (messageWriter, concreteColleague);
        }

        private class TestMessageWriter : IMessageWriter<Message>
        {
            public StringBuilder Output { get; } = new StringBuilder();
            public void Write(Message message)
            {
                Output.AppendLine($"[{message.Sender.Name}]: {message.Content}");
            }
        }
    }
}
