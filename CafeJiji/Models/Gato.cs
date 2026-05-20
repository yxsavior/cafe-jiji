namespace CafeJiji.Models
{
    public class Gato
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public StatusGato Status { get; set; } = StatusGato.Disponivel;
        public DateOnly DataChegada { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? DataAdotacao { get; set; }
        public string? FotoUrl { get; set; }
        public string? NumeroProtocoloONG { get; set; }
        public int? AdotanteId { get; set; }
        public Adotante? Adotante { get; set; }
    }
}
