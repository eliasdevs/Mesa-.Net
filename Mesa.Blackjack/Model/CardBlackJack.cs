using Mesa_SV;
using Mesa_SV.BlackJack.Model.Barajas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.BlackJack.Model
{
    /// <summary>
    /// Representa el mazo del BlackJack
    /// </summary>
    public class CardBlackJack : Card
    {
        public CardBlackJack(int originalValue, int subValue, string representation, TypeCard typeOfCardId, string blackJackId) : base(originalValue, subValue, representation, typeOfCardId)
        {
            BlackJackId = blackJackId;
            estado = StatusHand.INIT;//estado inicial de todas la cartas del mazo
        }

        /// <summary>
        /// este es el id del blackJack
        /// </summary>
        public string BlackJackId { get; set; }

        /// <summary>
        /// Todas las cartas que tengan Id Jugador representan la mano actual del jugador
        /// </summary>
        public string? IdJugador { get; set; }

        /// <summary>
        /// Todas las cartas con estado Init son las no asignadas a jugadores
        /// </summary>
        public StatusHand estado { get; set; }

        public void SetearJugador(string idJugador)
        {
            IdJugador = idJugador;
            estado= StatusHand.ACTIVE;
        }
    }
}
