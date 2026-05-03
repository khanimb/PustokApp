using Microsoft.AspNetCore.SignalR;
using PustokApp.Data;
using PustokApp.Models;
using System;

namespace PustokApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly PustokAppDbContext _context;

        public ChatHub(PustokAppDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string message)
        {
            var chatMsg = new ChatMessage
            {
                User = user,
                Message = message
            };
            _context.ChatMessages.Add(chatMsg);
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
