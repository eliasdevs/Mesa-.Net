using Mesa_SV.BlackJack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mesa.Blackjack.Data.Mapping
{
    public class DeckOfCardMap : IEntityTypeConfiguration<DeckOfCards>
    {
        public void Configure(EntityTypeBuilder<DeckOfCards> b)
        {
            b.ToTable("DeckOfCard");

            b.HasKey(x => x.Id);

            b.Property(p => p.Id)
             .HasMaxLength(14)
             .IsRequired();

            b.Property(p => p.Name)
             .HasMaxLength(200)
             .IsRequired();

            b.OwnsMany(p => p.Cards, c =>
            {
                c.ToTable("DeckOfCard_Cards");

                c.Property(p => p.OriginalValue)
                .HasMaxLength(50).IsRequired();

                c.Property(p => p.SubValue)
                .HasMaxLength(50).IsRequired();

                c.Property(p => p.Representation)
                .HasMaxLength(50).IsRequired();

                c.Property(p => p.TypeOfCardId)
                .HasMaxLength(50).IsRequired();

            });
        }
    }
}
