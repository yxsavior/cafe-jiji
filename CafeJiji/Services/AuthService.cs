using CafeJiji.Data;
using CafeJiji.DTOs;
using CafeJiji.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Services
{
    public interface IAuthService
    {
        LoginResponseDTO Login(LoginRequestDTO dto);
    }

    public class AuthService : IAuthService
    {
        private readonly CafeJijiDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(CafeJijiDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public LoginResponseDTO Login(LoginRequestDTO dto)
        {
            // Busca o usuário apenas pelo Username
            var usuario = _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.Username == dto.Username);

            // Validação de segurança
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            return new LoginResponseDTO
            {
                Nome = usuario.Nome,
                Perfil = usuario.Perfil,
                TokenJWT = _jwtService.GerarToken(usuario)
            };
        }
    }
}