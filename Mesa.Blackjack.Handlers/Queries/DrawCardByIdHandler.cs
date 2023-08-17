using AutoMapper;
using MediatR;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Queries;
using Mesa.BlackJack;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.BlackJack.Helper;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;
using System.Collections.Generic;

namespace Mesa.Blackjack.Handlers.Queries
{
    /// <summary>
    /// pedir carta
    /// </summary>
    public class DrawCardByIdHandler : IRequestHandler<DrawCardById, ManoJugadorVo>
    {
        private readonly IBlackJackRepository _repository;
        private readonly IMapper _mapper;

        public DrawCardByIdHandler(IBlackJackRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ManoJugadorVo> Handle(DrawCardById request, CancellationToken cancellationToken)
        {
            Blackjack? blackjack = await _repository.GetBlackjackByIdWithIncludes(request.BackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack), GetType(), "No se encontro la partida solicitada");

            if (!blackjack.ValidarUser(request.UserId))
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack), GetType(), "No se encontro la partida solicitada");

            if (blackjack.Mazo.Count() == 0)
                throw NotFoundException.CreateException(NotFoundExceptionType.Card, nameof(blackjack.Mazo), GetType(), "No se encontraron cartas disponibles");

            //se saca la primera carta de la lista
            Card carta = blackjack.Mazo[0];

            ManoJugador? datosJugador = blackjack.ManoJugadores?.FirstOrDefault(x => x.IdJugador == request.UserId);

            if (datosJugador == null || blackjack.ManoJugadores == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack.Mazo), GetType(), $"No se encontro registro de este usuario con Id {request.UserId}");

            //si ya esta plantado mando la mimsma mano
            if (datosJugador.estado == StatusHand.STAND_HAND)
                return new(datosJugador.IdJugador, _mapper.Map<List<CardOutput>>(datosJugador.Mano), datosJugador.estado);

            //elimina la carta seleccionada
            blackjack.Mazo.Remove(carta);

            //Agrego la carta a la mano del jugador
            datosJugador.Mano.Add(new Card(carta.OriginalValue, carta.SubValue, carta.Representation, carta.TypeOfCardId));
            datosJugador.estado = StatusHand.ACTIVE;

            //son las cartas que se mandaran en el output
            List<CardOutput> cartas = _mapper.Map<List<CardOutput>>(datosJugador.Mano);

            //calcula la puntuacion de la mano
            if (CalculateManoBlackJack.CalcularPuntuacion(cartas) >= 21)
            {
                //si es mayor o igual a 21 lo pongo plantado para que no pueda pedir mas cartas
                datosJugador.estado = StatusHand.STAND_HAND;
            }

            //agrega la carta al historial de blackjack
            blackjack?.History?.Add(
                new HistoryBlackJackVo(
                    new List<Card>() { new Card(carta.OriginalValue, carta.SubValue, carta.Representation, carta.TypeOfCardId) }, request.UserId, blackjack.ContadorMazo,
                    $"Se entrego la carta de Id {carta.Id} con valor {carta.OriginalValue}, sub value {carta.SubValue} y de tipo {carta.TypeOfCardId} al jugador {request.UserId}"));

            //guardo los cambios 
            await _repository.SaveChangesAsync();

            return new(datosJugador.IdJugador, cartas, datosJugador.estado);
        }
    }
}
