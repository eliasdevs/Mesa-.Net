using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mesa.Blackjack.Data.Mapping
{
    internal class BlackjackMap : IEntityTypeConfiguration<Blackjack>
    {
        public void Configure(EntityTypeBuilder<Blackjack> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.UserRetador, u =>
            {
                u.Property(y => y.IdJugador).HasMaxLength(50);
                u.OwnsMany(y => y.Mano, p =>
                {
                    p.ToTable("BlackJack_Ret_hand");
                    p.WithOwner().HasForeignKey("UserEmparejadoId");
                });
            });

            builder.OwnsOne(x => x.UserEmparejado, u =>
            {
                u.Property(y => y.IdJugador).HasMaxLength(50);
                u.OwnsMany(y => y.Mano, p =>
                {
                    p.ToTable("BlackJack_Emp_hand");
                    p.WithOwner().HasForeignKey("UserEmparejadoId");
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

                h.OwnsMany(c => c.PlayerHand, p =>
                {
                    p.ToTable("BlackJack_History_Player");
                    p.WithOwner().HasForeignKey("HistoryBlackJackVoId");

                });

                h.Property(p => p.IdJugador).IsRequired();

                h.Property(p => p.contadorMazo)
                .HasMaxLength(10).IsRequired();

                h.Property(p => p.Logger)
                .HasMaxLength(500).IsRequired();

            });
        }
    }
}
