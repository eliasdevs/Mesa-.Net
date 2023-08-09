using MediatR;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;

namespace Mesa.Blackjack.Queries
{
    /// <summary>
    /// extrae las cartas de la base de datos de manera desordenada (barajeada)
    /// estas se van asignar al mazo de blackJack
    /// </summary>
    public record GetCards(): IRequest<List<CardOutput>>;

    /// <summary>
    /// extrae una carta por medio del id del jugador y el id de la partida
    /// representa el boton pedir carta
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="RequestId"></param>
    public record DrawCardById(string UserId, string BackJackId) : IRequest<ManoJugadorVo>;

    /// <summary>
    /// este query permite extraer la mano activa completa del jugador este o no plantado
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="BackJackId"></param>
    public record GetHandActive(string UserId, string BackJackId) : IRequest<ManoJugadorVo>;

    /// <summary>
    /// permite extraer una solicitud por su id
    /// </summary>
    /// <param name="RequestId"></param>
    public record GetRequestById(string RequestId) : IRequest<GameRequestBackJack>;

    /// <summary>
    /// permite obtener todas las solicitudes
    /// </summary>
    /// <param name="RequestId"></param>
    public record GetRequests() : IRequest<List<GameRequestBackJack>>;
}
