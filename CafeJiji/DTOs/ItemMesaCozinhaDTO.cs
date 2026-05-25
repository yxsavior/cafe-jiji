namespace CafeJiji.DTOs
{
    public class ItemMesaCozinhaDTO
    {
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime HorarioPedido { get; set; }
    }
}