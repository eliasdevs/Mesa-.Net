using MediatR;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;

namespace Mesa.Blackjack.Commands
{
    /*string playerOne, string PlayerTwo*/
    /// <summary>
    /// recibe dos parametros e inicia el juego
    /// recibe el id del request para crear el backJack
    /// </summary>
    public record StartGame(string RequestId) : IRequest<Blackjack>;

    /// <summary>
    /// crea una solicitud de juego blackJack
    /// recibe solamente el id de la persona que hace la solicitud 
    /// puntos  a agregar :
    /// <item>notifica a todos los jugadores disponibles que hay una solicitud para ese request</item>
    /// </summary>
    public record CreateRequest(string UserId, string ContextId, GameMode GameMode) : IRequest<GameRequestBackJack>;

    /// <summary>
    /// recibe el id del jugador que acepta el request y  y el id de la solicitud
    /// al aceptar inicia el juego
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="RequestId"></param>
    public record AcceptedRequest(string UserId, string RequestId, string ContextId) : IRequest<GameRequestBackJack>;
    
    /// <summary>
    /// este comndo se va encargar de reemplazar el mazo cuando se ttermino el anterior
    /// </summary>
    /// <param name="BlackJackId"></param>
    public record ShuffleDeck(string BlackJackId): IRequest<HttpResponseMessage>;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="BlackJackId"></param>
    /// <param name="PlayerId"></param>
    public record StandHand(string BlackJackId, string PlayerId) : IRequest<ManoJugadorVo>;

    /// <summary>
    /// extrae una carta por medio del id del jugador y el id de la partida
    /// representa el boton pedir carta
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="RequestId"></param>
    public record DrawCardById(string UserId, string BackJackId) : IRequest<ManoJugadorVo>;
}
