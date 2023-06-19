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
    public class MensajesMap : IEntityTypeConfiguration<Mensaje>
    {
        public void Configure(EntityTypeBuilder<Mensaje> b)
        {
            b.ToTable("Mensajes");

            b.HasKey(x => x.Id);

            b.Property(p => p.Id)
             .HasMaxLength(14)
             .IsRequired();

            b.Property(p => p.Remitente)             
             .IsRequired();

            b.Property(p => p.idReceptor)             
             .IsRequired();

            b.Property(p => p.Contenido)
             .HasMaxLength(14)
             .IsRequired();

            b.Property(p => p.FechaEnvio)
            .HasMaxLength(14)
            .IsRequired();
        }
    }
}
