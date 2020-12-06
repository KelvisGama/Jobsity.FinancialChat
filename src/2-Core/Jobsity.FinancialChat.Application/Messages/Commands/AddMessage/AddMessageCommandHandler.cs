using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Interfaces.Repositories;
using Jobsity.FinancialChat.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Application.Messages.Commands.AddMessage
{
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, string>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public AddMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(AddMessageCommand request, CancellationToken cancellationToken)
        {
            var newMessage = _mapper.Map<Message>(request);

            newMessage = await _messageRepository.CreateAsync(newMessage);

            return newMessage.Id;
        }
    }
}
