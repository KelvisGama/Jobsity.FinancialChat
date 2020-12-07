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

        public MessageService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task AddMessageAsync(string body, string userName, DateTime when)
        {
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
    }
}
