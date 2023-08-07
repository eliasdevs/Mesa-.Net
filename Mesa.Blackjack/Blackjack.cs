
using Mesa.BlackJack;
using Mesa_SV;
using Mesa_SV.VoDeJuegos;

namespace Mesa.Blackjack
{
    public class Blackjack
    {
        public Blackjack()
        {
            
        }

        public Blackjack(string? id, string idRequest, List<ManoJugador> manoJugadores, List<Card> mazo, List<HistoryBlackJackVo>? history, GameStatus status)
        {
            Id = id ?? Guid.NewGuid().ToString();
            IdRequest = idRequest;
            ManoJugadores = manoJugadores;
            Mazo = mazo;
            ContadorMazo = 1;
            History = history;
            Status = status;
        }


        /// <summary>
        /// id de la partida blackjack
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// foranea de request
        /// </summary>
        public string IdRequest { get; set; }

        /// <summary>
        /// id del user acepta el reto y la mano
        /// </summary>
        public List<ManoJugador> ManoJugadores { get; set; }

        /// <summary>
        /// este representa el mazo que se les carga a los users en todo el juego
        /// </summary>
        public List<Card> Mazo { get; set; }

        /// <summary>
        /// este se actualiza cada que se crea un nuevo mazo
        /// es un contador 
        /// </summary>
        public int ContadorMazo { get; set; } 

        //agrega a la BD todo el historial del juego
        public List<HistoryBlackJackVo>? History { get; set; }

        /// <summary>
        /// indican el estado del juego
        /// </summary>
        public GameStatus Status { get; set; }

    }
}