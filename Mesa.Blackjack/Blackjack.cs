
using Mesa.BlackJack;
using Mesa_SV;
using Mesa_SV.BlackJack.Model.Barajas;
using Mesa_SV.VoDeJuegos;

namespace Mesa.Blackjack
{
    public class Blackjack
    {
        public Blackjack()
        {
            
        }

        public Blackjack(string? id, string idRequest, List<ManoJugador> manoJugadores, GameStatus status)
        {
            Id = id ?? Guid.NewGuid().ToString();
            IdRequest = idRequest;
            ManoJugadores = manoJugadores;            
            ContadorMazo = 1;            
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
        /// este se actualiza cada que se crea un nuevo mazo
        /// es un contador 
        /// </summary>
        public int ContadorMazo { get; set; } 

        /// <summary>
        /// indican el estado del juego
        /// </summary>
        public GameStatus Status { get; set; }


        /// <summary>
        /// este metodo se encarga de verificar si ya estan plantados los dos jugadores y de estarlo,
        /// lo que hara es reiniciar la mano a vacia y poner el estado de la mano del jugador a active        
        /// </summary>
        public void ReiniciarManoJugadoresPlantados()
        {
            if (ManoJugadores.Count(x => x.estado == StatusHand.STAND_HAND) == 2)
            {
                foreach (var jugador in ManoJugadores)
                {
                    jugador.estado = StatusHand.ACTIVE;
                    jugador.Mano = new List<Card>();
                }
            }
        }

        public bool ValidarUser(string userId) 
        {
            return ManoJugadores.Any(y => y.IdJugador == userId);
        }
    }
}