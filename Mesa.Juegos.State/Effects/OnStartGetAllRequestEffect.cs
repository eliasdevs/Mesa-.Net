using Fluxor;
using Mesa.Juegos.State.Actions;
using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.Juegos.State.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;

namespace Mesa.Juegos.State.Effects
{
    internal class OnStartGetAllRequestEffect : EffectBase<StartGetAllRequest>
    {
        private readonly ConfigRealTime _config;
        private HubConnection hubConnection;
        public OnStartGetAllRequestEffect(ConfigRealTime config)
        {
            _config = config;
            hubConnection = new HubConnectionBuilder()
           .WithUrl(_config.UrlBlackJackRealTime)
           .Build();
        }
        public override async  Task ExecuteAsync(StartGetAllRequest action, IDispatcher dispatcher)
        {
            await hubConnection.SendAsync("GetAllRequestGame", true);
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
    }
}
