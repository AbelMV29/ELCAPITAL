using ELCAPITAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ELCAPITAL.Models
{
    public class ELCAPITALContext : DbContext
    {
        public ELCAPITALContext(DbContextOptions<ELCAPITALContext> options)
: base(options)
        {
        }
        public DbSet<FormularioRaiz> FormularioRaizs { get; set; }
        public DbSet<FormularioRechazo> FormularioRechazos { get; set; }
        public DbSet<Restriccion> Restriccions { get; set; }
        public DbSet<Solicitud> Solicitudes { get; set; }
        public DbSet<TarjetaDeCredito> TarjetaDeCreditos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuider)
        {
            modelbuider.Entity<Cliente>()
                .HasDiscriminator<string>("TipoCliente")
                .HasValue<PersonaFisica>("PersonaFisica")
                .HasValue<PersonaJuridica>("PersonJuridica");

            modelbuider.Entity<Solicitud>()
                .HasOne(a => a.Cliente)
                .WithMany(b => b.Solicitudes);

            modelbuider.Entity<Producto>()
                .HasDiscriminator<string>("CualProducto")
                .HasValue<Paquete>("Paquete")
                .HasValue<Producto>("Producto");

            modelbuider.Entity<Producto>()
                .HasOne(a => a.Cliente)
                .WithMany(b => b.Productos);

            modelbuider.Entity<Restriccion>()
                .HasOne(a => a.Cliente)
                .WithMany(b => b.Restricciones);

            modelbuider.Entity<FormularioRaiz>()
                .HasOne(a => a.Solicitud)
                .WithOne(b => b.FormularioRaiz)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuider.Entity<FormularioRechazo>()
                .HasOne(a => a.Solicitud)
                .WithOne(b => b.FormularioRechazo)
                .OnDelete(DeleteBehavior.Cascade);

            modelbuider.Entity<Paquete>()
                .HasMany(a => a.TarjetaDeCreditos)
                .WithOne(b => b.Paquete);

        }

        public DbSet<ELCAPITAL.Models.Cliente> Cliente { get; set; }

        public DbSet<ELCAPITAL.Models.Producto> Producto { get; set; }

        public DbSet<ELCAPITAL.Models.PersonaFisica> PersonaFisica { get; set; }

        public DbSet<ELCAPITAL.Models.PersonaJuridica> PersonaJuridica { get; set; }

        public DbSet<ELCAPITAL.Models.Paquete> Paquete { get; set; }

        public DbSet<ELCAPITAL.Models.Prestamo> Prestamo { get; set; }
    }
}
