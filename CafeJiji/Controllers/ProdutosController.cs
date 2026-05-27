using CafeJiji.DTOs;
using CafeJiji.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        // =========================
        // GET
        // =========================
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var produtos = await _service.GetAllAsync();
            return Ok(produtos);
        }

        // =========================
        // POST
        // =========================
        [HttpPost]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Post(ProdutoCreateUpdateDTO dto)
        {
            try
            {
                var produto = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // =========================
        // PUT
        // =========================
        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Put(int id, ProdutoCreateUpdateDTO dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok(new { mensagem = "Produto atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // =========================
        // DELETE
        // =========================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { mensagem = "Produto removido com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}