using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.Modules.BlackJack.Models
{
    internal class AcceptRequest
    {
        /// <summary>
        /// el id que acepta la solicitud
        /// </summary>
        public string PlayerId { get; set; }

        /// <summary>
        /// tentativamnte esta propiedad
        /// </summary>
        public string RequestId { get; set; }
    }
}
