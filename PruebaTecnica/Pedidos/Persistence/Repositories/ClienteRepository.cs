using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Shared.Persistence.Context;
using PruebaTecnica.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;


namespace PruebaTecnica.Pedidos.Persistence.Repositories
{
    public class ClienteRepository : BaseRepository, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> ListAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public async Task<Cliente> FindById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public void Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }

        public void Remove(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
        }

        public async Task<Cliente> FindByEmailAsync(string correo)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Correo == correo);
        }

    }
}
