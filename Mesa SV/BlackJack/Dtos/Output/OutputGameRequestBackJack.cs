using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.BlackJack.Dtos.Output
{
    internal class OutputGameRequestBackJack
    {
        public OutputGameRequestBackJack(Guid id, string playerId, string? acceptedPlayerId, GameRequestStatus status)
        {
            Id = id;
            PlayerId = playerId;
            AcceptedPlayerId = acceptedPlayerId;
            Status = status;
        }

        /// <summary>
        /// el id de la solicitud
        /// </summary>
        public Guid Id { get; private set; }

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
        
    }
}
