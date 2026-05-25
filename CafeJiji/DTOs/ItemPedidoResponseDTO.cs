namespace CafeJiji.DTOs
{
    public class ItemPedidoResponseDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}