using Mesa_SV.VoDeJuegos;
using Mesa_SV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mesa_SV.BlackJack.Model.Barajas;

namespace Mesa.BlackJack
{
    public class ManoJugador
    {

        protected ManoJugador() { }

        public ManoJugador(string idJugador, List<Card> mano, StatusHand estado)
        {
            IdJugador = idJugador;
            Mano = mano;
            this.estado = estado;
        }

        public string IdJugador { get; set; }

        public List<Card> Mano { get; set; }

        public StatusHand estado { get; set; }
    }
}
