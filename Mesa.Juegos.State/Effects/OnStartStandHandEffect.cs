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
    public class OnStartStandHandEffect : EffectBase<StartStandHand>
    {
        private readonly IHubConnectionService _hubConnectionService;
        
        public OnStartStandHandEffect(IHubConnectionService hubConnectionService)
        {
            _hubConnectionService = hubConnectionService;
        }

        public async override Task ExecuteAsync(StartStandHand action, IDispatcher dispatcher)
        {
            HubConnection hubConnection = _hubConnectionService.GetHubConnection();

            // Envía el la data al servidor
            await hubConnection.SendAsync("StandHand", action.PlayerId, action.BlackJackId, action.RequestId);
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }
    }
}
