using CafeJiji.Data;
using CafeJiji.DTOs;
using CafeJiji.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsumosController : ControllerBase
    {
        private readonly CafeJijiDbContext _context;

        public InsumosController(CafeJijiDbContext context)
        {
            _context = context;
        }

        // GET: api/insumos (Listar todos os insumos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insumo>>> GetInsumos()
        {
            // Retorna a lista de insumos direto do banco para alimentar a tabela do Gerente
            return await _context.Set<Insumo>().ToListAsync();
        }

        // GET: api/insumos/5 (Buscar um insumo específico)
        [HttpGet("{id}")]
        public async Task<ActionResult<Insumo>> GetInsumo(int id)
        {
            var insumo = await _context.Set<Insumo>().FindAsync(id);
            if (insumo == null) return NotFound(new { mensagem = "Insumo não encontrado." });
            return insumo;
        }

        // POST: api/insumos (Cadastrar novo insumo)
        [HttpPost]
        [Authorize(Roles = "Gerente")] // Apenas gerentes podem cadastrar matérias-primas
        public async Task<ActionResult<Insumo>> PostInsumo(InsumoDTO dto)
        {
            var insumo = new Insumo
            {
                Nome = dto.Nome,
                QuantidadeAtual = dto.QuantidadeAtual,
                EstoqueMinimo = dto.EstoqueMinimo
            };

            _context.Set<Insumo>().Add(insumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInsumo), new { id = insumo.Id }, insumo);
        }

        // PUT: api/insumos/5 (Atualizar insumo completo pelo formulário do Gerente)
        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> PutInsumo(int id, InsumoDTO dto)
        {
            var insumo = await _context.Set<Insumo>().FindAsync(id);
            if (insumo == null) return NotFound(new { mensagem = "Insumo não encontrado." });

            insumo.Nome = dto.Nome;
            insumo.QuantidadeAtual = dto.QuantidadeAtual;
            insumo.EstoqueMinimo = dto.EstoqueMinimo;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/insumos/5/adicionar-estoque (Caso use seu DTO original para a Cozinha somar fardos)
        [HttpPatch("{id}/adicionar-estoque")]
        [Authorize]
        public async Task<IActionResult> AdicionarEstoque(int id, InsumoUpdateDTO dto)
        {
            var insumo = await _context.Set<Insumo>().FindAsync(id);
            if (insumo == null) return NotFound(new { mensagem = "Insumo não encontrado." });

            insumo.QuantidadeAtual += dto.QuantidadeAdicional;

            await _context.SaveChangesAsync();
            return Ok(new { mensagem = $"Estoque atualizado! Nova quantidade: {insumo.QuantidadeAtual}" });
        }

        // DELETE: api/insumos/5 (Remover insumo)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> DeleteInsumo(int id)
        {
            var insumo = await _context.Set<Insumo>().FindAsync(id);
            if (insumo == null) return NotFound(new { mensagem = "Insumo não encontrado." });

            _context.Set<Insumo>().Remove(insumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}