using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Services
{
    public interface IPedidoProductoService
    {
        Task<IEnumerable<PedidoProducto>> ListAsync();
        Task<ProductoPedidoResponse> SaveAsync(PedidoProducto pedidoProducto);
        Task<ProductoPedidoResponse> UpdateAsync(int id, PedidoProducto pedidoProducto);
        Task<ProductoPedidoResponse> DeleteAsync(int id);
    }
}
