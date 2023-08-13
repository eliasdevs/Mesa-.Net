using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.BlackJack.Dtos.Output
{
    public class GameRequestBackJackOutput
    {
        /// <summary>
        /// el id de la solicitud
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// el id del usuario que hace la solicitud 
        /// </summary>
        public string PlayerId { get; set; }

        /// <summary>
        /// el id del user que acepta la solicitud
        /// </summary>
        public string? AcceptedPlayerId { get; set; }

        /// <summary>
        /// el estado de la solicitud 
        /// </summary>
        public GameRequestStatus Status { get; set; }

        /// <summary>
        /// informacion de los jugadores involucrados en el juego
        /// </summary>
        public List<InfoJugador> PlayerInfo { get; set; }

        /// <summary>
        /// la fecha que se creo
        /// </summary>
        public DateTimeOffset FechaCreacion { get; set; }
    }
}
