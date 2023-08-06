using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.VoDeJuegos
{
    public record HistoryBlackJackVo
    {
        public HistoryBlackJackVo(List<Card>? playerHand, string? iduser, int idMazo, string logger)
        {
            Id = Guid.NewGuid().ToString();
            PlayerHand = playerHand;
            contadorMazo = idMazo;
            Logger = logger;
            IdJugador = iduser;
        }
        public HistoryBlackJackVo()
        {
            // Constructor sin parámetros requerido por Entity Framework Core
            Id = Guid.NewGuid().ToString();
        }


        //como es unVo va tomar id de la Gameplay o de Cualquiera que lo use

        public string Id { get; set; }
        /// <summary>
        /// Representa la mano del Jugador 
        /// </summary>
        public List<Card>? PlayerHand { get; set; }

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
