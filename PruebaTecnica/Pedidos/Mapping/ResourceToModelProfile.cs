using AutoMapper;
using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Resources;

namespace PruebaTecnica.Pedidos.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveClienteResource, Cliente>();
            CreateMap<SaveProductoResource, Producto>();
            CreateMap<SavePedidoResource, Pedido>();
            CreateMap<SavePedidoProductoResource, PedidoProducto>();
        }
    }
}
