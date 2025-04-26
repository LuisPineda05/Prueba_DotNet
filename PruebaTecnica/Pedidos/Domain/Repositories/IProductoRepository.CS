using PruebaTecnica.Pedidos.Domain.Models;

namespace PruebaTecnica.Pedidos.Domain.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ListAsync();
        Task<Producto> GetByIdAsync(int id);
        Task AddAsync(Producto producto);
        Task<Producto> FindById(int id);
        void Update(Producto producto);
        void Remove(Producto producto);
    }
}
