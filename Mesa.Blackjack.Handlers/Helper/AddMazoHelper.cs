using Mesa.Blackjack.Data;
using Mesa.BlackJack;
using Mesa_SV;
using Mesa_SV.BlackJack;
using Mesa_SV.BlackJack.Model.Barajas;
using Mesa_SV.VoDeJuegos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Handlers.Helper
{
    internal static class AddMazoHelper
    {
        /// <summary>
        /// Permite agregar cartas al mazo del blackJack
        /// </summary>
        /// <param name="cartas"></param>        
        /// <param name="repository"></param>
        /// <returns></returns>
        public static async Task<List<CardBlackJack>> AgregarCartas(List<CardBlackJack> cartas, IBlackJackRepository repository)
        {
            foreach(var carta in cartas)
            {
                await repository.AddCardAsync(carta);
                await repository.SaveChangesAsync();
            }
            //retorna la nueva lista
            return cartas;
        }
    }
}
