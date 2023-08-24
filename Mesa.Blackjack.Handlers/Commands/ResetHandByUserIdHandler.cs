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
        private readonly IMapper _mapper;

        public ResetHandByUserIdHandler(IBlackJackRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ManoJugadorVo> Handle(ResetHandByUserId request, CancellationToken cancellationToken)
        {

            //son las cartas que se mandaran en el output
            Blackjack.Blackjack? blackjack = await _repository.GetBlackjackById(request.BackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack,
                    nameof(blackjack), GetType(), $"No se encontro registro de este juego.");

            //mano activa del jugador
            List<CardBlackJack> manoActiva = await _repository.GetHandActive(request.BackJackId, request.BackJackId);

            if (!manoActiva.Any())
                throw NotFoundException.CreateException(NotFoundExceptionType.Card,
                   nameof(blackjack), GetType(), $"Este jugador no posee mano activa.");

            //verificar si ya estan plantadas
            if (GetMano.AllCardsSatatusSatnd(manoActiva))
                manoActiva.Clear(); //Limpio la mano del jugador

            await _repository.SaveChangesAsync();

            //reseteo
            return new(request.UserId, new List<CardOutput>(), StatusHand.ACTIVE);
        }
    }
}
