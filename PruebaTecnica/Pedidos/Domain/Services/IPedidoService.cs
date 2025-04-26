using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Services.Communication;
using PruebaTecnica.Pedidos.Resources;

namespace PruebaTecnica.Pedidos.Domain.Services
{
    public interface IPedidoService
    {
        Task<PedidoResponse> SaveAsync(Pedido pedido, List<PedidoProducto> productos);
        Task<PedidoResponse> GetByIdAsync(int id);
        Task<IEnumerable<Pedido>> ListAsync();
        Task<PedidoResponse> DeleteAsync(int id);
    }
}
