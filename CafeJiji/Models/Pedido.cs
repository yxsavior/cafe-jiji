namespace JijiCafe.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.Now;
        public DateTime? AtualizadoEm { get; set; }
        public StatusPedido Status { get; set; }
        public decimal Total { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public List<ItemPedido> Itens { get; set; } = new();
    }
}
