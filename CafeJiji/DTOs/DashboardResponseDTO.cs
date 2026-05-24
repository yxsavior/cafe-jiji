namespace CafeJiji.DTOs
{
    public class DashboardResponseDTO
    {
        public decimal FaturamentoTotal { get; set; }
        public int TotalAdocoes { get; set; }
        public int GatosDisponiveis { get; set; }
        public List<ProdutoEstoqueCriticoDTO> ItensCriticos { get; set; } = new();
    }

    public class ProdutoEstoqueCriticoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int EstoqueAtual { get; set; }
    }
}