using Mesa_SV;
using Mesa_SV.VoDeJuegos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.Helper
{
    internal static class CardHelper
    {
        /// <summary>
        /// permite barajear un mazo nuevo
        /// </summary>
        /// <param name="baraja"></param>
        /// <returns></returns>
        public static List<Card> BarajearCartas(DeckOfCards baraja)
        {
            List<Card> nuevaLista = new List<Card>();

            //Desordena las barajas de la BD para asignarlas a un mazo
            Random random = new Random();
            int n = baraja.Cards.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Card carta = baraja.Cards[k];
                baraja.Cards[k] = baraja.Cards[n];
                baraja.Cards[n] = carta;
            }

            //crea una lista sin Id para asignarlo a mazo de backajack
            foreach (var lista in baraja.Cards)
            {
                //el id lo asigna ef al momento de gaurdarlo en la Db
                nuevaLista.Add(new Card(lista.OriginalValue, lista.SubValue, lista.Representation, lista.TypeOfCardId));
            }

            //retorna la nueva lista
            return nuevaLista;
        }
    }
}
