using Fluxor;
using Mesa.Juegos.State.Actions.Blackjacks;
using Mesa.Juegos.State.Shared;
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
        public override Task ExecuteAsync(StartCreateRequest action, IDispatcher dispatcher)
        {
            throw new NotImplementedException();
        }

        public override Task OnException(ApiException ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new OnClearOnError());
            return Task.CompletedTask;
        }       
    }
}
