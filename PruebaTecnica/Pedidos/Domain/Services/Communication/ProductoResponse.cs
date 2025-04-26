using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Shared.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Services.Communication
{
    public class ProductoResponse : BaseResponse<Producto>
    {
        public ProductoResponse(string message) : base(message)
        {
        }

        public ProductoResponse(Producto resource) : base(resource)
        {
        }
    }
}
