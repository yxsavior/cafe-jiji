using CafeJiji.DTOs;
using CafeJiji.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDTO), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult<LoginResponseDTO> Login([FromBody] LoginRequestDTO dto)
        {
            try
            {
                var response = _authService.Login(dto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { mensagem = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno ao processar o login.");
            }
        }
    }
}