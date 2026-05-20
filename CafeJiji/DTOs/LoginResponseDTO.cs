using System.Globalization;

namespace CafeJiji.DTOs
{
    public class LoginResponseDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Perfil {  get; set; } = string.Empty;
        public string TokenJWT {  get; set; } = string.Empty;
    }
}
