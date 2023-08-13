using Fluxor;

using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.Juegos.State.Shared;
using Mesa.TimeReal.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;

namespace Mesa.Juegos.State.Effects
{
    internal class OnStartGetAllRequestEffect : EffectBase<StartGetAllRequest>
    {
        private readonly IHubConnectionService _hubConnectionService;
        public OnStartGetAllRequestEffect(IHubConnectionService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;
        }
        public override async  Task ExecuteAsync(StartGetAllRequest action, IDispatcher dispatcher)
        {
            HubConnection hubConnection = _hubConnectionService.GetHubConnection();


            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                // Inicia la Coneccion
                await hubConnection.StartAsync();
            }

            // Envía el mensaje al servidor
            await hubConnection.SendAsync("GetAllRequestGame");

            Console.WriteLine("Se envió una solicitud para obtener la lista de juegos.");

        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
        
    }
}
