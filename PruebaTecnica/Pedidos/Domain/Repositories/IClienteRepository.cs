using PruebaTecnica.Pedidos.Domain.Models;

namespace PruebaTecnica.Pedidos.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListAsync();
        Task AddAsync(Cliente cliente);
        Task<Cliente> FindById(int id);
        void Update(Cliente cliente);
        void Remove(Cliente cliente);
        Task<Cliente> FindByEmailAsync(string correo);
    }
}
