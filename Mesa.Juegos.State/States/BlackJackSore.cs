using Fluxor;
using Mesa_SV.BlackJack.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.State.States
{
    /// <summary>
    /// store que se encarga de almacenar la mano del jugador
    /// </summary>
    /// <param name="Mano"></param>
    /// <param name="Loader"></param>
    public record BlackJackSore(ImmutableList<CardOutput>? Mano, BlackJackLoaders Loader);

    public record BlackJackLoaders(bool CardListIsLoading);

    [FeatureState]
    public class AgentsFeatureStore : Feature<BlackJackSore>
    {
        public override string GetName() => nameof(BlackJackSore);

        protected override BlackJackSore GetInitialState()
        {
            return new BlackJackSore(ImmutableList.Create<CardOutput>(), new BlackJackLoaders(false));
        }
    }
}
