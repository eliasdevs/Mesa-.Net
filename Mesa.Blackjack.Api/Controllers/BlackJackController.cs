﻿using AutoMapper;
using MediatR;
using Mesa.Blackjack.Commands;
using Mesa.Blackjack.Queries;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mesa.Blackjack.Api.Controllers
{
    [Route("api/[controller]")]
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
        [ProducesResponseType(typeof(List<CardOutput>), 200)]
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
        public async Task<ActionResult<List<CardOutput>>> Post(string requestId)
        {
            Mesa.Blackjack.Commands.StartGame cmd = new Commands.StartGame(requestId);

            var response = await _mediator.Send(cmd);

            return response;

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
        public async Task<ActionResult<GameRequestBackJack>> CreateRequest([FromRoute] string playerId, [FromQuery] string contextId, EnumGame tipoJuego)
        {
            //proceso para crear una solicitud
            CreateRequest cmd = new CreateRequest(playerId, contextId, tipoJuego);

            var response = await _mediator.Send(cmd);

            return response;
        }

        /// <summary>
        /// recibe el id de la solicitud y el id del user que acepta la partida
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="playerId"></param>
        /// <param name="contextId"></param>
        /// <returns></returns>
        [HttpPut("users/{playerId}/request/{requestId}/accept")]
        public async Task<ActionResult<GameRequestBackJack>> AcceptRequest([FromRoute] string playerId, [FromRoute] string requestId, [FromQuery] string contextId)
        {
            //proceso para aceptar una solicitud
            AcceptedRequest cmd = new AcceptedRequest(playerId, requestId, contextId);

            var response = await _mediator.Send(cmd);

            return response;
        }

        /// <summary>
        /// permite extraer una carta por id la extrae de la db
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="backjackId"></param>
        /// <returns></returns>
        [HttpGet("{backjackId}/users/{playerId}/draw_card")]
        public async Task<ActionResult<CardOutput>> GetCardById([FromRoute] string playerId, [FromRoute] string backjackId)
        {
            DrawCardById query = new DrawCardById(playerId, backjackId);

            var response = await _mediator.Send(query);

            return _mapper.Map<CardOutput>(response);
        }

        /// <summary>
        /// este metodo permite barajear las cartas del juego
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpGet("{backjackId}/users/{playerId}/")]
        public async Task<ActionResult<HttpResponseMessage>> BarajearCartas([FromRoute] string playerId)
        {
            ShuffleDeck cmd = new ShuffleDeck(playerId);

            var response =await _mediator.Send(cmd);

            return response;
        }

    }
}
