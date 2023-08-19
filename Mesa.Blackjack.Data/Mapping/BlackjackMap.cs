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

                u.WithOwner().HasForeignKey("BlackjackId");

                u.OwnsMany(y => y.Mano, p =>
                {   
                    p.ToTable("BlackJack_Active_Hand");
                });
            });           
        }
    }
}
