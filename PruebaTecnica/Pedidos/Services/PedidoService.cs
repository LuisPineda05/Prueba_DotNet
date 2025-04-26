using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Domain.Repositories;
using PruebaTecnica.Pedidos.Domain.Services;
using PruebaTecnica.Pedidos.Domain.Services.Communication;
using PruebaTecnica.Shared.Domain.Repositories;

namespace PruebaTecnica.Pedidos.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IProductoService _productoService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProductoPedidoRepository _pedidoProductoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoService(IProductoService productoService, IPedidoRepository pedidoRepository, IProductoPedidoRepository pedidoProductoRepository, IUnitOfWork unitOfWork)
        {
            _productoService = productoService;
            _pedidoRepository = pedidoRepository;
            _pedidoProductoRepository = pedidoProductoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PedidoResponse> SaveAsync(Pedido pedido, List<PedidoProducto> productos)
        {
            try
            {
                decimal total = 0;

                foreach (var item in productos)
                {
                    var producto = await _productoService.GetByIdAsync(item.ProductoId);

                    if (producto == null)
                        return new PedidoResponse($"El producto con ID {item.ProductoId} no existe.");

                    if (producto.Stock < item.Cantidad)
                    {
                        return new PedidoResponse($"No hay suficiente stock para el producto {producto.Nombre}. Stock disponible: {producto.Stock}.");
                    }

                    total += producto.Precio * item.Cantidad;

                    producto.Stock -= item.Cantidad;
                    await _productoService.UpdateAsync(producto.Id, producto);
                }

                pedido.Total = total;
                pedido.Productos = productos;

                await _pedidoRepository.AddAsync(pedido);
                Console.WriteLine(pedido);
                await _unitOfWork.CompleteAsync();

                foreach (var productoPedido in productos)
                {
                    productoPedido.PedidoId = pedido.Id;
                    await _pedidoProductoRepository.AddAsync(productoPedido);
                }

                return new PedidoResponse(pedido);
            }
            catch (Exception e)
            {
                return new PedidoResponse($"An error occurred while saving the order: {e.Message}");
            }
        }

        public async Task<PedidoResponse> GetByIdAsync(int id)
        {
            var pedido = await _pedidoRepository.FindById(id);

            if (pedido == null)
                return new PedidoResponse("Pedido not found.");

            var productos = await _pedidoProductoRepository.ListByPedidoIdAsync(id);

            pedido.Productos = productos;

            return new PedidoResponse(pedido);
        }

        public async Task<IEnumerable<Pedido>> ListAsync()
        {
            return await _pedidoRepository.ListAsync();
        }

        public async Task<PedidoResponse> DeleteAsync(int id)
        {
            var pedido = await _pedidoRepository.FindById(id);

            if (pedido == null)
                return new PedidoResponse("Pedido not found.");

            await _pedidoRepository.RemoveAsync(pedido);
            await _unitOfWork.CompleteAsync(); 

            return new PedidoResponse(pedido);
        }
    }
}
