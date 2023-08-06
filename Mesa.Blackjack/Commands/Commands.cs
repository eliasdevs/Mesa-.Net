using MediatR;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;

namespace Mesa.Blackjack.Commands
{
    /*string playerOne, string PlayerTwo*/
    /// <summary>
    /// recibe dos parametros e inicia el juego
    /// recibe el id del request para crear el backJack
    /// </summary>
    public record StartGame(string RequestId) : IRequest<GameRequestBackJack>;

    /// <summary>
    /// crea una solicitud de juego blackJack
    /// recibe solamente el id de la persona que hace la solicitud 
    /// puntos  a agregar :
    /// <item>notifica a todos los jugadores disponibles que hay una solicitud para ese request</item>
    /// </summary>
    public record CreateRequest(string UserId, string ContextId, TypeGame tipoJuego) : IRequest<GameRequestBackJack>;

    /// <summary>
    /// recibe el id del jugador que acepta el request y  y el id de la solicitud
    /// al aceptar inicia el juego
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="RequestId"></param>
    public record AcceptedRequest(string UserId, string RequestId, string ContextId) : IRequest<GameRequestBackJack>;
    
    /// <summary>
    /// este se dara cuando el user le de click al boton "Pararse o en  todo caso se llame de otra forma como Plantarse"
    /// Lo que hara el comando es actualizar el mazo es decir eliminar las cartas del mazo actual y pasarlas a history
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="CardsIds"></param>
    public record UpdateMazoBackJack(string UserId, List<int> CardsIds, string BackJackId) :IRequest;

    /// <summary>
    /// este comndo se va encargar de reemplazar el mazo cuando se ttermino el anterior
    /// </summary>
    /// <param name="BlackJackId"></param>
    public record ShuffleDeck(string BlackJackId): IRequest<HttpResponseMessage>;
}
