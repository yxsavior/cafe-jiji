using CafeJiji.Data;
using CafeJiji.DTOs;
using CafeJiji.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly CafeJijiDbContext _context;

        public PedidosController(CafeJijiDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Abre uma nova comanda para uma mesa.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> AbrirPedido(PedidoCreateDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var existe = await _context.Set<Pedido>()
                .AnyAsync(p => p.NumeroMesa == dto.NumeroMesa
                            && p.Status == StatusPedido.Aberto);

            if (existe)
                return BadRequest(new { mensagem = "Já existe comanda aberta para essa mesa." });

            var pedido = new Pedido
            {
                NumeroMesa = dto.NumeroMesa,
                UsuarioId = userId,
                Status = StatusPedido.Aberto,
                CriadoEm = DateTime.Now,
                Total = 0
            };

            _context.Add(pedido);
            await _context.SaveChangesAsync();

            return Ok(pedido);
        }

        // ADICIONAR ITEM
        [HttpPost("{pedidoId}/itens/lote")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> AdicionarItensLote(int pedidoId, AdicionarItensPedidoDTO dto)
        {
            var pedido = await _context.Set<Pedido>()
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado" });

            if (pedido.Status != StatusPedido.Aberto)
                return BadRequest(new { mensagem = "Pedido já finalizado" });

            var produtosIds = dto.Itens.Select(i => i.ProdutoId).ToList();

            var produtos = await _context.Set<Produto>()
                .Where(p => produtosIds.Contains(p.Id))
                .ToListAsync();

            if (produtos.Count != produtosIds.Count)
                return BadRequest(new { mensagem = "Um ou mais produtos não existem" });

            foreach (var itemDto in dto.Itens)
            {
                if (itemDto.Quantidade <= 0)
                {
                    return BadRequest(new
                    {
                        mensagem = "Quantidade deve ser maior que zero"
                    });
                }
                var produto = produtos.First(p => p.Id == itemDto.ProdutoId);

                if (produto.QuantidadeEstoque < itemDto.Quantidade)
                {
                    return BadRequest(new
                    {
                        mensagem = $"Estoque insuficiente para {produto.Nome}"
                    });
                }

                produto.QuantidadeEstoque -= itemDto.Quantidade;

                var item = new ItemPedido
                {
                    PedidoId = pedido.Id,
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produto.Preco,
                    Status = produto.RequerPreparo
                        ? StatusPreparo.Pendente
                        : StatusPreparo.Pronto
                };

                pedido.Itens.Add(item);

                pedido.Total += item.Quantidade * item.PrecoUnitario;

                _context.Add(item);
            }

            pedido.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Itens adicionados com sucesso",
                total = pedido.Total
            });
        }
        // COZINHA
        [HttpGet("cozinha")]
        [Authorize(Roles = "Cozinha,Atendente,Gerente")]
        public async Task<IActionResult> Cozinha()
        {
            var itens = await _context.Set<ItemPedido>()
                .Include(i => i.Produto)
                .Include(i => i.Pedido)
                .Where(i => i.Status == StatusPreparo.Pendente)
                .ToListAsync();
            
           var dados = itens
                .GroupBy(i => i.Pedido.NumeroMesa)
                .Select(g => new MesaCozinhaDTO
                {
                    NumeroMesa = g.Key,
                    Itens = g.Select(i => new ItemMesaCozinhaDTO
                    {
                        NomeProduto = i.Produto.Nome,
                        Quantidade = i.Quantidade,
                        HorarioPedido = i.Pedido.CriadoEm
                    }).ToList()
                })
                .OrderBy(x => x.Itens.Min(i => i.HorarioPedido))
                .ToList(); 
            return Ok(dados);
        }

        // ATUALIZAR STATUS
        [HttpPut("itens/{idItem}/status")]
        [Authorize(Roles = "Cozinha,Gerente")]
        public async Task<IActionResult> AtualizarStatus(int idItem)
        {
            var item = await _context.Set<ItemPedido>()
                .FirstOrDefaultAsync(i => i.Id == idItem);

            if (item == null)
                return NotFound();

            if (item.Status == StatusPreparo.Pronto)
            return BadRequest(new { mensagem = "Item já está pronto." });

            item.Status = StatusPreparo.Pronto;

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Item pronto." });
        }

        // PEDIDOS ATIVOS
        [HttpGet("ativos")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> GetAtivos()
        {
            var pedidos = await _context.Set<Pedido>()
                .Where(p => p.Status == StatusPedido.Aberto)
                .Select(p => new
                {
                    p.Id,
                    p.NumeroMesa,
                    p.Status,
                    p.Total,

                    Itens = p.Itens.Select(i => new ItemPedidoResponseDTO
                    {
                        Id = i.Id,
                        ProdutoId = i.ProdutoId,
                        NomeProduto = i.Produto.Nome,
                        Quantidade = i.Quantidade,
                        PrecoUnitario = i.PrecoUnitario,
                        Status = i.Status.ToString()
                    })
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        // FINALIZAR COMANDA
        [HttpPut("{pedidoId}/finalizar")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> Finalizar(int pedidoId)
        {
            var pedido = await _context.Set<Pedido>()
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null)
                return NotFound();

            if (pedido.Status != StatusPedido.Aberto)
                return BadRequest(new { mensagem = "Pedido já finalizado." });

            var itensPendentes = await _context.Set<ItemPedido>()
                .AnyAsync(i => i.PedidoId == pedidoId && i.Status == StatusPreparo.Pendente);

            if (itensPendentes)
                return BadRequest(new { mensagem = "Ainda existem itens pendentes na cozinha." });

            pedido.Status = StatusPedido.Finalizado;
            pedido.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Comanda finalizada com sucesso." });
        }
    }
}