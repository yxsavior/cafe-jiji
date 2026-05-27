namespace CafeJiji.DTOs
{
    public class ItemMesaCozinhaDTO
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime HorarioPedido { get; set; }
    }
}