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
    public class GetRequestByIdHandler : IRequestHandler<GetRequestById, GameRequestBackJack>
    {
        private readonly IGameRequestBackJackRepository _repositoryRequest;
        public GetRequestByIdHandler(IGameRequestBackJackRepository repository)
        {
            _repositoryRequest = repository;
        }

        public async Task<GameRequestBackJack> Handle(GetRequestById request, CancellationToken cancellationToken)
        {
            GameRequestBackJack? solicitud= await _repositoryRequest.GetGameRequestBackJackAsync(request.RequestId);

            if (solicitud  == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack,
                    nameof(solicitud), GetType(), $"Error!!!, no se encontro la solicitud.");

            return solicitud;
        }
    }
}
