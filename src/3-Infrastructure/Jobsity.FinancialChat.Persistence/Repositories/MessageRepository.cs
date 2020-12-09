using Jobsity.FinancialChat.Application.Common.Interfaces.Repositories;
using Jobsity.FinancialChat.Domain.Entities;
using Jobsity.FinancialChat.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.Persistence.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Message>> GetLastFiftyMessages()
            => await _context.Messages
                    .OrderByDescending(m => m.When)
                    .Take(50)
                    .AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);

    }
}
