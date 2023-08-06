﻿using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa_SV;
using Mesa_SV.Exceptions;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;

namespace Mesa.Blackjack.Handlers.Commands
{
    public class StandHandHandler : IRequestHandler<StandHand, ManoJugadorVo>
    {
        private readonly IMapper _mapper;
        private readonly IBlackJackRepository _repoBlackJack;
        
        public StandHandHandler(IBlackJackRepository repoBlackJack, IMapper mapper)
        {
            _mapper = mapper;
            _repoBlackJack = repoBlackJack;        
        }

        public async Task<ManoJugadorVo> Handle(StandHand request, CancellationToken cancellationToken)
        {
            Blackjack? blackjack = await _repoBlackJack.GetBlackjackById(request.BlackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack,
                    nameof(blackjack), GetType(), $"Error!!!, no se encontro registro de este juego.");

            ManoJugadorVo? datosJugador = blackjack.ManoJugadores?.FirstOrDefault(x => x.IdJugador == request.PlayerId);

            if (datosJugador == null || blackjack.ManoJugadores == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack.Mazo), GetType(), $"No se encontro registro de este usuario con Id {request.PlayerId}");

            if (datosJugador.Mano == null || datosJugador.Mano.Count() == 0)
                throw ClientException.CreateException(ClientExceptionType.InvalidOperation, nameof(datosJugador.Mano), GetType(), $"Accion no permitida");

            blackjack.ManoJugadores.Remove(datosJugador);

            //acualizar el vo de mano             
            blackjack.ManoJugadores.Add(new ManoJugadorVo(request.PlayerId, datosJugador.Mano, StatusHand.ACTIVE));

            //actualiza los datos
            await _repoBlackJack.SaveChangesAsync();

            return datosJugador;
        }
    }

}