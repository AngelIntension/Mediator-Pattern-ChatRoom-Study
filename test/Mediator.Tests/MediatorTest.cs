using Moq;
using System;
using Xunit;

namespace Mediator.Tests
{
    public class MediatorTest
    {
        public class Constructor : MediatorTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullColleaguesArgument()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new Mediator(null));

                // assert
                Assert.Equal("colleagues", exception.ParamName);
            }
        }

        public class Send : MediatorTest
        {
            [Fact]
            public void ShouldSendSpecifiedMessageToAllColleagues()
            {
                // arrange
                var colleague1Mock = new Mock<IColleague>();
                var colleague2Mock = new Mock<IColleague>();
                var colleague3Mock = new Mock<IColleague>();

                var colleagues = new IColleague[]
                {
                    colleague1Mock.Object,
                    colleague2Mock.Object,
                    colleague3Mock.Object,
                };

                var senderMock = new Mock<IColleague>();

                var message = new Message(senderMock.Object, "test message");

                var sut = new Mediator(colleagues);

                // act
                sut.Send(message);

                // assert
                colleague1Mock.Verify(c => c.ReceiveMessage(message));
                colleague2Mock.Verify(c => c.ReceiveMessage(message));
                colleague3Mock.Verify(c => c.ReceiveMessage(message));
            }
        }
    }
}
