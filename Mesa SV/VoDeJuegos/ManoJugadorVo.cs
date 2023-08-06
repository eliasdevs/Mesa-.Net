using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa_SV.VoDeJuegos
{
    /// <summary>
    /// representa datos importantes del juego
    /// </summary>
    /// <param name="IdJugador">Id del jugador</param>
    /// <param name="Mano">Cartas que se le han asignado al jugador</param>
    /// <param name="estado">este es el estado de la mano</param>
    public record ManoJugadorVo(string IdJugador, List<Card>? Mano, StatusHand estado) {

        /// <summary>
        /// constructor sin parametro porque la migracion no se genera sin el 
        /// </summary>
        private ManoJugadorVo() : this(null, null, default)
        {

        }
    }
}
