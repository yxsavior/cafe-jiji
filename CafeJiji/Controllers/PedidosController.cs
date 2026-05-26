using CafeJiji.DTOs;
using CafeJiji.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CafeJiji.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        // =========================
        // ABRIR COMANDA
        // =========================
        [HttpPost]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> AbrirPedido(PedidoCreateDTO dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            try
            {
                var pedido = await _pedidoService.AbrirPedidoAsync(dto.NumeroMesa, userId);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // =========================
        // ADICIONAR ITENS EM LOTE
        // =========================
        [HttpPost("{pedidoId}/itens/lote")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> AdicionarItensLote(int pedidoId, AdicionarItensPedidoDTO dto)
        {
            try
            {
                var itens = dto.Itens
                    .Select(i => (i.ProdutoId, i.Quantidade))
                    .ToList();

                await _pedidoService.AdicionarItensAsync(pedidoId, itens);

                return Ok(new
                {
                    mensagem = "Itens adicionados com sucesso"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // =========================
        // COZINHA (AINDA PODE FICAR NO CONTROLLER)
        // =========================
        [HttpGet("cozinha")]
        [Authorize(Roles = "Cozinha,Atendente,Gerente")]
        public async Task<IActionResult> Cozinha()
        {
            var itens = await HttpContext.RequestServices
                .GetService<CafeJiji.Data.CafeJijiDbContext>()!
                .Set<CafeJiji.Models.ItemPedido>()
                .Include(i => i.Produto)
                .Include(i => i.Pedido)
                .Where(i => i.Status == CafeJiji.Models.StatusPreparo.Pendente)
                .ToListAsync();

            var dados = itens
                .GroupBy(i => i.Pedido.NumeroMesa)
                .Select(g => new MesaCozinhaDTO
                {
                    NumeroMesa = g.Key,
                    Itens = g.Select(i => new ItemMesaCozinhaDTO
                    {
                        Id = i.Id,
                        NomeProduto = i.Produto.Nome,
                        Quantidade = i.Quantidade,
                        HorarioPedido = i.Pedido.CriadoEm
                    }).ToList()
                })
                .OrderBy(x => x.Itens.Min(i => i.HorarioPedido))
                .ToList();

            return Ok(dados);
        }

        // =========================
        // STATUS ITEM (PODE IR PRO SERVICE DEPOIS)
        // =========================
        [HttpPut("itens/{idItem}/status")]
        [Authorize(Roles = "Cozinha,Gerente")]
        public async Task<IActionResult> AtualizarStatus(int idItem)
        {
            var db = HttpContext.RequestServices
                .GetService<CafeJiji.Data.CafeJijiDbContext>()!;

            var item = await db.Set<CafeJiji.Models.ItemPedido>()
                .FirstOrDefaultAsync(i => i.Id == idItem);

            if (item == null)
                return NotFound();

            if (item.Status == CafeJiji.Models.StatusPreparo.Pronto)
                return BadRequest(new { mensagem = "Item já está pronto." });

            item.Status = CafeJiji.Models.StatusPreparo.Pronto;

            await db.SaveChangesAsync();

            return Ok(new { mensagem = "Item pronto." });
        }

        // =========================
        // PEDIDOS ATIVOS
        // =========================
        [HttpGet("ativos")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> GetAtivos()
        {
            var db = HttpContext.RequestServices
                .GetService<CafeJiji.Data.CafeJijiDbContext>()!;

            var pedidos = await db.Set<CafeJiji.Models.Pedido>()
                .Where(p => p.Status == CafeJiji.Models.StatusPedido.Aberto)
                .Select(p => new
                {
                    p.Id,
                    p.NumeroMesa,
                    p.Total,
                    ItensPendentes = p.Itens.Count(i => i.Status == CafeJiji.Models.StatusPreparo.Pendente),
                    TotalItens = p.Itens.Count()
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        // =========================
        // FINALIZAR COMANDA
        // =========================
        [HttpPut("{pedidoId}/finalizar")]
        [Authorize(Roles = "Atendente,Gerente")]
        public async Task<IActionResult> Finalizar(int pedidoId)
        {
            try
            {
                await _pedidoService.FinalizarPedidoAsync(pedidoId);

                return Ok(new
                {
                    mensagem = "Comanda finalizada com sucesso."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}