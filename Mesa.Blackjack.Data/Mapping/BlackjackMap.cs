using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mesa.Blackjack.Data.Mapping
{
    internal class BlackjackMap : IEntityTypeConfiguration<Blackjack>
    {
        public void Configure(EntityTypeBuilder<Blackjack> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id).HasDatabaseName("IX_BlackJack_Id");                

            builder.Property(x => x.Id).HasMaxLength(50);

            builder.Property(x => x.IdRequest).HasMaxLength(50);

        }
    }
}
