using Moq;
using System;
using Xunit;

namespace Mediator.Tests
{
    public class ConcreteColleagueTest
    {
        public class Constructor : ConcreteColleagueTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullName()
            {
                // arrange
                var messageWriterMock = new Mock<IMessageWriter<Message>>();

                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new ConcreteColleague(null, messageWriterMock.Object));

                // assert
                Assert.Equal("name", exception.ParamName);
            }


            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullMessageWriter()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new ConcreteColleague("Message Writer", null));

                // assert
                Assert.Equal("messageWriter", exception.ParamName);
            }
        }

        public class Receive : ConcreteColleagueTest
        {
            [Fact]
            public void ShouldWriteReceivedMessage()
            {
                // assert
                var messageWriterMock = new Mock<IMessageWriter<Message>>();
                messageWriterMock.Setup(m => m.Write(It.IsAny<Message>()));

                var sender = new Mock<IColleague>();

                var message = new Message(sender.Object, "some content");

                var sut = new ConcreteColleague("Concrete Colleague", messageWriterMock.Object);

                // act
                sut.ReceiveMessage(message);

                // assert
                messageWriterMock.Verify(m => m.Write(message), Times.Once());
            }
        }
    }
}
