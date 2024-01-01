using Mesa_SV;

namespace Mesa.BlackJack
{
    /// <summary>
    /// Representa el mazo del BlackJack
    /// </summary>
    public class CardBlackJack
    {
        public CardBlackJack(int originalValue, int subValue, string representation, TypeCard typeOfCardId, string blackJackId)
        {
            BlackJackId = blackJackId;
            Estado = StatusHand.INIT;//estado inicial de todas la cartas del mazo
            OriginalValue = originalValue;
            SubValue = subValue;
            Representation = representation;
            TypeOfCardId = typeOfCardId;
            CardId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// toma el id de la carta
        /// </summary>
        public string CardId { get; set; }

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
        public StatusHand Estado { get; set; }

        /// <summary>
        /// valor original example A:1
        /// </summary>
        public int OriginalValue { get; set; }

        /// <summary>
        /// valor 2 example; A=11
        /// </summary>
        public int SubValue { get; set; }

        /// <summary>
        /// Simbolo de la carta de A,Q,K,J o 2-10
        /// </summary>
        public string Representation { get; set; }

        /// <summary>
        /// guarda el tipo de carta
        /// </summary>
        public TypeCard TypeOfCardId { get; set; }

        public void SetearJugador(string idJugador)
        {
            IdJugador = idJugador;
            Estado = StatusHand.ACTIVE;
        }
    }
}
