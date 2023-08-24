using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.BlackJack;
using Mesa.BlackJack.Handlers.Helper;
using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
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

            List<CardBlackJack> manoActiva = await _repoBlackJack.GetHandActive(request.PlayerId, request.BlackJackId);

            if (!manoActiva.Any())
                throw NotFoundException.CreateException(NotFoundExceptionType.Card, nameof(manoActiva), GetType(), $"No ha sido posible plantar esta mano");

            manoActiva.ForEach(carta => carta.Estado = StatusHand.STAND_HAND);

            //actualiza los datos
            await _repoBlackJack.SaveChangesAsync();

            return GetMano.GetHandWithStatus(request.PlayerId, manoActiva, _mapper);
        }
    }

}
