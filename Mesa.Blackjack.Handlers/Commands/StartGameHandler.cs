using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Handlers.Helper;
using Mesa.BlackJack;
using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.BlackJack.Model.Barajas;
using Mesa_SV.Exceptions;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Mesa.Blackjack.Handlers.Commands
{
    public class StartGameHandler : IRequestHandler<StartGame, Blackjack>
    {
        private readonly IMapper _mapper;
        private readonly IBlackJackRepository _repoBlackJack;
        private readonly IGameRequestBackJackRepository _repositoryRequest;
        public StartGameHandler(IBlackJackRepository repoBlackJack, IMapper mapper, IGameRequestBackJackRepository repositoryRequest)
        {
            _mapper = mapper;
            _repoBlackJack = repoBlackJack;
            _repositoryRequest = repositoryRequest;
        }

        public async Task<Blackjack> Handle(StartGame request, CancellationToken cancellationToken)
        {
            //extrae la request 
            GameRequestBackJack? solicitud = await _repositoryRequest.GetGameRequestBackJackAsync(request.RequestId);

            if (solicitud == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.Request,
                    nameof(solicitud), GetType(), $"Error!!!, la solicitud no existe.");

            if(solicitud.Status != GameRequestStatus.Accepted)
                throw ClientException.CreateException(ClientExceptionType.InvalidFieldValue,
                    nameof(request.RequestId), GetType(), $"No se puede Iniciar la Partida, La solicitud no se ha aceptado");

            if (string.IsNullOrEmpty(solicitud.PlayerId) || string.IsNullOrEmpty(solicitud.AcceptedPlayerId))
                throw ClientException.CreateException(ClientExceptionType.InvalidFieldValue,
                    nameof(request.RequestId), GetType(), $"No se puede Iniciar la Partida, Algo salio Mal: {request.RequestId}");
            
            


            //seteo constructor
            Blackjack blackjack = new Blackjack(null, request.RequestId, new List<ManoJugador>()
                {
                    new ManoJugador(solicitud.PlayerId, new List<Card>(), StatusHand.INIT),
                    new ManoJugador(solicitud.AcceptedPlayerId, new List<Card>(), StatusHand.INIT)
                },
                GameStatus.Started);

            DeckOfCards baraja = await _repoBlackJack.GetDeckOfCardsAsync();
            
            //mando a generar el nuevo mazo
            await AddMazoHelper.AgregarCartas(CardHelper.BarajearCartas(baraja, blackjack.Id), _repoBlackJack);

            

            //crea el registro en la BD
            await _repoBlackJack.AddHistoryBlackJackAsync(new HistoryBlackJack(null, null, 1, "Iniciando el Juego", blackjack.Id));
            await _repoBlackJack.CreateBlackJackAsync(blackjack);
            await _repoBlackJack.SaveChangesAsync();

            return blackjack;
        }        
    }

}
