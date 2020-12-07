using Jobsity.FinancialChat.Application.Messages.Models;
using MediatR;
using System.Collections.Generic;

namespace Jobsity.FinancialChat.Application.Messages.Queries.GetLastFiftyMessages
{
    public class GetLastFiftyMessagesQuery : IRequest<IEnumerable<MessageDto>>
    {
    }
}
