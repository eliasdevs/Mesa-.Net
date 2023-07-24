using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa_SV;
using Mesa_SV.Exceptions;
using Pisto.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.Commands
{
    public class AceptedRequestHandler : IRequestHandler<AcceptedRequest, GameRequestBackJack>
    {
        private readonly IGameRequestBackJackRepository _repository;
        public AceptedRequestHandler(IGameRequestBackJackRepository repository)
        {
            _repository = repository;
        }
        public async Task<GameRequestBackJack> Handle(AcceptedRequest request, CancellationToken cancellationToken)
        {
            Guid idRequest = Guid.Empty;

            //verifica si es un Gui Valido
            if (Guid.TryParse(request.RequestId, out Guid guid))
                idRequest = guid;
            else
                throw ClientException.CreateException(ClientExceptionType.InvalidFieldValue,
                    nameof(request.RequestId), GetType(), $"Error este valor no es valido: {request.RequestId}");

            //extrae la request 
            GameRequestBackJack? solicitud = await _repository.GetGameRequestBackJackAsync(idRequest);
            
            if (solicitud == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.Request,
                    nameof(solicitud), GetType(), $"Error!!!, la solicitud no existe.");

            //validar tambien que l id del retador no sea el del que acepta
            if (solicitud.PlayerId == request.UserId)
                throw ClientException.CreateException(ClientExceptionType.InvalidOperation,
                    nameof(solicitud.PlayerId), GetType(), $"No es posible procesar la solicitud. Valor repetido {solicitud.PlayerId}");

            //pone en aceptado el estado de la request
            solicitud.Status = GameRequestStatus.Accepted;

            //id del jugador que acepta la solicitud
            solicitud.AcceptedPlayerId = request.UserId;

            //modifica la solicitud
            await _repository.SaveChangesAsync();

            return solicitud;
        }
    }
}
