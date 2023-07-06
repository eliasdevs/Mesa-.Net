using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV
{
    public record HistoryBlackJackVo
    {
        public HistoryBlackJackVo(List<Card>? playerOneHand, List<Card>? playerTwoHand, int idMazo, string logger)
        {
            Id = Guid.NewGuid();
            PlayerOneHand = playerOneHand;
            PlayerTwoHand = playerTwoHand;
            IdMazo = idMazo;
            Logger = logger;
        }
        public HistoryBlackJackVo()
        {
            // Constructor sin parámetros requerido por Entity Framework Core
            Id = Guid.NewGuid();
        }


        //como es unVo va tomar id de la Gameplay o de Cualquiera que lo use

        public Guid Id { get; set; }
        /// <summary>
        /// Representa la mano del Jugador 1
        /// </summary>
        public List<Card>? PlayerOneHand { get; set; }

        //Representa la Mano de Jugador 2 una va nula cuando la otra esta vacia
        public List<Card>? PlayerTwoHand { get; set; }
        
        /// <summary>
        /// representa el numero de mazo al que pertenecen ambas manos
        /// este se da cuando se acaban las cartas se reparte otro mazo ya cuenta como mazo 2
        /// y asi sucesivamente
        /// </summary>
        public int IdMazo { get; set; }

        /// <summary>
        /// aqui se registra la actividad del juego
        /// </summary>
        public string Logger{ get; set; }

    }
}
