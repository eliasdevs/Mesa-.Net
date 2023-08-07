    using AutoMapper;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.MappingProfile
{
    public class CardProfile:Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardOutput>().ConstructUsing((card, automapper) =>
            {
                return new CardOutput(
                    card.OriginalValue,
                    card.SubValue,
                    card.Representation,
                    card.TypeOfCardId
                    );
            });
        }

    }
}
