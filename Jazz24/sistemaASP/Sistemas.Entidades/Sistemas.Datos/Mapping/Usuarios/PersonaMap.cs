using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistemas.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistemas.Datos.Mapping.Usuarios
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
         public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona")
              .HasKey(p => p.idpersona);
      
     
    }
}
}
