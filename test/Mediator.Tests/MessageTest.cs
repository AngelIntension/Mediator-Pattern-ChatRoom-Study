using Moq;
using System;
using Xunit;

namespace Mediator.Tests
{
    public class MessageTest
    {
        public class Contructor : MessageTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullSender()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new Message(null, "message"));

                // assert
                Assert.Equal("sender", exception.ParamName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullContent()
            {
                // arrange
                var sender = new Mock<IColleague>();

                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new Message(sender.Object, null));

                // assert
                Assert.Equal("content", exception.ParamName);
            }
        }
    }
}
