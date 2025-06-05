using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Net.Sockets;

namespace ProyectoFinal.Models
{
    public class TicketsDbContext : DbContext
    {
        public TicketsDbContext() : base("name=ProyectoFinalConnectionString")
        {
        }

        // DbSets (representa cada tabla en la base de datos)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DetalleUsuario> DetallesUsuario { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<HistorialEstado> HistorialEstados { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Adjunto> Adjuntos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }

        // Configuración de las relaciones (si no se usa convención de EF, se puede hacer explícitamente)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Usuario - DetalleUsuario (1 a 1)
            modelBuilder.Entity<Usuario>()
                .HasRequired(u => u.DetalleUsuario)
                .WithRequiredPrincipal(d => d.Usuario);

            // Relación Ticket - Categoria (muchos a 1)
            modelBuilder.Entity<Ticket>()
                .HasRequired(t => t.Categoria)
                .WithMany()
                .HasForeignKey(t => t.Id_Categoria);

            // Relación Ticket - HistorialEstado (1 a muchos)
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.HistorialEstados)
                .WithRequired(h => h.Ticket)
                .HasForeignKey(h => h.Id_Ticket);

            // Relación Ticket - Comentario (1 a muchos)
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Comentarios)
                .WithRequired(c => c.Ticket)
                .HasForeignKey(c => c.Id_Ticket);

            // Relación Ticket - Notificacion (1 a muchos)
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Notificaciones)
                .WithRequired(n => n.Ticket)
                .HasForeignKey(n => n.Id_Ticket);

            // Relación Usuario - Notificacion (1 a muchos)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Notificaciones)
                .WithRequired(n => n.Usuario)
                .HasForeignKey(n => n.Id_Usuario);

            // Relación Ticket - Adjunto (1 a muchos)
            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Adjuntos)
                .WithRequired(a => a.Ticket)
                .HasForeignKey(a => a.Id_Ticket);

            // Relación UsuarioRol - Usuario (muchos a 1)
            modelBuilder.Entity<UsuarioRol>()
                .HasRequired(ur => ur.Usuario)
                .WithMany()
                .HasForeignKey(ur => ur.Id_Usuario);

            // Relación UsuarioRol - Rol (muchos a 1)
            modelBuilder.Entity<UsuarioRol>()
                .HasRequired(ur => ur.Rol)
                .WithMany()
                .HasForeignKey(ur => ur.Id_Rol);

            // Relación Tecnico - Ticket (muchos a 1)
            modelBuilder.Entity<Tecnico>()
                .HasRequired(t => t.Ticket)
                .WithMany()
                .HasForeignKey(t => t.Id_Ticket);

            // Configuración de claves compuestas si se necesitan
            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.Id_Usuario, ur.Id_Rol });

            // Configuración adicional si fuera necesario, por ejemplo, índices, etc.
        }
    }
}
