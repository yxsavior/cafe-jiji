namespace CafeJiji.DTOs
{
    public class PedidoResponseDTO
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<ItemExtratoDTO> Itens { get; set; } = new();
    }
}