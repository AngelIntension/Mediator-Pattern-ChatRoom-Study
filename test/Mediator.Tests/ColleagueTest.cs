using Moq;
using System;
using Xunit;

namespace Mediator.Tests
{
    public class ColleagueTest
    {
        public class Constructor : ColleagueTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullName()
            {
                // arrange
                var messageWriterMock = new Mock<IMessageWriter<Message>>();

                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new Colleague(null, messageWriterMock.Object));

                // assert
                Assert.Equal("name", exception.ParamName);
            }


            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullMessageWriter()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new Colleague("Message Writer", null));

                // assert
                Assert.Equal("messageWriter", exception.ParamName);
            }
        }

        public class Receive : ColleagueTest
        {
            [Fact]
            public void ShouldWriteReceivedMessage()
            {
                // assert
                var messageWriterMock = new Mock<IMessageWriter<Message>>();

                var sender = new Mock<IColleague>();

                var message = new Message(sender.Object, "some content");

                var sut = new Colleague("Concrete Colleague", messageWriterMock.Object);

                // act
                sut.ReceiveMessage(message);

                // assert
                messageWriterMock.Verify(m => m.Write(message), Times.Once());
            }
        }
    }
}
