using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Interfaces.Repositories;
using Jobsity.FinancialChat.Application.Messages.Commands.AddMessage;
using Jobsity.FinancialChat.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace Jobsity.FinancialChat.Application.Tests.Messages.Commands.AddMessage
{
    public class AddMessageCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapMock;
        private readonly Mock<IMessageRepository> _messageRepositoryMock;

        public AddMessageCommandHandlerTests()
        {
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _mapMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task When_AddMessageCommandHandler_Should_ReturnNewCreatedId()
        {
            // arrange
            var guid = Guid.NewGuid();
            _messageRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Message>()))
                .ReturnsAsync(new Message { Id = guid });

            var handler = new AddMessageCommandHandler(_messageRepositoryMock.Object, _mapMock.Object);

            // act
            var result = await handler.Handle(new AddMessageCommand(), CancellationToken.None);

            // assert
            result.ShouldBe(guid.ToString());
            result.ShouldBeOfType(typeof(string));
        }
    }
}