using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ListAsync();
        Task<ClienteResponse> SaveAsync(Cliente cliente);
        Task<ClienteResponse> UpdateAsync(int id, Cliente cliente);
        Task<ClienteResponse> DeleteAsync(int id);
    }
}
