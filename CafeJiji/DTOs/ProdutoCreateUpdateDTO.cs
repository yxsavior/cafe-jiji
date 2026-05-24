namespace CafeJiji.DTOs
{
    public class ProdutoCreateUpdateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public int QuantidadeEstoque { get; set; }
        public int EstoqueMinimo { get; set; }
        public bool RequerPreparo { get; set; }
        public bool Ativo { get; set; } = true;
    }
}