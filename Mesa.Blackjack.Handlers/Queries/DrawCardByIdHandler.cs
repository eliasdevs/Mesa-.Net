using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Queries;
using Mesa.BlackJack;
using Mesa_SV;
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
    /// <summary>
    /// pedir carta
    /// </summary>
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

            ManoJugador? datosJugador = blackjack.ManoJugadores?.FirstOrDefault(x => x.IdJugador == request.UserId);

            if(datosJugador == null || blackjack.ManoJugadores == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack.Mazo), GetType(), $"No se encontro registro de este usuario con Id {request.UserId}");

            //si ya estuvo plantado reinnicia las cartas
            if(datosJugador.estado == StatusHand.HAND)
                throw ClientException.CreateException(ClientExceptionType.InvalidOperation, nameof(datosJugador), GetType(), $"Usted no puede pedri mas cartas porque esta plantado");

            //elimina la carta seleccionada
            blackjack.Mazo.Remove(carta);

            //acualizar el vo de mano             
            datosJugador.Mano.Add(new Card(carta.OriginalValue, carta.SubValue, carta.Representation, carta.TypeOfCardId));
            datosJugador.estado= StatusHand.ACTIVE;

            //TODO: aqui cada que se pida carta hacer la cuenta que no se pase de 21 si lo hace mandar el mismo vo y plantarlo
            //para eso crear un helper en handler que haga la cudnta por si se usa en otra clase

            //agrega la carta al historial de blackjack
            blackjack?.History?.Add(
                new HistoryBlackJackVo(
                    new List<Card>() { new Card(carta.OriginalValue, carta.SubValue, carta.Representation, carta.TypeOfCardId) }, request.UserId, blackjack.ContadorMazo,
                    $"Se entrego la carta de Id {carta.Id} con valor {carta.OriginalValue}, sub value {carta.SubValue} y de tipo {carta.TypeOfCardId} al jugador {request.UserId}"));

            //guardo los cambios 
            await _repository.SaveChangesAsync();

            return carta;
        }
    }
}
