using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Shared.Domain.Services.Communication;

namespace PruebaTecnica.Pedidos.Domain.Services.Communication
{
    public class ClienteResponse : BaseResponse<Cliente>
    {
        public ClienteResponse(string message) : base(message)
        {
        }

        public ClienteResponse(Cliente resource) : base(resource)
        {
        }
    }
}
