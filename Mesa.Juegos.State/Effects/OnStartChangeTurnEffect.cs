using Fluxor;
using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.TimeReal.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;

namespace Mesa.Juegos.State.Effects
{
    internal class OnStartChangeTurnEffect : EffectBase<StartChangeTurn>
    {
        private readonly IHubConnectionService _hubConnectionService;
        public OnStartChangeTurnEffect(IHubConnectionService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;
        }

        public async override Task ExecuteAsync(StartChangeTurn action, IDispatcher dispatcher)
        {
            if (!action.IsTurn)
            {
                HubConnection hubConnection = _hubConnectionService.GetHubConnection();

                // Envía el la data al servidor
                await hubConnection.SendAsync("PlayerTurn", action.requestId, true);
            }            
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
    }
}
