using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Interfaces;
using Jobsity.FinancialChat.Domain.Entities;
using System;

namespace Jobsity.FinancialChat.Application.Messages.Models
{
    public class MessageDto : IMapFrom<Message>
    {
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime When { get; set; }

        public MessageDto() { }
        public MessageDto(string body, string userName, DateTime when)
        {
            Body = body;
            UserName = userName;
            When = when;
        }

        public void Mapping(Profile profile)
            => profile.CreateMap<Message, MessageDto>();
    }
}
