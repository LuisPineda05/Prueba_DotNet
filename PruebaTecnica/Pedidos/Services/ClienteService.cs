using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Pedidos.Domain.Services;
using PruebaTecnica.Pedidos.Domain.Services.Communication;
using PruebaTecnica.Shared.Domain.Repositories;

namespace PruebaTecnica.Pedidos.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Cliente>> ListAsync()
        {
            return await _clienteRepository.ListAsync();
        }

        public async Task<ClienteResponse> SaveAsync(Cliente cliente)
        {
            try
            {
                var existingCliente = await _clienteRepository.FindByEmailAsync(cliente.Correo);

                if (existingCliente != null)
                {
                    return new ClienteResponse($"A client with the email '{cliente.Correo}' already exists.");
                }

                await _clienteRepository.AddAsync(cliente);
                await _unitOfWork.CompleteAsync();

                return new ClienteResponse(cliente);
            }
            catch (Exception e)
            {
                return new ClienteResponse($"An error occurred while saving the client: {e.Message}");
            }
        }


        public async Task<ClienteResponse> UpdateAsync(int id, Cliente cliente)
        {
            var existingCliente = await _clienteRepository.FindById(id);

            if (existingCliente == null)
                return new ClienteResponse("Cliente not found.");

            var clienteConMismoCorreo = await _clienteRepository.FindByEmailAsync(cliente.Correo);

            if (clienteConMismoCorreo != null && clienteConMismoCorreo.Id != id)
                return new ClienteResponse("A client with the same email already exists.");

            existingCliente.Nombre = cliente.Nombre;
            existingCliente.Correo = cliente.Correo;

            try
            {
                _clienteRepository.Update(existingCliente);
                await _unitOfWork.CompleteAsync();

                return new ClienteResponse(existingCliente);
            }
            catch (Exception e)
            {
                return new ClienteResponse($"An error occurred while updating the client: {e.Message}");
            }
        }


        public async Task<ClienteResponse> DeleteAsync(int id)
        {
            var existingCliente = await _clienteRepository.FindById(id);

            if (existingCliente == null)
                return new ClienteResponse("Cliente not found.");

            try
            {
                _clienteRepository.Remove(existingCliente);
                await _unitOfWork.CompleteAsync();

                return new ClienteResponse(existingCliente);
            }
            catch (Exception e)
            {
                return new ClienteResponse($"An error occurred while deleting the client: {e.Message}");
            }
        }
    }
}
