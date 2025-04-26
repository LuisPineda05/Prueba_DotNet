using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Shared.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Services.Communication
{
    public class ProductoPedidoResponse : BaseResponse<PedidoProducto>
    {
        public ProductoPedidoResponse(string message) : base(message)
        {
        }

        public ProductoPedidoResponse(PedidoProducto resource) : base(resource)
        {
        }
    }
}
