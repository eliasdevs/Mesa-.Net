using Mesa_SV;
using Microsoft.EntityFrameworkCore;



namespace Mesa.Blackjack.Data
{
    public class BlackJackContext : DbContext {

        public BlackJackContext(DbContextOptions<BlackJackContext> options) : base(options)
        {


        }
        public DbSet<DeckOfCards> DeckOfCards{ get; set; }
        public DbSet<Backjack> Blackjacks{ get; set; }

        public DbSet<GameRequestBackJack> GameRequests { get; set; }

        public DbSet<Mensaje> Mensajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
