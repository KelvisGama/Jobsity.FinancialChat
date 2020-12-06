using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.FinancialChat.Application.Message.Commands.AddMessage
{
    public class AddMessageCommand : IRequest
    {
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime When { get; set; }

        public AddMessageCommand(string userName, string body)
        {
            UserName = userName;
            Body = body;
            When = DateTime.Now;
        }
    }
}
