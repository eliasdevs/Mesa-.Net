using Mesa_SV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Data
{
    public interface IBlackJackRepository
    {
        /// <summary>
        /// metodo para extraer las cartas de la BD
        /// </summary>
        Task<DeckOfCards> GetDeckOfCardsAsync();

        /// <summary>
        /// crea un black jack iniciando el juego
        /// </summary>
        /// <param name="backjack"></param>
        /// <returns></returns>
        Task CreateBlackJackAsync(Blackjack backjack);

        /// <summary>
        /// actualiza los cambios en la BD
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// extrae una partida de backjack
        /// </summary>        
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        Task<Blackjack?> GetBlackjackByIdWithIncludes(string blackjackId);

        /// <summary>
        /// extrae una partida de backjack
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        Task<Blackjack?> GetBlackjackById(string blackjackId);
    }
}
