using Microsoft.EntityFrameworkCore;
using Sistemas.Datos.Mapping.Almacen;
using Sistemas.Datos.Mapping.Usuarios;
using Sistemas.Entidades.Almacen;
using Sistemas.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistemas.Datos
{
   public class DBContextSistema : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Articulo> Articulos { get; set; }



        public DBContextSistema(DbContextOptions<DBContextSistema> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
     
        }


    }
}
