namespace CafeJiji.DTOs
{
    public class AdicionarItensPedidoDTO
    {
        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }
}