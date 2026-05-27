using CafeJiji.Data;
using CafeJiji.DTOs;
using CafeJiji.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetAllAsync();
        Task<Produto> CreateAsync(ProdutoCreateUpdateDTO dto);
        Task UpdateAsync(int id, ProdutoCreateUpdateDTO dto);
        Task DeleteAsync(int id);
    }

    public class ProdutoService : IProdutoService
    {
        private readonly CafeJijiDbContext _context;

        public ProdutoService(CafeJijiDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET
        // =========================
        public async Task<List<Produto>> GetAllAsync()
        {
            return await _context.Produtos
                .Where(p => p.Ativo && p.QuantidadeEstoque > 0)
                .OrderBy(p => p.Categoria)
                .ThenBy(p => p.Nome)
                .ToListAsync();
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Produto> CreateAsync(ProdutoCreateUpdateDTO dto)
        {
            if (dto.Preco <= 0)
                throw new Exception("Preço deve ser maior que zero.");

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

            return produto;
        }

        // =========================
        // UPDATE
        // =========================
        public async Task UpdateAsync(int id, ProdutoCreateUpdateDTO dto)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                throw new Exception("Produto não encontrado.");

            produto.Nome = dto.Nome;
            produto.Preco = dto.Preco;
            produto.Categoria = dto.Categoria;
            produto.QuantidadeEstoque = dto.QuantidadeEstoque;
            produto.EstoqueMinimo = dto.EstoqueMinimo;
            produto.RequerPreparo = dto.RequerPreparo;
            produto.Ativo = dto.Ativo;
            produto.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        // =========================
        // DELETE (LÓGICO)
        // =========================
        public async Task DeleteAsync(int id)
        {
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                throw new Exception("Produto não encontrado.");

            produto.Ativo = false;
            produto.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}