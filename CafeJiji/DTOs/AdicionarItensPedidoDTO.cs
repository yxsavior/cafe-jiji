namespace CafeJiji.DTOs
{
    public class AdicionarItensPedidoDTO
    {
        public List<ItemPedidoDTO> Itens { get; set; } = new();
    }

    public class ItemPedidoDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}