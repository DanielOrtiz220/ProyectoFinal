using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions <AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Eventos> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.nombre).HasMaxLength(50);
                tb.Property(col => col.correo).HasMaxLength(50);
                tb.Property(col => col.clave).HasMaxLength(50);
            });
            modelBuilder.Entity<Usuario>().ToTable("Usuario");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Eventos>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.Nombre_Completo).HasMaxLength(50);
                tb.Property(col => col.Correo).HasMaxLength(50);
                tb.Property(col => col.Telefono).HasMaxLength(10);
                tb.Property(col => col.DateOnly);
                tb.Property(col => col.CantidadInvitados).HasMaxLength(100);
            });
            modelBuilder.Entity<Eventos>().ToTable("Eventos");

        }

    }
}
