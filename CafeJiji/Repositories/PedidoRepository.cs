using CafeJiji.Data;
using CafeJiji.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Repositories
{
    public class PedidoRepository
{
    private readonly CafeJijiDbContext _context;

    public PedidoRepository(CafeJijiDbContext context)
    {
        _context = context;
    }

    public async Task<Pedido?> GetByIdAsync(int id)
    {
        return await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
}