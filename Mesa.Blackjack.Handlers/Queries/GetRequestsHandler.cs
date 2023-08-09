using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Queries;
using Mesa.BlackJack;
using Mesa_SV;
using Mesa_SV.Exceptions;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.Queries
{
    /// <summary>
    /// pedir carta
    /// </summary>
    public class GetRequestsHandler : IRequestHandler<GetRequests, List<GameRequestBackJack>>
    {
        private readonly IGameRequestBackJackRepository _repositoryRequest;
        public GetRequestsHandler(IGameRequestBackJackRepository repository)
        {
            _repositoryRequest = repository;
        }

        public async Task<List<GameRequestBackJack>> Handle(GetRequests request, CancellationToken cancellationToken)
        {
            List<GameRequestBackJack> solicitudes = await _repositoryRequest.GetGameRequestsBackJackAsync();
            return solicitudes;
        }
    }
}
