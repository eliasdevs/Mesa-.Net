using Fluxor;
using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.TimeReal.Services;
using Mesa_SV;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.State.Effects
{
    internal class OnStartCreateRequestEffect : EffectBase<StartCreateRequest>
    {
        private readonly IHubConnectionService _hubConnectionService;
        public OnStartCreateRequestEffect(IHubConnectionService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;
        }

        public override async Task ExecuteAsync(StartCreateRequest action, IDispatcher dispatcher)
        {
            HubConnection hubConnection = _hubConnectionService.GetHubConnection();

            // Envía el la data al servidor
            await hubConnection.SendAsync("CreateRequest", action.PlayerId, action.TipoJuego ?? GameMode.CRUPIER_FRIENDLY);

            //consulta la lista completa y actualiza a todo mundo
            await hubConnection.SendAsync("GetAllRequestGame");
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
     
    }
}
