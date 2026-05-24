namespace CafeJiji.DTOs
{
    public class GatoUpdateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string? FotoUrl { get; set; }
        public string? IdadeAproximada { get; set; } // Adicionado conforme o escopo inicial
    }
}