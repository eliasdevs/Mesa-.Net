using Mesa_SV.BlackJack.Config;
using Microsoft.AspNetCore.SignalR.Client;

namespace Mesa.TimeReal.Services
{
    public class HubConnectionService : IHubConnectionService
    {
        private readonly HubConnection _hubConnection;

        public HubConnectionService(ConfigRealTime _config)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_config.UrlBlackJackRealTime)
                .Build();
        }
        public HubConnection GetHubConnection()
        {
            return _hubConnection;
        }
    }
}