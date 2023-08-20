
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

        public Blackjack(string? id, string idRequest, GameStatus status)
        {
            Id = id ?? Guid.NewGuid().ToString();
            IdRequest = idRequest;            
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
        /// este se actualiza cada que se crea un nuevo mazo
        /// es un contador 
        /// </summary>
        public int ContadorMazo { get; set; } 

        /// <summary>
        /// indican el estado del juego
        /// </summary>
        public GameStatus Status { get; set; }
    }
}