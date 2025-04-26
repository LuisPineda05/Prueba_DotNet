using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Pedidos.Domain.Services.Communication;
using PruebaTecnica.Shared.Persistence.Context;
using PruebaTecnica.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;


namespace PruebaTecnica.Pedidos.Persistence.Repositories
{
    public class PedidoProductoRepository : BaseRepository, IProductoPedidoRepository
    {
        public PedidoProductoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PedidoProducto>> ListAsync()
        {
            return await _context.PedidoProductos.ToListAsync();
        }

        public async Task AddAsync(PedidoProducto pedidoProducto)
        {
            await _context.PedidoProductos.AddAsync(pedidoProducto);
        }

        public async Task<PedidoProducto> FindById(int id)
        {
            return await _context.PedidoProductos.FindAsync(id);
        }

        public void Update(PedidoProducto pedidoProducto)
        {
            _context.PedidoProductos.Update(pedidoProducto);
        }

        public void Remove(PedidoProducto pedidoProducto)
        {
            _context.PedidoProductos.Remove(pedidoProducto);
        }

        public async Task<List<PedidoProducto>> ListByPedidoIdAsync(int pedidoId)
        {
            return await _context.PedidoProductos
                                 .Where(p => p.PedidoId == pedidoId)
                                 .Include(p => p.ProductoId)  
                                 .ToListAsync();
        }
    }
}

