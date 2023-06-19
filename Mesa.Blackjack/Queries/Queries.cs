using MediatR;
using Mesa_SV.BlackJack.Dtos.Output;

namespace Mesa.Blackjack.Queries
{
    /// <summary>
    /// extrae las cartas de la base de datos de manera desordenada (barajeada)
    /// estas se van asignar al mazo de blackJack
    /// </summary>
   public record GetCards(): IRequest<List<OutputDtoCard>>;
}
