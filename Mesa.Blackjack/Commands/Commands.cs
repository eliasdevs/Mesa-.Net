using MediatR;
using Mesa_SV.BlackJack.Dtos.Output;

namespace Mesa.Blackjack.Commands
{
    /*string playerOne, string PlayerTwo*/
    /// <summary>
    /// recibe dos parametros e inicia el juego
    /// recibe el id del request para crear el backJack
    /// </summary>
    public record StartGame(string RequestId) : IRequest<List<OutputDtoCard>>;

    /// <summary>
    /// crea una solicitud de juego blackJack
    /// recibe solamente el id de la persona que hace la solicitud 
    /// puntos  a agregar :
    /// <item>notifica a todos los jugadores disponibles que hay una solicitud para ese request</item>
    /// </summary>
    public record CreateRequest(string UserId) : IRequest<GameRequestBackJack>;

    /// <summary>
    /// recibe el id del jugador que acepta el request y  y el id de la solicitud
    /// al aceptar inicia el juego
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="RequestId"></param>
    public record AceptedRequest(string UserId, string RequestId): IRequest<GameRequestBackJack>; 
}
