using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Pedidos.Domain.Services;
using PruebaTecnica.Pedidos.Domain.Services.Communication;
using PruebaTecnica.Shared.Domain.Repositories;

namespace PruebaTecnica.Pedidos.Services
{
    public class PedidoProductoService : IPedidoProductoService
    {
        private readonly IProductoPedidoRepository _pedidoProductoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoProductoService(IProductoPedidoRepository pedidoProductoRepository, IUnitOfWork unitOfWork)
        {
            _pedidoProductoRepository = pedidoProductoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PedidoProducto>> ListAsync()
        {
            return await _pedidoProductoRepository.ListAsync();
        }

        public async Task<ProductoPedidoResponse> SaveAsync(PedidoProducto pedidoProducto)
        {
            try
            {
                await _pedidoProductoRepository.AddAsync(pedidoProducto);
                await _unitOfWork.CompleteAsync();

                return new ProductoPedidoResponse(pedidoProducto);
            }
            catch (Exception e)
            {
                return new ProductoPedidoResponse($"An error occurred while saving the order product: {e.Message}");
            }
        }

        public async Task<ProductoPedidoResponse> UpdateAsync(int id, PedidoProducto pedidoProducto)
        {
            var existingPedidoProducto = await _pedidoProductoRepository.FindById(id);

            if (existingPedidoProducto == null)
                return new ProductoPedidoResponse("PedidoProducto not found.");

            existingPedidoProducto.Cantidad = pedidoProducto.Cantidad;

            try
            {
                _pedidoProductoRepository.Update(existingPedidoProducto);
                await _unitOfWork.CompleteAsync();

                return new ProductoPedidoResponse(existingPedidoProducto);
            }
            catch (Exception e)
            {
                return new ProductoPedidoResponse($"An error occurred while updating the order product: {e.Message}");
            }
        }

        public async Task<ProductoPedidoResponse> DeleteAsync(int id)
        {
            var existingPedidoProducto = await _pedidoProductoRepository.FindById(id);

            if (existingPedidoProducto == null)
                return new ProductoPedidoResponse("PedidoProducto not found.");

            try
            {
                _pedidoProductoRepository.Remove(existingPedidoProducto);
                await _unitOfWork.CompleteAsync();

                return new ProductoPedidoResponse(existingPedidoProducto);
            }
            catch (Exception e)
            {
                return new ProductoPedidoResponse($"An error occurred while deleting the order product: {e.Message}");
            }
        }
    }
}
