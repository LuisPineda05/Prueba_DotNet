namespace PruebaTecnica.Pedidos.Domain.Models

{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }

    }
}
