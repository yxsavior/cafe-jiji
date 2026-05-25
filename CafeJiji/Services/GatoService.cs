using CafeJiji.Data;
using CafeJiji.DTOs;
using CafeJiji.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Services
{
    public interface IGatoService
    {
        Task<List<Gato>> GetAllAsync();
        Task<Gato?> GetByIdAsync(int id);
        Task<Gato> CreateAsync(CadastrarGatoDTO dto);
        Task UpdateAsync(int id, GatoUpdateDTO dto);
        Task ReservarAsync(int id, ReservarGatoDTO dto);
        Task AdotarAsync(int id);
    }

    public class GatoService : IGatoService
    {
        private readonly CafeJijiDbContext _context;

        public GatoService(CafeJijiDbContext context)
        {
            _context = context;
        }

        // =========================
        // GET ALL
        // =========================
        public async Task<List<Gato>> GetAllAsync()
        {
            return await _context.Set<Gato>()
                .Include(g => g.Adotante)
                .ToListAsync();
        }

        // =========================
        // GET BY ID
        // =========================
        public async Task<Gato?> GetByIdAsync(int id)
        {
            return await _context.Set<Gato>()
                .Include(g => g.Adotante)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        // =========================
        // CREATE
        // =========================
        public async Task<Gato> CreateAsync(CadastrarGatoDTO dto)
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

            return gato;
        }

        // =========================
        // UPDATE
        // =========================
        public async Task UpdateAsync(int id, GatoUpdateDTO dto)
        {
            var gato = await _context.Set<Gato>()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gato == null)
                throw new Exception("Gato não encontrado.");

            gato.Nome = dto.Nome;
            gato.FotoUrl = dto.FotoUrl;

            await _context.SaveChangesAsync();
        }

        // =========================
        // RESERVAR
        // =========================
        public async Task ReservarAsync(int id, ReservarGatoDTO dto)
        {
            var gato = await _context.Set<Gato>()
                .Include(g => g.Adotante)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gato == null)
                throw new Exception("Gato não encontrado.");

            if (gato.Status != StatusGato.Disponivel)
                throw new Exception("Gato não está disponível para reserva.");

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
        }

        // =========================
        // ADOTAR
        // =========================
        public async Task AdotarAsync(int id)
        {
            var gato = await _context.Set<Gato>()
                .Include(g => g.Adotante)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gato == null)
                throw new Exception("Gato não encontrado.");

            if (gato.Status != StatusGato.Reservado)
                throw new Exception("Gato precisa estar reservado antes da adoção.");

            gato.Status = StatusGato.Adotado;
            gato.DataAdotacao = DateOnly.FromDateTime(DateTime.Now);

            await _context.SaveChangesAsync();
        }
    }
}