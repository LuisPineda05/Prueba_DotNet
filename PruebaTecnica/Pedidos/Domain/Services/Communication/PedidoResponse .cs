using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Shared.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Services.Communication
{
    public class PedidoResponse : BaseResponse<Pedido>
    {
        public PedidoResponse(string message) : base(message)
        {
        }

        public PedidoResponse(Pedido resource) : base(resource)
        {
        }




    }
}
