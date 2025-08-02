using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LogicaAccesoDatos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Comun> EnviosComun { get; set; }
        public DbSet<Urgente> EnviosUrgente { get; set; }
        public DbSet<Agencia> Agencias { get; set; }
        public DbSet<SeguimientoEnvio> SeguimientosEnvios { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*Debido a que el VO NombreCompleto es compartido, es necesario configurar aquí
            Note que si el VO no es compartido, alcanza con [ComplexType] en lugar de configurar con FluentAPI
             */
            modelBuilder.Entity<Usuario>().OwnsOne(a => a.NombreCompleto, n =>
            {
                n.Property(p => p.Nombre).HasColumnName("Nombre");
                n.Property(p => p.Apellido).HasColumnName("Apellido");
            });
            modelBuilder.Entity<Agencia>().OwnsOne(a => a.Ubicacion, n =>
            {
                n.Property(p => p.Latitud).HasColumnName("Latitud");
                n.Property(p => p.Longitud).HasColumnName("Longitud");
            });

            // Configuración de la herencia TPH
            modelBuilder.Entity<Envio>()
                .HasDiscriminator<string>("TipoDeEnvio")
                .HasValue<Comun>("Comun")
                .HasValue<Urgente>("Urgente");



            // Configuración de la entidad usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Crea un índice único para el campo Email


            // Configuro many to many de ..., sin propiedad de navegación de genero a ...

            //Configuro el tipo de dato decimal para EF core



            //Relacion multiple entre ... y ...

            // Relación Empleado -> Envios
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Empleado) // Relación uno a muchos
                .WithMany() // El empleado no necesita una colección de envíos
                .HasForeignKey(c => c.EmpleadoId) // Clave foránea en Envios
                .OnDelete(DeleteBehavior.Restrict); // Evita eliminar envíos al borrar un empleado

            // Relación Cliente -> Envios
            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Cliente) // Relación uno a muchos
                .WithMany() // El cliente no necesita una colección de envíos
                .HasForeignKey(c => c.ClienteId) // Clave foránea en Envios
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comentario>()
           .HasOne(c => c.Empleado)
           .WithMany() // No hay propiedad de navegación inversa en Empleado
           .OnDelete(DeleteBehavior.Restrict);








        }


}
}
