using Mesa_SV;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Data
{
    public class GameRequestBackJackRepository : IGameRequestBackJackRepository
    {
        private readonly BlackJackContext _context;

        public GameRequestBackJackRepository(BlackJackContext context)
        {
            _context = context;
        }

        public async Task CreateRequestAsync(GameRequestBackJack request)
        {
            await _context.GameRequests.AddAsync(request);
        }

        public async Task<GameRequestBackJack?> GetGameRequestBackJackAsync(string id)
        {
            return await _context.GameRequests.FindAsync(id);
        }

        public async Task<List<GameRequestBackJack>> GetGameRequestsBackJackAsync()
        {
            return await _context.GameRequests.Where(x=> x.Status == GameRequestStatus.Pending).OrderBy(x => x.CreacionDate).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
