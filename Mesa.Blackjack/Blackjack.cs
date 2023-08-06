
using Mesa_SV;
using Mesa_SV.VoDeJuegos;

namespace Mesa.Blackjack
{
    public class Blackjack
    {
        public Blackjack()
        {
            
        }
        private  Blackjack(Guid? id, Guid idRequest, List<Card> mazo, List<HistoryBlackJackVo>? history, GameStatus status)
        {
            Id = id ?? Guid.NewGuid();
            IdRequest = idRequest;            
            Mazo = mazo;
            ContadorMazo = 1;
            History = history;
            Status = status;
        }


        public Blackjack(Guid? id, Guid idRequest, ManoJugadorVo userRetador, ManoJugadorVo userEmparejado, List<Card> mazo, List<HistoryBlackJackVo>? history, GameStatus status)
        {
            Id = id ?? Guid.NewGuid();
            IdRequest = idRequest;
            UserRetador = userRetador;
            UserEmparejado = userEmparejado;
            Mazo = mazo;
            ContadorMazo = 1;
            History = history;
            Status = status;
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
        /// contiene el id del usuario retador y la mano
        /// </summary>
        public ManoJugadorVo UserRetador { get; set; }

        /// <summary>
        /// id del user acepta el reto y la mano
        /// </summary>
        public ManoJugadorVo UserEmparejado { get; set; }

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