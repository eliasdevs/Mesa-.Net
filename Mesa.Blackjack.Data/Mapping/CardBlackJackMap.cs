using Mesa.BlackJack.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mesa.BlackJack.Data.Mapping
{
    public class CardBlackJackMap : IEntityTypeConfiguration<CardBlackJack>
    {
        public void Configure(EntityTypeBuilder<CardBlackJack> builder)
        {
            builder.ToTable("MazoBlackJack");
            builder.HasKey(p => new { p.CardId, p.BlackJackId});

            builder.Property(p => p.BlackJackId)
            .HasMaxLength(50);            

            builder.Property(y => y.IdJugador).HasMaxLength(50);

            builder.Property(p => p.OriginalValue)
            .HasMaxLength(50);

            builder.Property(p => p.SubValue)
            .HasMaxLength(50);

            builder.Property(p => p.Representation)
            .HasMaxLength(50);

            builder.Property(p => p.TypeOfCardId)
            .HasMaxLength(50);

            builder.HasIndex(x => x.BlackJackId).HasDatabaseName("IX_CardBlackJack_BlackJackId");
        }
    }
}
