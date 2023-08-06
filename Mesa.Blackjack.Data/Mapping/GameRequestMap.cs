using Mesa_SV;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mesa.Blackjack.Data.Mapping
{
    internal class GameRequestMap : IEntityTypeConfiguration<GameRequestBackJack>
    {
        public void Configure(EntityTypeBuilder<GameRequestBackJack> builder)
        {
            builder.ToTable("GameRequest");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasMaxLength(50);

            builder.Property(p => p.AcceptedPlayerId);

            builder.Property(p => p.PlayerId);

            builder.OwnsMany(x => x.PlayerInfo, i =>
            {
                i.Property(p => p.IdUser).IsRequired();
                i.Property(p => p.IdContextWS).IsRequired();
            });

            builder.HasOne(d => d.backjack)
                .WithOne()
                .HasForeignKey<Blackjack>(d => d.IdRequest) // Utiliza la expresión lambda para referenciar la propiedad IdRequest
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
