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
    internal class BackjackMap : IEntityTypeConfiguration<Backjack>
    {
        public void Configure(EntityTypeBuilder<Backjack> builder)
        {
            builder.HasKey(x => x.Id);            

            builder.OwnsOne(x => x.UserIdRetador, u =>
            {
                u.ToTable("PlayerOneHand");

            });

            builder.OwnsOne(x => x.UserIdEmpareja, u => {

                u.ToTable("PlayerTwoHand");                
            }); 

            builder.OwnsMany(x => x.Mazo, m =>
            {
                m.ToTable("BlackJack_Mazo");
            });
            
            builder.OwnsMany(x => x.History, h =>
            {
                h.ToTable("BlackJack_History");
                
                h.HasKey(p=>p.Id);
               
                h.OwnsMany(c => c.PlayerOneHand, p =>
                {                    
                    p.ToTable("BlackJack_History_PlayerOneHand");
                    p.WithOwner().HasForeignKey("HistoryBlackJackVoId");                    

                });

                h.OwnsMany(c => c.PlayerTwoHand, p =>
                {
                    p.ToTable("BlackJack_History_PlayerTwoHand");
                    p.WithOwner().HasForeignKey("HistoryBlackJackVoId");
                    
                });

                h.Property(p => p.IdMazo)
                .HasMaxLength(10).IsRequired();

                h.Property(p => p.Logger)
                .HasMaxLength(500).IsRequired();
                
            });
        }
    }
}
