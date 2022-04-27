using Moq;
using System;
using Xunit;

namespace Mediator.Tests
{
    public class ChatMessageTest
    {
        public class Constructor : ChatMessageTest
        {
            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullFrom()
            {
                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new ChatMessage(null, "some content"));

                // assert
                Assert.Equal("from", exception.ParamName);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionGivenNullContent()
            {
                // arrange
                var participantMock = new Mock<IParticipant>();

                // act
                var exception = Assert.Throws<ArgumentNullException>(() => new ChatMessage(participantMock.Object, null));

                // assert
                Assert.Equal("content", exception.ParamName);
            }
        }
    }
}
