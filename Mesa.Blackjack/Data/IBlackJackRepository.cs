using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Data
{
    /// <summary>
    /// Se usa un solo repo por todos los modelos ya que todos componen el BlackJack
    /// </summary>
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
        /// <param name="userId"></param>
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        Task<Blackjack?> GetBlackjackById(string blackjackId);

        /// <summary>
        /// Permite extraer la lista de cartas (mazo del juego) solo va consultar los primeros 10 reguistros
        /// </summary>
        /// <param name="blackjackId"></param>
        /// <returns></returns>
        Task<List<CardBlackJack>> GetMazoBlackJackAsync(string blackjackId);

        /// <summary>
        /// la mano activa del jugador recibe el id del jugador y el blackId
        /// </summary>
        /// <param name="idJugador"></param>
        /// <param name="blackJackId"></param>
        /// <returns></returns>
        Task<List<CardBlackJack>> GetHandActive(string idJugador, string blackJackId);

        /// <summary>
        /// Permite agregar cartas al mazo
        /// </summary>
        /// <returns></returns>
        Task AddCardAsync(CardBlackJack carta);

        /// <summary>
        /// Permite extraer el history por su Id
        /// </summary>
        /// <returns></returns>
        Task<List<HistoryBlackJack>> GetHistoryBlackJackAsync(string blackJackId);

        /// <summary>
        /// Permite agregar un history al BlackJak
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        Task AddHistoryBlackJackAsync(HistoryBlackJack history);

        /// <summary>
        /// Permite eliminar las cartas en el context
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        Task RemoveCardsFromHand(List<CardBlackJack> cards);
    }
}
