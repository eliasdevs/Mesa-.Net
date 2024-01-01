using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.BlackJack
{
    /// <summary>
    /// permite consumir el juego de BlackJack
    /// </summary>
    public interface IBlackJackJackSdk
    {
        /// <summary>
        /// 
        /// </summary>
        const string URLBASE = "/api/blackjack/";

        /// <summary>
        /// Permite Iniciar el juego mandando el id de la request y empareja los dos jugadores
        /// </summary>
        /// <param name="requestId">el id de la solicitud</param>
        /// <returns></returns>
        [Post(URLBASE + "startGame/{requestId}")]
        Task<BlackjackOutput> StartBlackJack(string requestId);

        /// <summary>
        /// permite crear una solicitud de juego
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="contextId"></param>
        /// <param name="tipoJuego"></param>
        /// <returns></returns>
        [Post(URLBASE + "users/{playerId}/request")]
        Task<GameRequestBackJackOutput> CreateRequest([FromRoute] string playerId, [FromQuery] string contextId, [FromQuery] GameMode tipoJuego);

        /// <summary>
        /// permite a un jugador aceptar la request
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="requestId"></param>
        /// <param name="contextId"></param>
        /// <returns></returns>
        [Put(URLBASE+ "users/{playerId}/request/{requestId}/accept")]
        Task<GameRequestBackJackOutput> AcceptRequest(string playerId, string requestId, [Query] string contextId);
        
        /// <summary>
        /// permite consultar todas las solicitudes en estado de pending
        /// </summary>
        /// <returns></returns>
        [Get(URLBASE + "request")]
        Task<List<GameRequestBackJackOutput>> GetAllRequest();

        /// <summary>
        /// pedir carta
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        [Get(URLBASE + "{blackjackId}/users/{playerId}/draw_card")]
        Task<ManoJugadorVo> GetCardById( string playerId, string blackjackId);

        /// <summary>
        /// este barajear cartas
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        [Post(URLBASE + "{blackjackId}/shuffle/")]
        Task<HttpResponseMessage> BarajearCartas(string blackjackId);

        /// <summary>
        /// este metodo permite plantarse
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [Post(URLBASE + "{blackjackId}/users/{playerId}/stand")]
        Task<ManoJugadorVo> PlantarBlackJack(string blackjackId, string playerId);

        /// <summary>
        /// este permite obtener la mano del jugador
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [Get(URLBASE + "{blackjackId}/users/{playerId}/Hand")]
        Task<ManoJugadorVo> GetActiveHand(string blackjackId, string playerId);

        /// <summary>
        /// permite extraer la solicitud por su Id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [Get(URLBASE + "request/{requestId}")]
        Task<GameRequestBackJackOutput> GetRequest(string requestId);

        /// <summary>
        /// Permite reiniciar la mano de un jugador
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [Patch(URLBASE + "{blackjackId}/users/{playerId}/reset_hand")]
        Task<ManoJugadorVo> ResetHand(string blackjackId, string playerId);
    }
}
