using MediatR;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;

namespace Mesa.Blackjack.Queries
{
    /// <summary>
    /// extrae las cartas de la base de datos de manera desordenada (barajeada)
    /// estas se van asignar al mazo de blackJack
    /// </summary>
   public record GetCards(): IRequest<List<OutputDtoCard>>;

    /// <summary>
    /// extrae una carta por medio del id del jugador y el id de la partida
    /// representa el boton pedir carta
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="RequestId"></param>
    public record GetCardById(string UserId, string BackJackId) : IRequest<Card>;
}
