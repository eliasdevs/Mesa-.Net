using Mesa_SV.VoDeJuegos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.BlackJack.Handlers.Helper
{
    internal static class CalculateManoBlackJack
    {
        public static int CalcularPuntuacion(List<Card> cartas)
        {
            int puntuacion = 0;

            //este cuenta si hay Ases
            int numAses = 0;

            foreach (var carta in cartas)
            {
                //este se da solo para las ases, no pone original sino subvalue
                if (carta.Representation == "A")
                {
                    numAses++;
                    puntuacion += carta.SubValue;
                }
                else
                {
                    //para todas las demas cartas
                    puntuacion += carta.OriginalValue;
                }
            }

            //resta 10 a cada A
            while (numAses > 0 && puntuacion > 21)
            {
                numAses--; //resto una A que aqui ya vale 1 y no 11
                puntuacion -= 10; //como vale 1 le resto 10
            }

            return puntuacion;
        }
    }
}
