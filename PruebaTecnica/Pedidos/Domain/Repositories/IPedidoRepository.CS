using PruebaTecnica.Pedidos.Domain.Models;

namespace PruebaTecnica.Pedidos.Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ListAsync();
        Task AddAsync(Pedido pedido);
        Task<Pedido> FindById(int id);
        void Update(Pedido pedido);
        void Remove(Pedido pedido);
        Task RemoveAsync(Pedido pedido);
    }
}
