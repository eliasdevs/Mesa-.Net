using Microsoft.AspNetCore.SignalR;

namespace Mesa.RealTime.Project.Hubs
{
    /// <summary>
    /// este proyecto va controlar el realtime de los juegos
    /// aparentemente es un netcore pero no se usara como tal si no como servidor signal
    /// </summary>
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
