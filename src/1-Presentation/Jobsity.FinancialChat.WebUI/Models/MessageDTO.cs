using System;

namespace Jobsity.FinancialChat.WebUI.Models
{
    public class MessageDTO
    {
        public string UserName { get; set; }
        public string Body { get; set; }
        public bool IsFromCurrentUser { get; set; }

        public DateTime When { get; set; }

        public MessageDTO(string body, string userName, bool isFromCurrentUser)
        {
            Body = body;
            UserName = userName;
            IsFromCurrentUser = isFromCurrentUser;
            When = DateTime.Now;
        }
    }
}
