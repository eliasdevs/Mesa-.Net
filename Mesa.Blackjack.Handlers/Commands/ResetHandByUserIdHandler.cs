using AutoMapper;
using MediatR;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Queries;
using Mesa.BlackJack.Handlers.Helper;
using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;

namespace Mesa.BlackJack.Handlers.Commands
{
    public class ResetHandByUserIdHandler : IRequestHandler<ResetHandByUserId, ManoJugadorVo>
    {
        private readonly IBlackJackRepository _repository;
        
        public ResetHandByUserIdHandler(IBlackJackRepository repository)
        {
            _repository = repository;            
        }

        public async Task<ManoJugadorVo> Handle(ResetHandByUserId request, CancellationToken cancellationToken)
        {
            //son las cartas que se mandaran en el output
            Blackjack.Blackjack? blackjack = await _repository.GetBlackjackById(request.BackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack,
                    nameof(blackjack), GetType(), $"No se encontro registro de este juego.");

            //mano activa del jugador
            List<CardBlackJack> manoActiva = await _repository.GetHandActive(request.UserId, request.BackJackId);

            if (!manoActiva.Any())
                throw NotFoundException.CreateException(NotFoundExceptionType.Card,
                   nameof(blackjack), GetType(), $"Este jugador no posee mano activa.");

            //verificar si ya estan plantadas
            if (GetMano.AllCardsSatatusSatnd(manoActiva))
                await _repository.RemoveCardsFromHand(manoActiva);

            //reseteo
            return new(request.UserId, new List<CardOutput>(), StatusHand.ACTIVE);
        }
    }
}
