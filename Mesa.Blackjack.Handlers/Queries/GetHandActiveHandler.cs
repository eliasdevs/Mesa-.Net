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
    public class GetHandActiveHandler : IRequestHandler<GetHandActive, ManoJugadorVo>
    {
        private readonly IBlackJackRepository _repository;
        public GetHandActiveHandler(IBlackJackRepository repository)
        {
            _repository = repository;
        }
        public async Task<ManoJugadorVo> Handle(GetHandActive request, CancellationToken cancellationToken)
        {
            Blackjack? blackjack = await _repository.GetBlackjackById(request.BackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack,
                    nameof(blackjack), GetType(), $"Error!!!, no se encontro registro de este juego.");

            ManoJugador? datosJugador = blackjack.ManoJugadores?.FirstOrDefault(x => x.IdJugador == request.UserId);

            if (datosJugador == null || blackjack.ManoJugadores == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack.Mazo), GetType(), $"No se encontro registro de este usuario con Id {request.UserId}");

            return new(datosJugador.IdJugador, datosJugador.Mano, datosJugador.estado);
        }
    }
}
