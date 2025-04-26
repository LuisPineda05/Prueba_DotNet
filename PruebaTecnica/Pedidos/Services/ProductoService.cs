using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Pedidos.Domain.Services;
using PruebaTecnica.Pedidos.Domain.Services.Communication;
using PruebaTecnica.Shared.Domain.Repositories;

namespace PruebaTecnica.Pedidos.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductoService(IProductoRepository productoRepository, IUnitOfWork unitOfWork)
        {
            _productoRepository = productoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Producto>> ListAsync()
        {
            return await _productoRepository.ListAsync();
        }

        public async Task<ProductoResponse> SaveAsync(Producto producto)
        {
            try
            {
                await _productoRepository.AddAsync(producto);
                await _unitOfWork.CompleteAsync();

                return new ProductoResponse(producto);
            }
            catch (Exception e)
            {
                return new ProductoResponse($"An error occurred while saving the product: {e.Message}");
            }
        }

        public async Task<ProductoResponse> UpdateAsync(int id, Producto producto)
        {
            var existingProducto = await _productoRepository.FindById(id);

            if (existingProducto == null)
                return new ProductoResponse("Producto not found.");

            existingProducto.Nombre = producto.Nombre;
            existingProducto.Precio = producto.Precio;

            try
            {
                _productoRepository.Update(existingProducto);
                await _unitOfWork.CompleteAsync();

                return new ProductoResponse(existingProducto);
            }
            catch (Exception e)
            {
                return new ProductoResponse($"An error occurred while updating the product: {e.Message}");
            }
        }

        public async Task<ProductoResponse> DeleteAsync(int id)
        {
            var existingProducto = await _productoRepository.FindById(id);

            if (existingProducto == null)
                return new ProductoResponse("Producto not found.");

            try
            {
                _productoRepository.Remove(existingProducto);
                await _unitOfWork.CompleteAsync();

                return new ProductoResponse(existingProducto);
            }
            catch (Exception e)
            {
                return new ProductoResponse($"An error occurred while deleting the product: {e.Message}");
            }
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _productoRepository.GetByIdAsync(id); // Llamada a tu repositorio
        }
    }
}
