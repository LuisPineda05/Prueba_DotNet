using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<Producto>> ListAsync();
        Task<Producto> GetByIdAsync(int id);
        Task<ProductoResponse> SaveAsync(Producto producto);
        Task<ProductoResponse> UpdateAsync(int id, Producto producto);
        Task<ProductoResponse> DeleteAsync(int id);
    }
}
