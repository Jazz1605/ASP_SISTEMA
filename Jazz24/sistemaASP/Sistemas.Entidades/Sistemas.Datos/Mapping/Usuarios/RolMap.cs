using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistemas.Entidades.Usuarios;

namespace Sistemas.Datos.Mapping.Usuarios
{
    public class RolMap : IEntityTypeConfiguration<Rol>

    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("rol")
                  .HasKey(r => r.idrol);
           

        }
    }
}