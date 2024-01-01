using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Handlers.Helper;
using Mesa.BlackJack.Handlers.Helper;
using Mesa_SV;
using Mesa_SV.BlackJack;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.BlackJack.Helper;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;

namespace Mesa.BlackJack.Handlers.Commands
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
            //son las cartas que se mandaran en el output            
            Blackjack.Blackjack? blackjack = await _repository.GetBlackjackById(request.BackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack), GetType(), "No se encontro la partida solicitada");

            List<CardBlackJack> manoActiva = await _repository.GetHandActive(request.UserId, request.BackJackId);

            if (GetMano.AllCardsSatatusSatnd(manoActiva))
                return GetMano.GetHandWithStatus(request.UserId, manoActiva, _mapper);

            List<CardBlackJack> mazo = await _repository.GetMazoBlackJackAsync(request.BackJackId);

            if (!mazo.Any())
            {
                //si ya no hay cartas saco la baraja global y asigno nuevo mazo
                DeckOfCards baraja = await _repository.GetDeckOfCardsAsync();

                //mando a generar el nuevo mazo
                await AddMazoHelper.AgregarCartas(CardHelper.BarajearCartas(baraja, blackjack.Id), _repository);

                //consulto el mazo
                mazo = await _repository.GetMazoBlackJackAsync(request.BackJackId);
            }

            //se saca la primera carta de la lista
            CardBlackJack carta = mazo[0];
            carta.SetearJugador(request.UserId);

            await _repository.AddHistoryBlackJackAsync(new HistoryBlackJack(
                    request.UserId, blackjack.ContadorMazo,
                    $"Se entrego la carta de Id {carta.CardId} con valor {carta.OriginalValue}, sub value {carta.SubValue} y de tipo {carta.TypeOfCardId} al jugador {request.UserId}", blackjack.Id));

            //se guarda los cambios
            await _repository.SaveChangesAsync();

            //mano activa del jugador
            manoActiva = await _repository.GetHandActive(request.UserId, request.BackJackId);

            //calcula la puntuacion de la mano
            if (CalculateManoBlackJack.CalcularPuntuacion(_mapper.Map<List<CardOutput>>(manoActiva)) >= 21)
            {
                //si es mayor o igual a 21 lo pongo plantado para que no pueda pedir mas cartas
                manoActiva.ForEach(carta => carta.Estado = StatusHand.STAND_HAND);

                //se guarda los cambios
                await _repository.SaveChangesAsync();

                return GetMano.GetHandWithStatus(request.UserId, manoActiva, _mapper);
            }

            return GetMano.GetHandWithStatus(request.UserId, manoActiva, _mapper);
        }
    }
}
