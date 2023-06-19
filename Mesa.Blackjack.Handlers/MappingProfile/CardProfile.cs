using AutoMapper;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
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
            CreateMap<Card, OutputDtoCard>().ConstructUsing((card, automapper) =>
            {
                return new OutputDtoCard(
                    card.OriginalValue,
                    card.SubValue,
                    card.Representation,
                    card.TypeOfCardId
                    );
            });
        }

    }
}
