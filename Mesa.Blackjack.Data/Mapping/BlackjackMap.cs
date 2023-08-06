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
    internal class BlackjackMap : IEntityTypeConfiguration<Blackjack>
    {
        public void Configure(EntityTypeBuilder<Blackjack> builder)
        {
            builder.HasKey(x => x.Id);            

            builder.OwnsOne(x => x.IdUserRetador);

            builder.OwnsOne(x => x.IdUserEmparejado);            

            builder.OwnsMany(x => x.Mazo, m =>
            {
                m.ToTable("BlackJack_Mazo");
            });
            
            builder.OwnsMany(x => x.History, h =>
            {
                h.ToTable("BlackJack_History");
                
                h.HasKey(p=>p.Id);
               
                h.OwnsMany(c => c.PlayerHand, p =>
                {                    
                    p.ToTable("BlackJack_History_PlayerOneHand");
                    p.WithOwner().HasForeignKey("HistoryBlackJackVoId");                    

                });

                h.Property(p => p.IdJugador).IsRequired();

                h.Property(p => p.IdMazo)
                .HasMaxLength(10).IsRequired();

                h.Property(p => p.Logger)
                .HasMaxLength(500).IsRequired();
                
            });
        }
    }
}
