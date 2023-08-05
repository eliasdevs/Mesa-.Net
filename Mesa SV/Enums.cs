using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV
{
    /// <summary>
    /// representa el tipo de carta
    /// </summary>
    public enum TypeCard
    {
        Corazon=0,
        Diamantes=1,
        Trebol=2,
        Picas=3
    }

    /// <summary>
    /// representa el typo de backjack amistoso o recompensada
    /// </summary>
    public enum TypeGame
    {
        /// <summary>
        /// partida amistosa, solo dejara 5 amistosas entre jugadores y 5 amistosa con el crupier por día
        /// </summary>
        FRIENDLY=1,

        /// <summary>
        /// CONTRA EL SISTEMA
        /// </summary>
        CRUPIER = 2,

        /// <summary>
        /// partida recompensada
        /// </summary>
        REWARDED= 3
    }

    /// <summary>
    /// muestra el los valores de las fichas o menedas dentro del jeugo
    /// </summary>
    public enum CoinValue
    {
        One = 1,
        Five = 5,
        TwentyFive = 25,
        Fifty = 50,
        OneHundred = 100,
        TwoHundred = 200,
        FiveHundred = 500
    }

    /// <summary>
    /// representa el estado de la solicitud de start Game
    /// </summary>
    public enum GameRequestStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3
    }

    /// <summary>
    /// los estados del blackjack
    /// </summary>
    public enum GameStatus
    {
        Started = 1,
        finalized = 2,
    }

    /// <summary>
    /// va ser el enum de todos los juegos disponibles en la plataforma
    /// </summary>
    public enum EnumGame
    {
        BlackJack = 1
    }
}
