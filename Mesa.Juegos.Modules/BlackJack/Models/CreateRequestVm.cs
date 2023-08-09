using Mesa_SV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Juegos.Modules.BlackJack.Models
{
    public class CreateRequestVm
    {
        /// <summary>
        /// tipo de juego
        /// </summary>
        public TypeGame TypeGame { get; set; }

        /// <summary>
        /// este es el Id del jugador
        /// </summary>
        public string PlayerId { get; set; }
    }
}
