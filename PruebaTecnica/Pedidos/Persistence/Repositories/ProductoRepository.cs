using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Shared.Persistence.Context;
using PruebaTecnica.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;


namespace PruebaTecnica.Pedidos.Persistence.Repositories
{
    public class ProductoRepository : BaseRepository, IProductoRepository
    {
        public ProductoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Producto>> ListAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
        }

        public async Task<Producto> FindById(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public void Update(Producto producto)
        {
            _context.Productos.Update(producto);
        }

        public void Remove(Producto producto)
        {
            _context.Productos.Remove(producto);
        }
        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id); 
        }
    }
}
