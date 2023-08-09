using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Queries;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mesa.Blackjack.Api.Controllers
{
    [Route("api/blackjack")]
    [ApiController]
    public class BlackJackController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public BlackJackController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;

        }

        /// <summary>
        /// extrae todas las cartas de la baraja global
        /// </summary>
        /// <returns>aaaa</returns>
        [HttpGet("global_cards")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<List<CardOutput>>> Get()
        {
            Mesa.Blackjack.Queries.GetCards query = new Queries.GetCards();

            var response = await _mediator.Send(query);

            return response;
        }

        /// <summary>
        /// se encarga de extraer los valores de un enum en un SelectListItem
        /// sirve para el valor que van a tener las fichas en el juego
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
        /// extrae y muestra los datos de una moneda válida en el juego
        /// </summary>
        /// <returns></returns>
        [HttpGet("Coin")]
        public IEnumerable<SelectListItem> GetCoin()
        {
            return GetSelectListItem();
        }


        /// <summary>
        /// este metodo sirve para iniciar el blackJack
        /// recibe el id de la request
        /// </summary>
        /// <param name="requestId">representa el id de la solicitud</param>
        [HttpPost("startGame/{requestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<BlackjackStartOutput>> StartBlackJack(string requestId)
        {
            Mesa.Blackjack.Commands.StartGame cmd = new Commands.StartGame(requestId);

            var response = await _mediator.Send(cmd);

            return _mapper.Map<BlackjackStartOutput>(response);
        }

        /// <summary>
        /// permite extraer una solicitud de juego por su Id
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("request/{requestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<GameRequestBackJackOutput>> GetRequest([FromRoute] string requestId)
        {
            //proceso para crear una solicitud
            GetRequestById query = new GetRequestById(requestId);

            var response = await _mediator.Send(query);

            return _mapper.Map<GameRequestBackJackOutput>(response);
        }

        /// <summary>
        /// crea el una solicitud de blackJack
        /// recibe el id del usuario que hace la solicitud
        /// </summary>
        /// <param name="playerId">representa el id del jugador que crea la solicitud</param>
        /// <param name="contextId"></param>
        /// <param name="tipoJuego"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("users/{playerId}/request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<GameRequestBackJackOutput>> CreateRequest([FromRoute] string playerId, [FromQuery] string contextId, [FromQuery] TypeGame tipoJuego)
        {
            //proceso para crear una solicitud
            CreateRequest cmd = new CreateRequest(playerId, contextId, tipoJuego);

            var response = await _mediator.Send(cmd);

            return _mapper.Map<GameRequestBackJackOutput>(response);
        }

        [HttpGet]
        [Route("request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<List<GameRequestBackJackOutput>>> GetAllRequest()
        {
            //proceso para crear una solicitud
            GetRequests query = new GetRequests();

            var response = await _mediator.Send(query);

            return _mapper.Map<List<GameRequestBackJackOutput>>(response);
        }


        /// <summary>
        /// recibe el id de la solicitud y el id del user que acepta la partida
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="playerId"></param>
        /// <param name="contextId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [HttpPut("users/{playerId}/request/{requestId}/accept")]
        public async Task<ActionResult<GameRequestBackJackOutput>> AcceptRequest([FromRoute] string playerId, [FromRoute] string requestId, [FromQuery] string contextId)
        {
            //proceso para aceptar una solicitud
            AcceptedRequest cmd = new AcceptedRequest(playerId, requestId, contextId);

            var response = await _mediator.Send(cmd);

            return _mapper.Map<GameRequestBackJackOutput>(response);
        }

        /// <summary>
        /// permite extraer una carta por id la extrae de la db  - pedir carta
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        [HttpGet("{blackjackId}/users/{playerId}/draw_card")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<CardOutput>> GetCardById([FromRoute] string playerId, [FromRoute] string blackjackId)
        {
            DrawCardById query = new DrawCardById(playerId, blackjackId);

            var response = await _mediator.Send(query);

            return _mapper.Map<CardOutput>(response);
        }

        /// <summary>
        /// este metodo permite barajear las cartas del juego - solo cuando se acaban las cartas de la baraja anterior
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        [HttpPost("{blackjackId}/shuffle/")]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<HttpResponseMessage>> BarajearCartas([FromRoute] string blackjackId)
        {
            ShuffleDeck cmd = new ShuffleDeck(blackjackId);

            var response =await _mediator.Send(cmd);

            return response;
        }        

      
        /// <summary>
        /// este metodo permite plantarse
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpPost("{blackjackId}/users/{playerId}/stand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<ManoJugadorVo>> PlantarBlackJack([FromRoute] string blackjackId , [FromRoute] string playerId)
        {
            StandHand query = new StandHand(blackjackId,playerId);

           var response = await _mediator.Send(query);

            return response;
        }

        /// <summary>
        /// este permite extraer la mano de un jugador 
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{blackjackId}/users/{playerId}/Hand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Mesa_SV.Filter.ApiExceptionResult))]
        public async Task<ActionResult<ManoJugadorVo>> GetActiveHand([FromRoute] string blackjackId, [FromRoute] string playerId)
        {
            GetHandActive query = new GetHandActive(playerId, blackjackId);

            var response = await _mediator.Send(query);

            return response;
        }
    }
}
