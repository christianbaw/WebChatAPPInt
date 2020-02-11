using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using WebChatAPP.Data.Models;

namespace WebChatAPP.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            if (message.Text.Contains("stock"))

            {
                message.Text = message.Text + "testing";
                await Clients.All.SendAsync("receiveMessage", new { message.UserName, message.Text, message.MessageDate });
            }
            else
            {
                await Clients.All.SendAsync("receiveMessage", new { message.UserName, message.Text, message.MessageDate });
            }
            
        }
    }
}
