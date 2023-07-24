
using Mesa_SV;

namespace Mesa.Blackjack
{
    public class Blackjack
    {
        public Blackjack()
        {
            
        }
      
        /// <summary>
        /// id de la partida blackjack
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// foranea de request
        /// </summary>
        public Guid IdRequest { get; set; }

        /// <summary>
        /// contiene el id del usuario retador
        /// </summary>
        public string IdUserRetador{ get; set; }

        //acepta el reto
        public string IdUserEmparejado{ get; set; }

        //este representa el mazo que se les carga a los users
        public List<Card> Mazo { get; set; }

        //agrega a la BD todo el historial del juego
        public List<HistoryBlackJackVo>? History { get; set; }
    }
}