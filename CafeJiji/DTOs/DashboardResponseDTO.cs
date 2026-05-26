namespace CafeJiji.DTOs
{
    public class DashboardResponseDTO
    {
        public decimal FaturamentoMensal { get; set; }
        public double PercentualCrescimento { get; set; }
        public int PedidosConcluidos { get; set; }
        public int MediaClientesDiarios { get; set; }
        public decimal TaxasGatil { get; set; }
        public int VisitasAgendadas { get; set; }
        
        // Listas para alimentar os gráficos dinamicamente
        public List<decimal> FaturamentoSemanal { get; set; } = new();
        public List<CategoriaQuantidadeDTO> CategoriasMaisVendidas { get; set; } = new();
    }
}