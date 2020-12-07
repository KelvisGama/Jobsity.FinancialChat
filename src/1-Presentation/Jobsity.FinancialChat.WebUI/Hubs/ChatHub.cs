using Jobsity.FinancialChat.WebUI.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        public const string Url = "/chatroom";
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessageAsync(string body, string userName, DateTime when)
        {
            // joined user message use an empty user name and should not be processed
            if (!string.IsNullOrWhiteSpace(userName))
                await _messageService.AddMessageAsync(body, userName, when);

            await Clients.All.SendAsync("SendMessageAsync", body, userName, when);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
