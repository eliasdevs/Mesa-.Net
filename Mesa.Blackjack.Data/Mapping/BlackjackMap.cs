using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mesa.Blackjack.Data.Mapping
{
    internal class BlackjackMap : IEntityTypeConfiguration<Blackjack>
    {
        public void Configure(EntityTypeBuilder<Blackjack> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(50);

            builder.OwnsMany(x => x.ManoJugadores, u =>
            {
                u.Property(y => y.IdJugador).HasMaxLength(50);
                
                u.OwnsMany(y => y.Mano, p =>
                {   
                    p.ToTable("BlackJack_Active_Hand");
                });
            });


            builder.OwnsMany(x => x.Mazo, m =>
            {
                m.ToTable("BlackJack_Mazo");
            });

            builder.OwnsMany(x => x.History, h =>
            {
                h.ToTable("BlackJack_History");

                h.HasKey(p => p.Id);
                h.Property(p => p.Id).HasMaxLength(50);

                h.OwnsMany(c => c.PlayerHand, p =>
                {
                    p.ToTable("BlackJack_History_Player");
                    p.WithOwner().HasForeignKey("HistoryBlackJackVoId");

                });

                h.Property(p => p.IdJugador);

                h.Property(p => p.contadorMazo)
                .HasMaxLength(10).IsRequired();

                h.Property(p => p.Logger)
                .HasMaxLength(500).IsRequired();

            });
        }
    }
}
