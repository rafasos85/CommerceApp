using EcommerceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<ArticuloTienda> ArticuloTiendas { get; set; }
        public DbSet<ClienteArticulo> ClienteArticulos { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de índices únicos
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Articulo>()
                .HasIndex(a => a.Codigo)
                .IsUnique();

            // Configuración de relaciones
            modelBuilder.Entity<ArticuloTienda>()
                .HasOne(at => at.Articulo)
                .WithMany(a => a.ArticuloTiendas)
                .HasForeignKey(at => at.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ArticuloTienda>()
                .HasOne(at => at.Tienda)
                .WithMany(t => t.ArticuloTiendas)
                .HasForeignKey(at => at.TiendaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteArticulo>()
                .HasOne(ca => ca.Cliente)
                .WithMany(c => c.ClienteArticulos)
                .HasForeignKey(ca => ca.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClienteArticulo>()
                .HasOne(ca => ca.Articulo)
                .WithMany(a => a.ClienteArticulos)
                .HasForeignKey(ca => ca.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Cliente)
                .WithMany(cl => cl.Carritos)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarritoItem>()
                .HasOne(ci => ci.Carrito)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CarritoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarritoItem>()
                .HasOne(ci => ci.Articulo)
                .WithMany(a => a.CarritoItems)
                .HasForeignKey(ci => ci.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
