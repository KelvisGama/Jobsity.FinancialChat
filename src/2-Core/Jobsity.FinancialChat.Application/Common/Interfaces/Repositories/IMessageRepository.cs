using Jobsity.FinancialChat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Application.Common.Interfaces.Repositories
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<IEnumerable<Message>> GetLastFiftyMessages();
    }
}
