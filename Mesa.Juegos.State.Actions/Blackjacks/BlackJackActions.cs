﻿using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.VoDeJuegos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.State.Actions.Blackjacks
{
    #region Solcitud de Juego
    /// <summary>
    /// permite crear una solicitud de juego
    /// </summary>
    /// <param name="PlayerId"></param>
    /// <param name="TipoJuego"></param>
    public record StartCreateRequest(string PlayerId, TypeGame? TipoJuego);

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
    public record EndAcceptRequest(GameRequestBackJackOutput Request);

    /// <summary>
    /// permite iniciar el juego - posiblemente no se ejecute desde blazor sino desde el server signal R
    /// </summary>
    /// <param name="RequestId"></param>
    public record StartCreateBlackJack(string RequestId);

    /// <summary>
    /// cuando finaliza el proceso de crear BlackJack
    /// </summary>
    /// <param name="BlackJackInfo"></param>
    public record EndCreateBlackJack(BlackjackStartOutput BlackJackInfo);

    /// <summary>
    /// consulta todas las solicitudes con estado pending
    /// </summary>
    public record StartGetAllRequest();

    /// <summary>
    /// Finaliza proceso de consulta de request
    /// </summary>
    /// <param name="Requests"></param>
    public record EndGetAllRequest(List<GameRequestBackJackOutput> Requests);

    #endregion

    #region BlackJack Game
    /// <summary>
    /// Permite Pedir una carta
    /// </summary>
    /// <param name="PlayerId">El id del jugador que pide una carta</param>
    /// <param name="BlackJackId">El Id del Juego</param>
    public record StartDrawCard(string PlayerId, string BlackJackId);

    /// <summary>
    /// Finaliza pedir Carta
    /// </summary>
    /// <param name="Mano">Representa la mano del jugador</param>
    public record EndDrawCard(ManoJugadorVo Mano);

    /// <summary>
    /// permite plantarse con la mano actual
    /// </summary>
    /// <param name="BlackJackId"></param>
    /// <param name="PlayerId"></param>
    public record StartStandHand(string BlackJackId, string PlayerId);

    /// <summary>
    /// Finaliza Plantarse
    /// </summary>
    /// <param name="Mano">Representa la mano del jugador</param>
    public record EndStandHand(ManoJugadorVo Mano);

    /// <summary>
    /// Inicia Pide la Mano de Un jugador en especifico
    /// </summary>
    /// <param name="BlackJackId"></param>
    /// <param name="PlayerId"></param>
    public record StartGetActiveHand(string BlackJackId, string PlayerId);

    /// <summary>
    /// finaliza Pide la Mano de Un jugador en especifico
    /// </summary>
    /// <param name="Mano">Representa la mano del jugador</param>
    public record EndGetActiveHand(ManoJugadorVo Mano);
    #endregion

    /// <summary>
    /// manejo de errores
    /// </summary>
    public record OnClearOnError();

}
