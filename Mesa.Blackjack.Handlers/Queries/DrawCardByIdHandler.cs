using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Queries;
using Mesa_SV.Exceptions;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.Queries
{
    public class DrawCardByIdHandler : IRequestHandler<DrawCardById, Card>
    {
        private readonly IBlackJackRepository _repository;
        public DrawCardByIdHandler(IBlackJackRepository repository)
        {
            _repository = repository;
        }
        public async Task<Card> Handle(DrawCardById request, CancellationToken cancellationToken)
        {
            Blackjack? blackjack = await _repository.GetBlackjackByUserId(request.UserId, request.BackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack), GetType(),"No se encontro la partida solicitada");

            //esta es la nueva carta que se va a mandar a los jugadores
            if(blackjack.Mazo.Count() == 0)
                throw NotFoundException.CreateException(NotFoundExceptionType.Card, nameof(blackjack.Mazo), GetType(), "No se encontraron cartas disponibles");
                 
            //se saca la primera carta ya que estan desordenadas
            Card carta = blackjack.Mazo[0];

            //elimina la carta seleccionada
            blackjack.Mazo.RemoveAt(0);

            //la carta eliminada se agrega a la mano del jugador para no perdeerloa por si se actualiza algo
            blackjack.ManoJugadores.FirstOrDefault(x => x.IdJugador == request.UserId)?.Mano?.Add(carta);            

            //agrega la carta al historial de blackjack
            blackjack?.History?.Add(
                new HistoryBlackJackVo(
                    new List<Card>() {carta}, request.UserId, blackjack.ContadorMazo,
                    $"Se entrego la carta de Id {blackjack.Mazo[0].Id} con valor {blackjack.Mazo[0].OriginalValue} y sub value {blackjack.Mazo[0].SubValue} al jugador {request.UserId}"));
            
            //guardo los cambios 
            await _repository.SaveChangesAsync();

            return carta;
        }
    }
}
