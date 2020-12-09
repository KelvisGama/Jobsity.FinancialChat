using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Interfaces;
using Jobsity.FinancialChat.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.FinancialChat.Application.Messages.Commands.AddMessage
{
    public class AddMessageCommand : IRequest<string>, IMapFrom<Message>
    {
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime When { get; set; }

        public AddMessageCommand() { }

        public AddMessageCommand(string userName, string body, DateTime when)
        {
            UserName = userName;
            Body = body;
            When = when;
        }

        public void Mapping(Profile profile)
            => profile.CreateMap<AddMessageCommand, Message>()
                .ForMember(d => d.Id, c => c.Ignore());
    }
}
