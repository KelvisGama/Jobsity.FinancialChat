using Jobsity.FinancialChat.Application.Messages.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.WebUI.Services
{
    public interface IMessageService 
    {
        Task AddMessageAsync(string body, string userName, DateTime when);
        Task<IEnumerable<MessageDto>> GetMessagesAsync();
    }
}
