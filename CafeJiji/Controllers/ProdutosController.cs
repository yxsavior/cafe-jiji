using CafeJiji.Data;
using CafeJiji.DTOs;
using CafeJiji.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly CafeJijiDbContext _context;

        public ProdutosController(CafeJijiDbContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var produtos = await _context.Produtos
                .Where(p => p.Ativo && p.QuantidadeEstoque > 0)
                .OrderBy(p => p.Categoria)
                .ThenBy(p => p.Nome)
                .ToListAsync();

            return Ok(produtos);
        }

        // POST
        [HttpPost]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Post(
            ProdutoCreateUpdateDTO dto)
        {
            if (dto.Preco <= 0)
            {
                return BadRequest(new
                {
                    mensagem = "Preço deve ser maior que zero."
                });
            }

            var produto = new Produto
            {
                Nome = dto.Nome,
                Preco = dto.Preco,
                Categoria = dto.Categoria,
                QuantidadeEstoque = dto.QuantidadeEstoque,
                EstoqueMinimo = dto.EstoqueMinimo,
                RequerPreparo = dto.RequerPreparo,
                Ativo = dto.Ativo,
                CriadoEm = DateTime.Now,
                AtualizadoEm = DateTime.Now
            };

            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = produto.Id },
                produto
            );
        }

        // PUT
        [HttpPut("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Put(
            int id,
            ProdutoCreateUpdateDTO dto)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            produto.Nome = dto.Nome;
            produto.Preco = dto.Preco;
            produto.Categoria = dto.Categoria;
            produto.QuantidadeEstoque = dto.QuantidadeEstoque;
            produto.EstoqueMinimo = dto.EstoqueMinimo;
            produto.RequerPreparo = dto.RequerPreparo;
            produto.Ativo = dto.Ativo;
            produto.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Produto atualizado com sucesso."
            });
        }

        // DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            // DELETE LÓGICO
            produto.Ativo = false;
            produto.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Produto removido com sucesso."
            });
        }
    }
}