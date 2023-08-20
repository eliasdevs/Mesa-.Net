using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Data;
using Mesa.Blackjack.Handlers.Helper;
using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack;
using Mesa_SV.Exceptions;
using Pisto.Exceptions;
using System.Net;

namespace Mesa.Blackjack.Handlers.Commands
{
    public class ShuffleDeckHandler : IRequestHandler<ShuffleDeck, HttpResponseMessage>
    {

        private readonly IBlackJackRepository _repoBlackJack;

        public ShuffleDeckHandler(IBlackJackRepository repoBlackJack)
        {
            _repoBlackJack = repoBlackJack;
        }
        public async Task<HttpResponseMessage> Handle(ShuffleDeck request, CancellationToken cancellationToken)
        {
            Blackjack? blackjack = await _repoBlackJack.GetBlackjackById(request.BlackJackId);

            if (blackjack == null)
                throw NotFoundException.CreateException(NotFoundExceptionType.BlackJack, nameof(blackjack), GetType(), "No se encontro la partida solicitada");

            //consultar mazo
            List<CardBlackJack> mazo = await _repoBlackJack.GetMazoBlackJackAsync(blackjack.Id);

            //el dos es el margen que se da desde que se puede volver a barajear
            if (!mazo.Any() && blackjack.Status == GameStatus.Started)
            {
                DeckOfCards baraja = await _repoBlackJack.GetDeckOfCardsAsync();

                //mando a generar el nuevo mazo
                await AddMazoHelper.AgregarCartas(CardHelper.BarajearCartas(baraja, blackjack.Id), _repoBlackJack);

                blackjack.ContadorMazo = ++blackjack.ContadorMazo;

                //si han habido cambios los guardamos
                await _repoBlackJack.SaveChangesAsync();

                return GetHttpResponseMessage(true);                
            }

            //retorna un badrequest
            return GetHttpResponseMessage(false);
        }
        
        public HttpResponseMessage GetHttpResponseMessage(bool exitoso)
        {
            if (exitoso)
            {
                // Crear la respuesta con el objeto de respuesta y el código de estado 200 (OK)
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                // Crear la respuesta con el objeto de respuesta y el código de estado 400 (BadRequest)
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }
        }      
    }
}
