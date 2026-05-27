namespace CafeJiji.DTOs
{
    public class InsumoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int QuantidadeAtual { get; set; }
        public int EstoqueMinimo { get; set; }
    }
}