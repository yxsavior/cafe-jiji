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

        [HttpGet("dashboard")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> GetDashboard()
        {
            var db = HttpContext.RequestServices.GetService<CafeJiji.Data.CafeJijiDbContext>()!;

            try
            {
                var hoje = DateTime.UtcNow;
                var inicioMesAtual = new DateTime(hoje.Year, hoje.Month, 1);
                var inicioMesAnterior = inicioMesAtual.AddMonths(-1);

                // 1. Faturamento Mensal (Mês Atual)
                var faturamentoMensal = await db.Set<CafeJiji.Models.Pedido>()
                    .Where(p => p.Status == CafeJiji.Models.StatusPedido.Finalizado && p.CriadoEm >= inicioMesAtual)
                    .SumAsync(p => p.Total);

                // 2. Faturamento do Mês Anterior (para calcular o % de crescimento)
                var faturamentoMesAnterior = await db.Set<CafeJiji.Models.Pedido>()
                    .Where(p => p.Status == CafeJiji.Models.StatusPedido.Finalizado && p.CriadoEm >= inicioMesAnterior && p.CriadoEm < inicioMesAtual)
                    .SumAsync(p => p.Total);

                double percentualCrescimento = 0;
                if (faturamentoMesAnterior > 0)
                {
                    percentualCrescimento = (double)((faturamentoMensal - faturamentoMesAnterior) / faturamentoMesAnterior) * 100;
                }

                // 3. Pedidos Concluídos no Mês
                var pedidosConcluidos = await db.Set<CafeJiji.Models.Pedido>()
                    .CountAsync(p => p.Status == CafeJiji.Models.StatusPedido.Finalizado && p.CriadoEm >= inicioMesAtual);

                // 4. Média de Clientes Diários (Pedidos únicos por dia no mês)
                var diasComPedidos = await db.Set<CafeJiji.Models.Pedido>()
                    .Where(p => p.CriadoEm >= inicioMesAtual)
                    .Select(p => p.CriadoEm.Date)
                    .Distinct()
                    .CountAsync();

                int mediaClientesDiarios = diasComPedidos > 0 ? pedidosConcluidos / diasComPedidos : 0;

                // 5. Taxas do Gatil e Visitas (Exemplo se houver um produto/serviço do tipo "Gatil" ou tabela dedicada)
                // Aqui buscamos produtos vendidos que tenham "Gatil" ou "Visita" na categoria/nome
                var taxasGatil = await db.Set<CafeJiji.Models.ItemPedido>()
                    .Where(i => i.Pedido.Status == CafeJiji.Models.StatusPedido.Finalizado && 
                                i.Pedido.CriadoEm >= inicioMesAtual && 
                                (i.Produto.Categoria == "Gatil" || i.Produto.Nome.Contains("Visita")))
                    .SumAsync(i => i.Quantidade * i.PrecoUnitario);

                var visitasAgendadas = await db.Set<CafeJiji.Models.ItemPedido>()
                    .Where(i => i.Pedido.CriadoEm >= inicioMesAtual && 
                                (i.Produto.Categoria == "Gatil" || i.Produto.Nome.Contains("Visita")))
                    .SumAsync(i => i.Quantidade);

                // 6. Dados para o Gráfico de Curva Semanal (Últimos 7 dias)
                var faturamentoSemanal = new List<decimal>();
                for (int i = 6; i >= 0; i--)
                {
                    var dia = hoje.Date.AddDays(-i);
                    var totalDia = await db.Set<CafeJiji.Models.Pedido>()
                        .Where(p => p.Status == CafeJiji.Models.StatusPedido.Finalizado && p.CriadoEm.Date == dia)
                        .SumAsync(p => p.Total);
                    faturamentoSemanal.Add(totalDia);
                }

                // 7. Dados para o Gráfico de Pizza/Donut (Categorias mais vendidas)
                var categoriasMaisVendidas = await db.Set<CafeJiji.Models.ItemPedido>()
                    .Where(i => i.Pedido.Status == CafeJiji.Models.StatusPedido.Finalizado && i.Pedido.CriadoEm >= inicioMesAtual)
                    .GroupBy(i => i.Produto.Categoria)
                    .Select(g => new CategoriaQuantidadeDTO
                    {
                        Categoria = g.Key ?? "Geral",
                        Quantidade = g.Sum(i => i.Quantidade)
                    })
                    .OrderByDescending(c => c.Quantidade)
                    .Take(4) // Top 4 categorias
                    .ToListAsync();

                return Ok(new DashboardResponseDTO
                {
                    FaturamentoMensal = faturamentoMensal,
                    PercentualCrescimento = Math.Round(percentualCrescimento, 1),
                    PedidosConcluidos = pedidosConcluidos,
                    MediaClientesDiarios = mediaClientesDiarios,
                    TaxasGatil = taxasGatil,
                    VisitasAgendadas = visitasAgendadas,
                    FaturamentoSemanal = faturamentoSemanal,
                    CategoriasMaisVendidas = categoriasMaisVendidas
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "Erro ao processar métricas: " + ex.Message });
            }
        }

    }
}