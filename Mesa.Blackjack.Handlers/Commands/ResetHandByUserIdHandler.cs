using AutoMapper;
using MediatR;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Queries;
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
            List<CardOutput> cartas = new List<CardOutput>();

            Blackjack.Blackjack? blackjack = await _repository.GetBlackjackById(request.BackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack,
                    nameof(blackjack), GetType(), $"No se encontro registro de este juego.");

            //mano activa del jugador
            List<CardBlackJack> manoActiva = await _repository.GetHandActive(request.BackJackId, request.BackJackId);

            if (!manoActiva.Any())
                throw NotFoundException.CreateException(NotFoundExceptionType.Card,
                   nameof(blackjack), GetType(), $"Este jugador no posee mano activa.");

            bool todasEnEstadoHand = manoActiva.All(carta => carta.estado == StatusHand.STAND_HAND);

            //verificar si ya estan plantadas
            if (todasEnEstadoHand)
            {
                //Limpio la mano del jugador
                manoActiva.Clear();
                cartas = new List<CardOutput>();
            }

            await _repository.SaveChangesAsync();

            return new(request.UserId, cartas, StatusHand.ACTIVE);
        }
    }
}
