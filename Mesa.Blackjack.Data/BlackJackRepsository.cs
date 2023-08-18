using Mesa_SV;
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

        public async Task CreateBlackJackAsync(Blackjack backjack)
        {
            await _context.Blackjacks.AddAsync(backjack);            
        }

        public async Task<Blackjack?> GetBlackjackById(string blackjackId)
        {
            return await _context.Blackjacks.Include(x => x.Mazo).FirstOrDefaultAsync(x => x.Id == blackjackId);
        }

        public async Task<Blackjack?> GetBlackjackByIdWithIncludes(string blackjackId)
        {
            return await _context.Blackjacks                
                .FirstOrDefaultAsync(x => x.Id == blackjackId);
        }


        public async Task<DeckOfCards> GetDeckOfCardsAsync()
        {
            return await _context.DeckOfCards.Include(deck => deck.Cards).Where(c => c.Id == 1) .FirstAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
