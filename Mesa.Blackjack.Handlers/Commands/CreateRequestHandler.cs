using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa_SV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.Commands
{
    public class CreateRequestHandler : IRequestHandler<CreateRequest, GameRequestBackJack>
    {
        private readonly IGameRequestBackJackRepository _repository;
        public CreateRequestHandler(IGameRequestBackJackRepository repository)
        {
            _repository = repository;
        }
        public async Task<GameRequestBackJack> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            //setea el id de la request
            GameRequestBackJack solicitud = new GameRequestBackJack();
            
            //pone en pendiente el estado
            solicitud.Status = GameRequestStatus.Pending;

            //id del jugador que hace la solicitud
            solicitud.PlayerId = request.UserId;

            //agrega la info del retador
            solicitud.PlayerInfo = new List<InfoJugador>()
            {
                new InfoJugador
                { IdContextWS = request.ContextId, IdUser = request.UserId }
            };

            //crea la solicitud del juego
            await _repository.CreateRequestAsync(solicitud);
            await _repository.SaveChangesAsync();

            return solicitud;
        }
    }
}
