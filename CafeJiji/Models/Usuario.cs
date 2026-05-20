namespace JijiCafe.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Username {  get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Perfil { get; set; } = "Atendente";

    }
}
