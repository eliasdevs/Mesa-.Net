using Mesa.BlackJack.Model;
using Mesa_SV.VoDeJuegos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.BlackJack.Data.Mapping
{
    public class HistoryBlackJackMap : IEntityTypeConfiguration<HistoryBlackJack>
    {
        public void Configure(EntityTypeBuilder<HistoryBlackJack> builder)
        {
            builder.ToTable("BlackJack_History");

            builder.HasKey(p => p.BlackJackId);

            builder.Property(p => p.BlackJackId).HasMaxLength(50);

            builder.OwnsMany(c => c.PlayerHand, p =>
            {
                p.ToTable("BlackJack_History_Player");
                p.WithOwner().HasForeignKey("HistoryBlackJackVoId");

            });

            builder.Property(p => p.IdJugador);

            builder.Property(p => p.contadorMazo)
            .HasMaxLength(10).IsRequired();

            builder.Property(p => p.Logger)
            .HasMaxLength(500).IsRequired();
        }
    }
}
