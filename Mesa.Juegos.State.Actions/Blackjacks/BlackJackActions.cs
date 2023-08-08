﻿using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.State.Actions.Blackjacks
{
    /// <summary>
    /// permite crear una solicitud de juego
    /// </summary>
    /// <param name="PlayerId"></param>
    /// <param name="TipoJuego"></param>
    public record StartCreateRequest(string PlayerId, TypeGame TipoJuego);

    /// <summary>
    /// es cuando se ha creado la Request
    /// </summary>
    /// <param name="Request"></param>
    public record EndCreateRequest(GameRequestBackJackOutput Request);

    /// <summary>
    /// permite aceptar la solicitud creado por otro jugador
    /// </summary>
    /// <param name="PlayerId"></param>
    /// <param name="IdRequest"></param>
    public record StartAcceptRequest(string PlayerId, string IdRequest);

    /// <summary>
    /// cuando se acepta la solicitud
    /// </summary>
    /// <param name="RequestId"></param>
    public record EndAcceptRequest(GameRequestBackJackOutput RequestId);

    /// <summary>
    /// permite iniciar el juego
    /// </summary>
    /// <param name="RequestId"></param>
    public record StartCreateBlackJack(string RequestId);

    /// <summary>
    /// cuando finaliza el proceso de crear BlackJack
    /// </summary>
    /// <param name="BlackJack"></param>
    public record EndCreateBlackJack(BlackjackStartOutput BlackJack);
}