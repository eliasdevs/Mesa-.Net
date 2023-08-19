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

        public Task AddCardAsync(CardBlackJack carta)
        {
            throw new NotImplementedException();
        }

        public Task AddHistoryBlackJackAsync(HistoryBlackJack history)
        {
            throw new NotImplementedException();
        }

        public async Task CreateBlackJackAsync(Blackjack backjack)
        {
            await _context.Blackjacks.AddAsync(backjack);            
        }

        public async Task<Blackjack?> GetBlackjackById(string blackjackId)
        {
            return await _context.Blackjacks.FirstOrDefaultAsync(x => x.Id == blackjackId);
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

        public Task<List<HistoryBlackJack>> GetHistoryBlackJackAsync(string blackJackId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CardBlackJack>> GetMazoBlackJackAsync(string blackjackId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        Task<DeckOfCards> IBlackJackRepository.GetDeckOfCardsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
