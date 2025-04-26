namespace PruebaTecnica.Pedidos.Resources
{
    public class PedidoResource
    {
        public int Id { get; set; } 
        public int ClienteId { get; set; }  
        public DateTime Fecha { get; set; } 
        public decimal Total { get; set; } 

        public List<PedidoProductoResource> Productos { get; set; }
    }
}
