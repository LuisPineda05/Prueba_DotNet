using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Repositories
{
    public interface IProductoPedidoRepository
    {
        Task<IEnumerable<PedidoProducto>> ListAsync();
        Task AddAsync(PedidoProducto productoPedido);
        Task<PedidoProducto> FindById(int id);
        void Update(PedidoProducto productoPedido);
        void Remove(PedidoProducto productoPedido);
        Task<List<PedidoProducto>> ListByPedidoIdAsync(int pedidoId);
    }
}
