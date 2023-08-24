using AutoMapper;
using Mesa.Blackjack;
using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack.Dtos.Output;
using Mesa_SV.BlackJack.Helper;
using Mesa_SV.Exceptions;
using Mesa_SV.VoDeJuegos;
using Pisto.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.BlackJack.Handlers.Helper
{
    internal static class GetMano
    {
        /// <summary>
        /// sirve para extraer el estado de la mano
        /// </summary>        
        /// <param name="mano"></param>
        /// <param name="_mapper"></param>
        /// <param name="idJugador"></param>
        /// <returns></returns>
        public static ManoJugadorVo GetHandWithStatus(string idJugador, List<CardBlackJack> mano, IMapper _mapper)
        {
            //valido que todas las cartas tengan seteado el jugador
            if(mano.Any(x => x.IdJugador == null))
                throw ClientException.CreateException(ClientExceptionType.InvalidOperation, nameof(mano), mano.GetType(), "No se puede procesar esta solicitud");


            //Si al menos una carta esta en estado stand se pone estatus hand
            StatusHand estado = (mano.Any(x => x.Estado == StatusHand.STAND_HAND)) ? StatusHand.STAND_HAND : StatusHand.ACTIVE;
                        
            return new ManoJugadorVo(idJugador, _mapper.Map<List<CardOutput>>(mano), estado);
        }

        /// <summary>
        /// determina si la mano tiene todas sus cartas en estado stand
        /// </summary>
        /// <param name="mano"></param>
        /// <returns></returns>
        public static bool AllCardsSatatusSatnd(List<CardBlackJack> mano)
        {
            if (mano.Count() == 0)
                return false;

            return mano.All(carta => carta.Estado == StatusHand.STAND_HAND);
        }
        
    }
}
