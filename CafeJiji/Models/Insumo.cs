namespace JijiCafe.Models
{
    public class Insumo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int QuantidadeAtual { get; set; }
        public int EstoqueMinimo { get; set; }
    }
}
