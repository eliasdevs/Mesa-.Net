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

            builder.HasKey(p => new { p.Id, p.BlackJackId});

            builder.Property(p => p.Id).HasMaxLength(50);

            builder.Property(p => p.BlackJackId).HasMaxLength(50);

            builder.Property(p => p.IdPlayer);

            builder.Property(p => p.ContadorMazo)
            .HasMaxLength(10).IsRequired();

            builder.Property(p => p.Logger)
            .HasMaxLength(500).IsRequired();

            builder.HasIndex(x => x.BlackJackId).HasDatabaseName("IX_HistoryBlackJack_BlackJackId");

        }
    }
}
