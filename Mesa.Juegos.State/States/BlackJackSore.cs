using Fluxor;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;
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
    /// <param name="Request">La solicitud que se esta ligado</param>
    /// <param name="Loader">Se pone en true algunas propiedades cuando el proceso lo requiere</param>
    /// <param name="BlackJackRequests">Esta Representa todas Las solicitudes que existen, se debe actualizar cada que se creen, cierren o finalicen</param>
    public record BlackJackSore(ManoJugadorVo? Mano, 
        GameRequestBackJackOutput? Request,
        BlackjackStartOutput? BlackjackInfo,
        BlackJackLoaders Loader,
        ImmutableList<GameRequestBackJackOutput> BlackJackRequests
        );

    /// <summary>
    /// se pone en true dependiendo si esta en su proceso
    /// </summary>
    /// <param name="CardListIsLoading">Cuando esta cargando una carta o mano</param>
    /// <param name="RequestIsLoading">Cuando esta cargando la request</param>
    /// <param name="BlackJackIsLoading">Cuando esta cargado el blackJack inicarlo a o Finalizarlo</param>
    /// <param name="BlackJackIsInProgress">Esta es cuando se esta jugando</param>
    /// <param name="IsProcessingRequest">se indica si se esta creando una request</param>
    /// <param name="IsChallenger">Indica si el jugador es un retador</param>
    public record BlackJackLoaders(bool CardListIsLoading,
        bool RequestIsLoading, 
        bool BlackJackIsLoading, 
        bool BlackJackIsInProgress,
        bool IsProcessingRequest,
        bool IsChallenger
        );


    [FeatureState]
    public class AgentsFeatureStore : Feature<BlackJackSore>
    {
        public override string GetName() => nameof(BlackJackSore);

        protected override BlackJackSore GetInitialState()
        {
            return new BlackJackSore(null
                , null, null, 
                new BlackJackLoaders(false, false, false, false, false, false),
                ImmutableList.Create<GameRequestBackJackOutput>());
        }
    }
}
