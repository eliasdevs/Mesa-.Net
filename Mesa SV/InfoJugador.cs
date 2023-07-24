using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV
{
    /// <summary>
    /// detalles del usuario jugador
    /// </summary>
    public  class InfoJugador
    {
        /// <summary>
        /// este es el id del user en el identity 
        /// </summary>
        public string IdUser { get; set; }

        /// <summary>
        /// representa el id del websocket
        /// </summary>
        public string IdContextWS{ get; set; }

    }    
}
