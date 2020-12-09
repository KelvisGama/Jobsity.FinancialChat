using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Interfaces.Repositories;
using Jobsity.FinancialChat.Application.Messages.Models;
using Jobsity.FinancialChat.Application.Messages.Queries.GetLastFiftyMessages;
using Jobsity.FinancialChat.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace Jobsity.FinancialChat.Application.Tests.Messages.Queries.GetLastFiftyMessages
{
    public class GetLastFiftyMessagesQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapMock;
        private readonly Mock<IMessageRepository> _messageRepositoryMock;

        public GetLastFiftyMessagesQueryHandlerTests()
        {
            _messageRepositoryMock = GetMessageRepositoryMock(51);
            _mapMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task When_HandleGetLastFiftyMessagesQuery_Should_ReturnMaxFiftyMessages()
        {
            // arrange
            var handler = new GetLastFiftyMessagesQueryHandler(_messageRepositoryMock.Object, _mapMock.Object);

            // act
            var result = await handler.Handle(new GetLastFiftyMessagesQuery(), CancellationToken.None);

            // assert
            var dtos = result.ToList();
            dtos.Count().ShouldBeLessThanOrEqualTo(50);
            dtos.ShouldBeOfType(typeof(List<MessageDto>));
        }

        private Mock<IMessageRepository> GetMessageRepositoryMock(int count)
        {
            var mock = new Mock<IMessageRepository>();
            mock.Setup(x => x.GetLastFiftyMessages())
                .ReturnsAsync(Enumerable.Range(0, count).Select(x => new Message()));

            return mock;
        }
    }
}