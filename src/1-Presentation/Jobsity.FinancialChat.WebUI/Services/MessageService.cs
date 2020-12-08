using AutoMapper;
using Jobsity.FinancialChat.Application.Messages.Commands.AddMessage;
using Jobsity.FinancialChat.Application.Messages.Models;
using Jobsity.FinancialChat.Application.Messages.Queries.GetLastFiftyMessages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.WebUI.Services
{
    public class MessageService : IMessageService
    {
        private IMediator _mediator;
        private IMapper _mapper;
        private readonly IRabbitMqService _rabbitMqService;

        public MessageService(IMediator mediator, IMapper mapper, IRabbitMqService rabbitMqService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _rabbitMqService = rabbitMqService;
        }

        public async Task AddMessageAsync(string body, string userName, DateTime when)
        {
            if (IsCommandMessage(body))
            {
                var stockName = body.Split("=")[1];
                _rabbitMqService.PushMessageToWorker(stockName);
                return;
            }

            var command = new AddMessageCommand
            {
                Body = body,
                UserName = userName,
                When = when
            };

            await _mediator.Send(command);
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesAsync()
            => await _mediator.Send(new GetLastFiftyMessagesQuery());

        private bool IsCommandMessage(string message)
            => message.Split("=")[0]?.Equals("/stock") ?? false;
    }
}
