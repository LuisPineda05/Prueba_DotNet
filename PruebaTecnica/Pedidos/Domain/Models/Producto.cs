namespace PruebaTecnica.Pedidos.Domain.Models

{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public ICollection<PedidoProducto> PedidoProductos { get; set; }

    }
}
