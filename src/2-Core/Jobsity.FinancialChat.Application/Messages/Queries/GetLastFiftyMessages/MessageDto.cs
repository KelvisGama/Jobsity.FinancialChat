using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Interfaces;
using Jobsity.FinancialChat.Domain.Entities;
using System;

namespace Jobsity.FinancialChat.Application.Messages.Queries.GetLastFiftyMessages
{
    public class MessageDto : IMapFrom<Message>
    {
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime When { get; set; }

        public void Mapping(Profile profile)
            => profile.CreateMap<Message, MessageDto>();
    }
}
