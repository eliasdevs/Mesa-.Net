using Fluxor;
using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.Juegos.State.Shared;
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
    public class OnStartAcceptRequestEffect : EffectBase<StartAcceptRequest>
    {
        private readonly IHubConnectionService _hubConnectionService;

        public OnStartAcceptRequestEffect(IHubConnectionService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;
        }

        public async override Task ExecuteAsync(StartAcceptRequest action, IDispatcher dispatcher)
        {   
            HubConnection hubConnection = _hubConnectionService.GetHubConnection();

            // Envía el la data al servidor
            await hubConnection.SendAsync("AcceptRequest", action.PlayerId, action.IdRequest);
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
    }
}
