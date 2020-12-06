using Jobsity.FinancialChat.WebUI.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Jobsity.FinancialChat.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        public const string Url = "/chatroom";

        public async Task SendMessageAsync(string message)
            => await Clients.All.SendAsync("SendMessageAsync", message);

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
