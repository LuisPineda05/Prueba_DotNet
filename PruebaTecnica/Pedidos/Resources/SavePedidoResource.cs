namespace PruebaTecnica.Pedidos.Resources
{
    public class SavePedidoResource
    {
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }

        public List<SavePedidoProductoResource> Productos { get; set; }
    }
}
