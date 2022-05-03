using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Webpage.POCO;

namespace Webpage.Shared.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string receiver, string sender)
        {           
            await Clients.All.SendAsync("ReceiveMessage", receiver, sender);
            await Clients.All.SendAsync("ReceiveMessage", sender, receiver);
        }
    }
}
