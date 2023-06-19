﻿using AutoMapper;
using MediatR;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Queries;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.Queries
{
    public class GetCardsHandlers : IRequestHandler<GetCards, List<OutputDtoCard>>
    {
        private readonly IMapper _mapper;
        private readonly IBlackJackRepository _repoBlackJack;
        public GetCardsHandlers(IBlackJackRepository repoBlackJack, IMapper mapper)
        {
            _repoBlackJack = repoBlackJack;
            _mapper = mapper;
        }
        public async Task<List<OutputDtoCard>> Handle(GetCards request, CancellationToken cancellationToken)
        {
            DeckOfCards baraja= await _repoBlackJack.GetDeckOfCardsAsync();

            //hacer aqui un algoritmo de desordenar las barajas a lo random, nunca debe salir una baraja similar
            Random random = new Random();
            int n = baraja.Cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card carta= baraja.Cards[k];
                baraja.Cards[k] = baraja.Cards[n];
                baraja.Cards[n] = carta;
            }

            //remite las cartas de la baraja desordenadas            
            return _mapper.Map<List<OutputDtoCard>>(baraja.Cards);
        }
    }
}
