using Mesa_SV;

namespace Mesa.Blackjack
{
    public class GameRequestBackJack : GameRequestBase
    {
        /// <summary>
        /// este sera el backjack que va pertenecer a la solicitud, permite null, hasta que se acepte la solicitud se setea backjack
        /// </summary>
        public Blackjack? backjack { get; set; }


        /// <summary>
        /// informacion de los jugadores involucrados en el juego
        /// </summary>
        public List<InfoJugador> PlayerInfo { get; set; }
    }
}
