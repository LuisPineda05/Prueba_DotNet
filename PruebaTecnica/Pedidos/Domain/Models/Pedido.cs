namespace PruebaTecnica.Pedidos.Domain.Models

{
    public class Pedido
    {
        public int Id { get; set; }  
        public int ClienteId { get; set; }  
        public DateTime Fecha { get; set; }  
        public decimal Total { get; set; } 

        public ICollection<PedidoProducto> Productos { get; set; }  

      //  public Cliente Cliente { get; set; }  

    }
}
