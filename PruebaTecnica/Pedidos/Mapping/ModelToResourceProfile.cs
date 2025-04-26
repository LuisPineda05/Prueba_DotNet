using AutoMapper;
using PruebaTecnica.Pedidos.Domain.Models;
using PruebaTecnica.Pedidos.Resources;

namespace PruebaTecnica.Pedidos.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Cliente, ClienteResource>();
            CreateMap<Producto, ProductoResource>();
            CreateMap<Pedido, PedidoResource>();
            CreateMap<PedidoProducto, PedidoProductoResource>();
        }
    }
}
