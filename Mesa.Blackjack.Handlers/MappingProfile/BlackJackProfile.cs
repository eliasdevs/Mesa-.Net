using AutoMapper;
using Mesa_SV.BlackJack.Dtos.Output;

namespace Mesa.BlackJack.Handlers.MappingProfile
{
    public class BlackJackProfile : Profile
    {
        public BlackJackProfile()
        {

            CreateMap<Blackjack.Blackjack, BlackjackOutput>();
        }
    }
}
