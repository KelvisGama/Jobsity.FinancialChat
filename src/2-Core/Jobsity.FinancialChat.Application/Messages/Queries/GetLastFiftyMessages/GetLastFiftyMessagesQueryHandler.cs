﻿using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Interfaces.Repositories;
using Jobsity.FinancialChat.Application.Messages.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Application.Messages.Queries.GetLastFiftyMessages
{
    public class GetLastFiftyMessagesQueryHandler : IRequestHandler<GetLastFiftyMessagesQuery, IEnumerable<MessageDto>>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public GetLastFiftyMessagesQueryHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MessageDto>> Handle(GetLastFiftyMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = await _messageRepository.GetLastFiftyMessages();
            messages = messages.OrderByDescending(m => m.When).Take(50);
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }
    }
}
