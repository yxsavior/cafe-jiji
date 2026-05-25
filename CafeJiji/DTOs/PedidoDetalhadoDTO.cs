namespace CafeJiji.DTOs
{
    public class PedidoDetalhadoDTO
    {
        public int PedidoId { get; set; }
        public int NumeroMesa {  get; set; }
        public string NomeAtendente { get; set; } = string.Empty;
        public List<ItemExtratoDTO> ItensConsumidos { get; set; } = new();
        public decimal ValorTotal { get; set; }
    }
}
