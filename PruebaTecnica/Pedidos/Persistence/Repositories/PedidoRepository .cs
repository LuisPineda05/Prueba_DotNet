using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Shared.Persistence.Context;
using PruebaTecnica.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;


namespace PruebaTecnica.Pedidos.Persistence.Repositories
{
    public class PedidoRepository : BaseRepository, IPedidoRepository
    {
        public PedidoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pedido>> ListAsync()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task AddAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task<Pedido> FindById(int id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public void Update(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }

        public void Remove(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
        }

        public async Task RemoveAsync(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
        }
    }
}
