﻿using Fluxor;
using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.TimeReal.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.State.Effects
{
    public class OnStartCreateBlackJackEffect : EffectBase<StartCreateBlackJack>
    {
        private readonly IHubConnectionService _hubConnectionService;
        public OnStartCreateBlackJackEffect(IHubConnectionService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;
        }

        public async override Task ExecuteAsync(StartCreateBlackJack action, IDispatcher dispatcher)
        {
            HubConnection hubConnection = _hubConnectionService.GetHubConnection();

            // Envía el la data al servidor
            await hubConnection.SendAsync("StartGameBlackJack", action.RequestId);
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
    }
}
