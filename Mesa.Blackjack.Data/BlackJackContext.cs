using Mesa.BlackJack.Model;
using Mesa_SV;
using Mesa_SV.BlackJack;
using Microsoft.EntityFrameworkCore;



namespace Mesa.Blackjack.Data
{
    public class BlackJackContext : DbContext {

        public BlackJackContext(DbContextOptions<BlackJackContext> options) : base(options)
        {
        }

        /// <summary>
        /// las cartas globales
        /// </summary>
        public DbSet<DeckOfCards> DeckOfCards{ get; set; }

        /// <summary>
        /// BlackJacks
        /// </summary>
        public DbSet<Blackjack> Blackjacks{ get; set; }

        /// <summary>
        /// Las solicitudes
        /// </summary>
        public DbSet<GameRequestBackJack> GameRequests { get; set; }

        /// <summary>
        /// Representa el mazo del juego
        /// </summary>
        public DbSet<CardBlackJack> CardBlackJack { get; set; }

        /// <summary>
        /// El historial del juego
        /// </summary>
        public DbSet<HistoryBlackJack> HistoryBlackJack { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        
    }
}
