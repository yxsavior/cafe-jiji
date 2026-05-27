using CafeJiji.Data;
using CafeJiji.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Services
{
    public interface IPedidoService
    {
        Task<Pedido> AbrirPedidoAsync(int numeroMesa, int usuarioId);
        Task AdicionarItensAsync(int pedidoId, List<(int ProdutoId, int Quantidade)> itens);
        Task FinalizarPedidoAsync(int pedidoId);
    }

    public class PedidoService : IPedidoService
    {
        private readonly CafeJijiDbContext _context;

        public PedidoService(CafeJijiDbContext context)
        {
            _context = context;
        }

        // =========================
        // ABRIR COMANDA
        // =========================
        public async Task<Pedido> AbrirPedidoAsync(int numeroMesa, int usuarioId)
        {
            var existe = await _context.Pedidos
                .AnyAsync(p => p.NumeroMesa == numeroMesa && p.Status == StatusPedido.Aberto);

            if (existe)
                throw new Exception("Já existe comanda aberta para essa mesa.");

            var pedido = new Pedido
            {
                NumeroMesa = numeroMesa,
                UsuarioId = usuarioId,
                Status = StatusPedido.Aberto,
                CriadoEm = DateTime.Now,
                Total = 0
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return pedido;
        }

        // =========================
        // ADICIONAR ITENS
        // =========================
        public async Task AdicionarItensAsync(int pedidoId, List<(int ProdutoId, int Quantidade)> itens)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null)
                throw new Exception("Pedido não encontrado.");

            if (pedido.Status != StatusPedido.Aberto)
                throw new Exception("Pedido já finalizado.");

            var produtosIds = itens.Select(i => i.ProdutoId).ToList();

            var produtos = await _context.Produtos
                .Where(p => produtosIds.Contains(p.Id))
                .ToListAsync();

            if (produtos.Count != produtosIds.Count)
                throw new Exception("Um ou mais produtos não existem.");

            foreach (var item in itens)
            {
                if (item.Quantidade <= 0)
                    throw new Exception("Quantidade inválida.");

                var produto = produtos.First(p => p.Id == item.ProdutoId);

                if (produto.QuantidadeEstoque < item.Quantidade)
                    throw new Exception($"Estoque insuficiente para {produto.Nome}.");

                // baixa estoque
                produto.QuantidadeEstoque -= item.Quantidade;

                var novoItem = new ItemPedido
                {
                    PedidoId = pedido.Id,
                    ProdutoId = produto.Id,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = produto.Preco,
                    Status = produto.RequerPreparo
                        ? StatusPreparo.Pendente
                        : StatusPreparo.Pronto
                };

                pedido.Itens.Add(novoItem);

                pedido.Total += item.Quantidade * produto.Preco;

                // ✅ CORREÇÃO AQUI
                _context.ItensPedidos.Add(novoItem);
            }

            pedido.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        // =========================
        // FINALIZAR COMANDA
        // =========================
        public async Task FinalizarPedidoAsync(int pedidoId)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null)
                throw new Exception("Pedido não encontrado.");

            if (pedido.Status != StatusPedido.Aberto)
                throw new Exception("Pedido já finalizado.");

            var temPendentes = pedido.Itens
                .Any(i => i.Status == StatusPreparo.Pendente);

            if (temPendentes)
                throw new Exception("Ainda existem itens pendentes na cozinha.");

            pedido.Status = StatusPedido.Finalizado;
            pedido.AtualizadoEm = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}