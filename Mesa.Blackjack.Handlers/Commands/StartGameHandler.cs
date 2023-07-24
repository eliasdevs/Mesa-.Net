using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Handlers.Helper;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.Exceptions;
using Pisto.Exceptions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Mesa.Blackjack.Handlers.Commands
{
    public class StartGameHandler : IRequestHandler<StartGame, List<CardOutput>>
    {
        private readonly IMapper _mapper;
        private readonly IBlackJackRepository _repoBlackJack;
        public StartGameHandler(IBlackJackRepository repoBlackJack, IMapper mapper)
        {
            _mapper = mapper;
            _repoBlackJack = repoBlackJack;
        }

        public async Task<List<CardOutput>> Handle(StartGame request, CancellationToken cancellationToken)
        {
            DeckOfCards baraja = await _repoBlackJack.GetDeckOfCardsAsync();
            Guid foranea = Guid.Empty;
            var listaCartas = CardHelper.BarajearCartas(baraja);

            //TODO: validar que el id de la request que viene exista y que la request este en estado aceptado y que el player que acepta sea diferente de null
            
            //verifica si es un Gui Valido
            if (Guid.TryParse(request.RequestId, out Guid guid))
                foranea = guid;
            else
                throw ClientException.CreateException(ClientExceptionType.InvalidFieldValue,
                    nameof(request.RequestId), GetType(), $"Error este valor no es valido: {request.RequestId}");

            //llama al metodo de crear blackjack del repo
            Blackjack backjack = new Blackjack();
            backjack.IdRequest = foranea;
            backjack.ContadorMazo = 1;
            backjack.Status = GameStatus.Started;
            backjack.IdUserRetador = "idretador";
            backjack.IdUserEmparejado = "idaceptareto";
            
            //el amzo se manda sin id de carta xq lo asigna ef al persistir la DB
            backjack.Mazo = listaCartas;

            List<HistoryBlackJackVo> listHistory= new List<HistoryBlackJackVo>() 
            {
                new HistoryBlackJackVo(null, null, backjack.ContadorMazo, "Iniciando el Juego")
            };

            //setea el objeto  history de backjack
            backjack.History = listHistory;

            //crea el registro en la BD
            await _repoBlackJack.CreateBlackJackAsync(backjack);
            await _repoBlackJack.SaveChangesAsync();
                         
            return _mapper.Map<List<CardOutput>>(backjack.Mazo);
        }        
    }

}
