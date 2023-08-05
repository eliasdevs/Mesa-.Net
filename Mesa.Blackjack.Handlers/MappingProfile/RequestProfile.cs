using AutoMapper;
using Mesa_SV.BlackJack.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.MappingProfile
{
    public  class RequestProfile : Profile
    {
        public RequestProfile() {
            CreateMap<GameRequestBackJack, GameRequestBackJackOutput>();
        }
        
    }
}
