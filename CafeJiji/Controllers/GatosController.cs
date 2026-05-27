using CafeJiji.DTOs;
using CafeJiji.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/gatos")]
    [Authorize]
    public class GatosController : ControllerBase
    {
        private readonly IGatoService _service;

        public GatosController(IGatoService service)
        {
            _service = service;
        }

        // =========================
        // GET ALL
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gatos = await _service.GetAllAsync();

            var result = gatos.Select(g => new
            {
                g.Id,
                g.Nome,
                g.Status,
                g.DataChegada,
                g.DataAdotacao,
                g.FotoUrl,
                Adotante = g.Adotante == null ? null : new
                {
                    g.Adotante.Id,
                    g.Adotante.Nome
                }
            });

            return Ok(result);
        }

        // =========================
        // GET BY ID
        // =========================
        [HttpGet("{id}")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> GetById(int id)
        {
            var gato = await _service.GetByIdAsync(id);

            if (gato == null)
                return NotFound(new { mensagem = "Gato não encontrado" });

            return Ok(new
            {
                gato.Id,
                gato.Nome,
                gato.Status,
                gato.DataChegada,
                gato.DataAdotacao,
                gato.FotoUrl,
                Adotante = gato.Adotante == null ? null : new
                {
                    gato.Adotante.Id,
                    gato.Adotante.Nome,
                    gato.Adotante.CPF,
                    gato.Adotante.Telefone,
                    gato.Adotante.Email
                }
            });
        }

        // =========================
        // CREATE
        // =========================
        [HttpPost]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Create(CadastrarGatoDTO dto)
        {
            var gato = await _service.CreateAsync(dto);
            return Ok(gato);
        }

        // =========================
        // UPDATE
        // =========================
        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Update(int id, GatoUpdateDTO dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok(new { mensagem = "Gato atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        // =========================
        // RESERVAR
        // =========================
        [HttpPut("{id}/reservar")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> Reservar(int id, ReservarGatoDTO dto)
        {
            try
            {
                await _service.ReservarAsync(id, dto);
                return Ok(new { mensagem = "Gato reservado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // =========================
        // ADOTAR
        // =========================
        [HttpPut("{id}/adotado")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> Adotar(int id)
        {
            try
            {
                await _service.AdotarAsync(id);
                return Ok(new { mensagem = "Adoção concluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}