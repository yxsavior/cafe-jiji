using CafeJiji.Data;
using CafeJiji.DTOs;
using CafeJiji.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/gatos")]
    [Authorize]
    public class GatosController : ControllerBase
    {
        private readonly CafeJijiDbContext _context;

        public GatosController(CafeJijiDbContext context)
        {
            _context = context;
        }

        // GET: /api/gatos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gatos = await _context.Set<Gato>()
                .Include(g => g.Adotante)
                .Select(g => new
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
                })
                .ToListAsync();

            return Ok(gatos);
        }

        // GET: /api/gatos/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> GetById(int id)
        {
            var gato = await _context.Set<Gato>()
                // .Include(g => g.Adotante)
                .FirstOrDefaultAsync(g => g.Id == id);

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

        // POST: /api/gatos
        [HttpPost]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Create(CadastrarGatoDTO dto)
        {
            var gato = new Gato
            {
                Nome = dto.Nome,
                FotoUrl = dto.FotoUrl,
                Status = StatusGato.Disponivel,
                DataChegada = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Add(gato);
            await _context.SaveChangesAsync();

            return Ok(gato);
        }

        // PUT: /api/gatos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Update(int id, GatoUpdateDTO dto)
        {
            var gato = await _context.Set<Gato>()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gato == null)
                return NotFound(new { mensagem = "Gato não encontrado" });

            gato.Nome = dto.Nome;
            gato.FotoUrl = dto.FotoUrl;

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Gato atualizado com sucesso" });
        }

        // PUT: /api/gatos/{id}/reservar
        [HttpPut("{id}/reservar")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> Reservar(int id, ReservarGatoDTO dto)
        {
            var gato = await _context.Set<Gato>()
                .Include(g => g.Adotante)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gato == null)
                return NotFound(new { mensagem = "Gato não encontrado" });

            if (gato.Status != StatusGato.Disponivel)
                return BadRequest(new { mensagem = "Gato não está disponível para reserva" });

            var adotante = new Adotante
            {
                Nome = dto.AdotanteNome,
                CPF = dto.AdotanteCPF,
                Telefone = dto.AdotanteTelefone,
                Email = dto.AdotanteEmail
            };

            gato.Adotante = adotante;
            gato.Status = StatusGato.Reservado;

            _context.Add(adotante);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Gato reservado com sucesso" });
        }

        // PUT: /api/gatos/{id}/adotado
        [HttpPut("{id}/adotado")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> Adotar(int id)
        {
            var gato = await _context.Set<Gato>()
                .Include(g => g.Adotante)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gato == null)
                return NotFound(new { mensagem = "Gato não encontrado" });

            if (gato.Status != StatusGato.Reservado)
                return BadRequest(new { mensagem = "Gato precisa estar reservado antes da adoção" });

            gato.Status = StatusGato.Adotado;
            gato.DataAdotacao = DateOnly.FromDateTime(DateTime.Now);

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Adoção concluída com sucesso" });
        }
    }
}