using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using agenda.Models;

namespace agenda.Data
{
    public class dbCtx : DbContext
    {
        public dbCtx(DbContextOptions<dbCtx> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //crear tablas con nombres deseados
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Contacto>().ToTable("Contacto");
            modelBuilder.Entity<Telefono>().ToTable("Telefono");
        }
    }
}
