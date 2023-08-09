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
            try
            {
                HubConnection hubConnection = _hubConnectionService.GetHubConnection();

                // Manejar el evento OnConnected antes de iniciar la conexión
                hubConnection.On<string>("OnConnected", (message) =>
                {
                    // La conexión está lista para ser activada, iniciarla
                    StartConnection(hubConnection);
                });


                // Espera hasta que la conexión esté activa
                await hubConnection.StartAsync();

                // Envía el mensaje al servidor
                await hubConnection.SendAsync("GetAllRequestGame");

                Console.WriteLine("Se envió una solicitud para obtener la lista de juegos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar la solicitud para obtener la lista de juegos. "+ ex);
            }
         
           
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
        private async void StartConnection(HubConnection hubConnection)
        {
            try
            {
                await hubConnection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
            }
        }
    }
}
