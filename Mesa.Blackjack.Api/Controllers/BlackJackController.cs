using MediatR;
using Mesa.Blackjack.Commands;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mesa.Blackjack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlackJackController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        private static readonly Dictionary<string, WebSocket> _clientSockets = new Dictionary<string, WebSocket>();
        public BlackJackController(IMediator mediator)
        {
            _mediator = mediator;
        
        }

        // GET: api/<BlackJackController>
        [HttpGet]
        public async Task<ActionResult<List<OutputDtoCard>>> Get()
        {
            Mesa.Blackjack.Queries.GetCards query = new Queries.GetCards();

            var response = await _mediator.Send(query);

            return response;
        }

        /// <summary>
        /// se encarga de extraer los valores de un enum en un SelectListItem
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetSelectListItem()
        {
            IEnumerable<SelectListItem> coinvalue = Enum.GetValues(typeof(CoinValue)).Cast<CoinValue>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                });

            return coinvalue;
        }

        /// <summary>
        /// extrae y muestra los datos de una moneda valida en el juego
        /// </summary>
        /// <returns></returns>
        [HttpGet("Coin")]
        public IEnumerable<SelectListItem> GetCoin()
        {
            return GetSelectListItem();
            
        }

        // POST api/<BlackJackController>
        /// <summary>
        /// este metodo sirve para iniciar el blackJack
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("StartGame")]
        public async Task<ActionResult<List<OutputDtoCard>>>  Post(string id)
        {
            Mesa.Blackjack.Commands.StartGame cmd = new Commands.StartGame(id);

            var response = await _mediator.Send(cmd);

            return response;

        }
        /// <summary>
        /// crea el una solicitud de blackJack
        /// recibe el id del usuario que hace la solicitud
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpPost] // Asegúrate de tener el atributo HttpPost aquí
        [Route("create-request")]
        public async Task<ActionResult<GameRequestBackJack>> CreateRequest([FromQuery] string playerId)
        {
            //proceso para crear una solicitud
            CreateRequest cmd = new CreateRequest(playerId);

            var response = await _mediator.Send(cmd);

            return response;
        }

        /// <summary>
        /// recibe el id de la solicitud y el id del user que acepta la partida
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpPut("accept-request")]
        public async Task<ActionResult<GameRequestBackJack>> AcceptRequest(string requestId, string playerId)
        {
            //proceso para aceptar una solicitud
            AceptedRequest cmd = new AceptedRequest(playerId,requestId);
            
            var response = await _mediator.Send(cmd);

            return response;
        }

        // PUT api/<BlackJackController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlackJackController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    //    [HttpGet("/chat")]
    //    public async Task Chat()
    //    {
    //        if (HttpContext.WebSockets.IsWebSocketRequest)
    //        {
    //            WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
    //            string clientId = "";// Obtiene el identificador del cliente
    //            _clientSockets[clientId] = webSocket;

    //            await ReceiveMessages(clientId, webSocket);
    //        }
    //        else
    //        {
    //            HttpContext.Response.StatusCode = 400;
    //        }
    //    }

    //    private async Task ReceiveMessages(string clientId, WebSocket webSocket)
    //    {
    //        while (webSocket.State == WebSocketState.Open)
    //        {
    //            var buffer = new ArraySegment<byte>(new byte[4096]);
    //            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

    //            if (result.MessageType == WebSocketMessageType.Text)
    //            {
    //                string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

    //                // Procesar el mensaje y actualizar la base de datos

    //                // Obtener la conexión WebSocket del cliente B
    //                string clientIdB = "";// Obtener el identificador del cliente B
    //        if (_clientSockets.TryGetValue(clientIdB, out WebSocket webSocketB))
    //                {
    //                    // Enviar el mensaje actualizado a B
    //                    await webSocketB.SendAsync(buffer, result.MessageType, result.EndOfMessage, CancellationToken.None);
    //                }
    //            }
    //        }

    //        // Cuando se cierra la conexión WebSocket, eliminarla del diccionario
    //        _clientSockets.Remove(clientId);
    //    }
    }
}
