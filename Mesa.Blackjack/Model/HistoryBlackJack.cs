using Mesa_SV.BlackJack.Model.Barajas;

namespace Mesa.BlackJack.Model
{
    public class HistoryBlackJack
    {
        public HistoryBlackJack(string? iduser, int idMazo, string logger, string blackjackId)
        {
            Id = Guid.NewGuid().ToString();            
            contadorMazo = idMazo;
            Logger = logger;
            IdJugador = iduser;
            BlackJackId = blackjackId;
        }
        public HistoryBlackJack()
        {
            // Constructor sin parámetros requerido por Entity Framework Core
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// representa el blackjack
        /// </summary>
        public string BlackJackId { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// el Id del jugador
        /// </summary>
        public string? IdJugador { get; set; }

        /// <summary>
        /// representa el numero de mazo al que pertenecen ambas manos
        /// este se da cuando se acaban las cartas se reparte otro mazo ya cuenta como mazo 2
        /// y asi sucesivamente
        /// </summary>
        public int contadorMazo { get; set; }

        /// <summary>
        /// aqui se registra la actividad del juego
        /// </summary>
        public string Logger { get; set; }

    }
}
