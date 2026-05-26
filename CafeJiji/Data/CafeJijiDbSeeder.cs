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
                // ==================== CATEGORIA: CAFÉS & BEBIDAS ====================
                new Produto { 
                    Nome = "Espresso Tradicional", Preco = 7.50m, Categoria = "Cafés", 
                    QuantidadeEstoque = 9, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/espresso.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Capuccino Jiji", Preco = 12.00m, Categoria = "Cafés", 
                    QuantidadeEstoque = 9, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/capuccino-jiji.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Latte Macchiato", Preco = 14.50m, Categoria = "Cafés", 
                    QuantidadeEstoque = 9, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/latte-macchiato.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Chocolate Quente Cremoso", Preco = 11.00m, Categoria = "Cafés", 
                    QuantidadeEstoque = 9, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/chocolate-quente.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Iced Tea de Frutas Vermelhas", Preco = 10.50m, Categoria = "Cafés", 
                    QuantidadeEstoque = 9, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/iced-tea.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Suco Gato Laranja", Preco = 9.00m, Categoria = "Cafés", 
                    QuantidadeEstoque = 9, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/suco-laranja.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },

                // ==================== CATEGORIA: DOCES ====================
                new Produto { 
                    Nome = "Fatia de Torta Holandesa", Preco = 16.00m, Categoria = "Doces", 
                    QuantidadeEstoque = 12, EstoqueMinimo = 3, RequerPreparo = false, 
                    ImagemUrl = "/images/torta-holandesa.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Brownie com Sorvete", Preco = 15.50m, Categoria = "Doces", 
                    QuantidadeEstoque = 20, EstoqueMinimo = 5, RequerPreparo = true, // Requer preparo para montar e aquecer
                    ImagemUrl = "/images/brownie-sorvete.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Bolo Mágico Jiji", Preco = 14.00m, Categoria = "Doces", 
                    QuantidadeEstoque = 10, EstoqueMinimo = 2, RequerPreparo = false, 
                    ImagemUrl = "/images/bolo-jiji.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Cheesecake com Geleia de Frutas Vermelhas", Preco = 18.00m, Categoria = "Doces", 
                    QuantidadeEstoque = 8, EstoqueMinimo = 2, RequerPreparo = false, 
                    ImagemUrl = "/images/cheesecake.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Totoro Puff (Choux Cream)", Preco = 13.50m, Categoria = "Doces", 
                    QuantidadeEstoque = 15, EstoqueMinimo = 4, RequerPreparo = false, 
                    ImagemUrl = "/images/totoro-puff.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Croissant de Morango e Chocolate", Preco = 16.50m, Categoria = "Doces", 
                    QuantidadeEstoque = 12, EstoqueMinimo = 3, RequerPreparo = true, // Aquecido na hora
                    ImagemUrl = "/images/croissant-morango.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Petit Gatô", Preco = 16.50m, Categoria = "Doces", 
                    QuantidadeEstoque = 12, EstoqueMinimo = 3, RequerPreparo = false,
                    ImagemUrl = "/images/petit-gato.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },

                // ==================== CATEGORIA: SALGADOS ====================
                new Produto { 
                    Nome = "Pão de Queijo de Gatinho", Preco = 9.00m, Categoria = "Salgados", 
                    QuantidadeEstoque = 10, EstoqueMinimo = 5, RequerPreparo = true, // Assado na hora!
                    ImagemUrl = "/images/pao-de-queijo-gatinho.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Croissant de Salmão e Ovo Poché", Preco = 24.00m, Categoria = "Salgados", 
                    QuantidadeEstoque = 10, EstoqueMinimo = 2, RequerPreparo = true, // Cozinha gourmet feita na hora
                    ImagemUrl = "/images/croissant-salmao.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Misto Quente de Queijo com Presunto", Preco = 11.50m, Categoria = "Salgados", 
                    QuantidadeEstoque = 10, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/misto-presunto.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },
                new Produto { 
                    Nome = "Misto Cremoso de Frango", Preco = 13.00m, Categoria = "Salgados", 
                    QuantidadeEstoque = 10, EstoqueMinimo = 0, RequerPreparo = true, 
                    ImagemUrl = "/images/misto-frango.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                },

                // ==================== CATEGORIA: SERVIÇOS ====================
                new Produto { 
                    Nome = "Taxa de Entrada Gatil", Preco = 15.00m, Categoria = "Serviços", 
                    QuantidadeEstoque = 100, EstoqueMinimo = 0, RequerPreparo = false, 
                    ImagemUrl = "/images/padrao.jpg", Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now 
                }
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