﻿using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.Exceptions;
using Pisto.Exceptions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Mesa.Blackjack.Handlers.Commands
{
    public class StartGameHandler : IRequestHandler<StartGame, List<OutputDtoCard>>
    {
        private readonly IMapper _mapper;
        private readonly IBlackJackRepository _repoBlackJack;
        public StartGameHandler(IBlackJackRepository repoBlackJack, IMapper mapper)
        {
            _mapper = mapper;
            _repoBlackJack = repoBlackJack;
        }

        public async Task<List<OutputDtoCard>> Handle(StartGame request, CancellationToken cancellationToken)
        {
            DeckOfCards baraja = await _repoBlackJack.GetDeckOfCardsAsync();
            Guid foranea = Guid.Empty;
            var listaCartas = getCardRamdom(baraja);

            //TODO: validar que el id de la request que viene exista y que la request este en estado aceptado y que el player que acepta sea diferente de null
            //TODO: pedir ambos id y compararlos con los de la request

            //verifica si es un Gui Valido
            if (Guid.TryParse(request.RequestId, out Guid guid))
                foranea = guid;
            else
                throw ClientException.CreateException(ClientExceptionType.InvalidFieldValue,
                    nameof(request.RequestId), GetType(), $"Error este valor no es valido: {request.RequestId}");

            //llama al metodo de crear blackjack del repo
            Blackjack backjack = new Blackjack();
            backjack.IdRequest = foranea;

            //TODO: consultar el user a la sdk del identity
            backjack.UserIdRetador = new JugadorVo(Guid.NewGuid(),"user1");
            backjack.UserIdEmpareja= new JugadorVo(Guid.NewGuid(), "retador");
            
            //el amzo se manda sin id de carta xq lo asigna ef al persistir la DB
            backjack.Mazo = listaCartas;

            List<HistoryBlackJackVo> listHistory= new List<HistoryBlackJackVo>() 
            {
                new HistoryBlackJackVo(null, null, 1, "Iniciando el Juego")
            };

            //setea el objeto  history de backjack
            backjack.History = listHistory;

            //crea el registro en la BD
            await _repoBlackJack.CreateBlackJackAsync(backjack);
            await _repoBlackJack.SaveChangesAsync();

            //todo modificar que retorne las dos manos de los dos jugadores
            return _mapper.Map<List<OutputDtoCard>>(backjack.Mazo);
        }

        private List<Card> getCardRamdom(DeckOfCards baraja)
        {
            List<Card> nuevaLista= new List<Card>();

            //Desordena las barajas de la BD para asignarlas a un mazo
            Random random = new Random();
            int n = baraja.Cards.Count;
            while (n > 1)
            {   n--;
                int k = random.Next(n + 1);
                Card carta = baraja.Cards[k];
                baraja.Cards[k] = baraja.Cards[n];
                baraja.Cards[n] = carta;                
            }

            //crea una lista sin Id para asignarlo a mazo de backajack
            foreach(var lista in baraja.Cards)
            {
                //el id lo asigna ef al momento de gaurdarlo en la Db
                nuevaLista.Add(new Card(lista.OriginalValue, lista.SubValue, lista.Representation, lista.TypeOfCardId));
            }
            
            //retorna la nueva lista
            return nuevaLista;

        }
    }

}
