using CafeJiji.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Data
{
    public static class CafeJijiDbSeeder
    {
        public static void Seed(CafeJijiDbContext context, IConfiguration configuration) 
        {
            // 1. SEED DE USUÁRIOS
            if (!context.Set<Usuario>().Any())
            {
                var srPassword = configuration["SeedUsers:SrPassword"];
                var midPassword = configuration["SeedUsers:MidPassword"];
                var jrPassword = configuration["SeedUsers:JrPassword"];

                if (string.IsNullOrEmpty(srPassword) ||
                    string.IsNullOrEmpty(midPassword) ||
                    string.IsNullOrEmpty(jrPassword))
                {
                    throw new Exception("SeedUsers não configurado no .env");
                }

                context.Set<Usuario>().AddRange(
                    new Usuario
                    {
                        Nome = "Kiki",
                        Username = "gerente",
                        SenhaHash = BCrypt.Net.BCrypt.HashPassword(srPassword),
                        Perfil = "Gerente"
                    },

                    new Usuario
                    {
                        Nome = "Ursula",
                        Username = "atendente",
                        SenhaHash = BCrypt.Net.BCrypt.HashPassword(jrPassword),
                        Perfil = "Atendente"
                    },

                    new Usuario
                    {
                        Nome = "Osono",
                        Username = "barista",
                        SenhaHash = BCrypt.Net.BCrypt.HashPassword(midPassword),
                        Perfil = "Cozinha"
                    }
                );
            }

            // 2. SEED DE PRODUTOS
            if (!context.Set<Produto>().Any())
            {
                context.Set<Produto>().AddRange(
                    // Categoria: Cafés
                    new Produto { Nome = "Espresso Tradicional", Preco = 7.50m, Categoria = "Cafés", QuantidadeEstoque = 999, EstoqueMinimo = 0, RequerPreparo = true, ImagemUrl = "/images/expresso.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },
                    new Produto { Nome = "Capuccino Jiji", Preco = 12.00m, Categoria = "Cafés", QuantidadeEstoque = 999, EstoqueMinimo = 0, RequerPreparo = true, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },
                    new Produto { Nome = "Latte Macchiato", Preco = 14.50m, Categoria = "Cafés", QuantidadeEstoque = 999, EstoqueMinimo = 0, RequerPreparo = true, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },

                    // Categoria: Doces
                    new Produto { Nome = "Fatia de Torta Holandesa", Preco = 16.00m, Categoria = "Doces", QuantidadeEstoque = 12, EstoqueMinimo = 3, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },
                    new Produto { Nome = "Brownie de Chocolate", Preco = 9.50m, Categoria = "Doces", QuantidadeEstoque = 20, EstoqueMinimo = 5, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },

                    // Categoria: Salgados
                    new Produto { Nome = "Pão de Queijo Recheado", Preco = 8.00m, Categoria = "Salgados", QuantidadeEstoque = 15, EstoqueMinimo = 4, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },

                    // Categoria: Serviços
                    new Produto { Nome = "Taxa de Entrada Gatil", Preco = 15.00m, Categoria = "Serviços", QuantidadeEstoque = 9999, EstoqueMinimo = 0, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now }
                );
            }

            // 3. SEED DE GATOS
            if (!context.Set<Gato>().Any())
            {
                context.Set<Gato>().AddRange(
                    new Gato { Nome = "Mingau", Status = StatusGato.Disponivel, DataChegada = new DateOnly(2026, 4, 10), FotoUrl = "https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?q=80&w=500" },
                    new Gato { Nome = "Frajola", Status = StatusGato.Disponivel, DataChegada = new DateOnly(2026, 5, 01), FotoUrl = "https://images.unsplash.com/photo-1573865526739-10659fec78a5?q=80&w=500" },
                    new Gato { Nome = "Paçoca", Status = StatusGato.Reservado, DataChegada = new DateOnly(2026, 3, 15), FotoUrl = "https://images.unsplash.com/photo-1533738363-b7f9aef128ce?q=80&w=500" }
                );
            }

            // 4. SEED DE INSUMOS
            if (!context.Set<Insumo>().Any())
            {
                context.Set<Insumo>().AddRange(
                    new Insumo { Nome = "Fardo Leite Integral (12L)", QuantidadeAtual = 5, EstoqueMinimo = 2 },
                    new Insumo { Nome = "Café em Grãos Blend Especial (1kg)", QuantidadeAtual = 8, EstoqueMinimo = 3 }
                );
            }

            // salva no banco
            context.SaveChanges();
        }
    }
}