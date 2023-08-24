using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Data
{
    public class BlackJackRepsository : IBlackJackRepository
    {
        private readonly BlackJackContext _context;
     
        public BlackJackRepsository(BlackJackContext context)
        {
            _context=context;
        }

        public async Task AddCardAsync(CardBlackJack carta)
        {
            await _context.CardBlackJack.AddAsync(carta);
        }

        public async Task AddHistoryBlackJackAsync(HistoryBlackJack history)
        {
            await _context.HistoryBlackJack.AddAsync(history);
        }

        public async Task CreateBlackJackAsync(Blackjack backjack)
        {
            await _context.Blackjacks.AddAsync(backjack);            
        }

        public async Task<Blackjack?> GetBlackjackById(string blackjackId)
        {
            return await _context.Blackjacks.FirstOrDefaultAsync(x => x.Id == blackjackId);
        }

        public async Task<DeckOfCards> GetDeckOfCardsAsync()
        {
            return await _context.DeckOfCards.Include(deck => deck.Cards).Where(c => c.Id == 1) .FirstAsync();
        }

        public async Task<List<HistoryBlackJack>> GetHistoryBlackJackAsync(string blackJackId)
        {
            return await _context.HistoryBlackJack.Where(x => x.BlackJackId == blackJackId).ToListAsync();
        }

        public async Task<List<CardBlackJack>> GetMazoBlackJackAsync(string blackjackId)
        {
            return await _context.CardBlackJack.Where(x => x.BlackJackId == blackjackId && x.Estado == StatusHand.INIT).Take(10).ToListAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<CardBlackJack>> GetHandActive(string idJugador, string blackJackId)
        {
           return await _context.CardBlackJack.Where(x => x.IdJugador == idJugador  && x.BlackJackId == blackJackId).ToListAsync();
        }
   
    }
}
