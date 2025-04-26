using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Pedidos.Domain.Models;

namespace PruebaTecnica.Shared.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProducto> PedidoProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PedidoProducto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProductoId });  // Definir la clave primaria compuesta

            modelBuilder.Entity<PedidoProducto>()
                .HasOne(pp => pp.Pedido)  // Relación con Pedido
                .WithMany(p => p.Productos)  // Relación inversa en Pedido
                .HasForeignKey(pp => pp.PedidoId)  // Definir la clave foránea
                .OnDelete(DeleteBehavior.Cascade);  // Comportamiento en la eliminación

            modelBuilder.Entity<PedidoProducto>()
                .HasOne(pp => pp.Producto)  // Relación con Producto
                .WithMany()  // Relación inversa con Producto no se necesita
                .HasForeignKey(pp => pp.ProductoId);  // Definir la clave foránea

            modelBuilder.Entity<Pedido>()
                .HasOne<Cliente>()
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
