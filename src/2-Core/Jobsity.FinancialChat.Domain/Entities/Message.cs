using System;

namespace Jobsity.FinancialChat.Domain.Entities
{
    public class Message
    {
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime When { get; set; }

        public Message()
        {
            When = DateTime.Now;
        }
    }
}
