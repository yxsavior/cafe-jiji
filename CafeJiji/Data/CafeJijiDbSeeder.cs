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
                    // === 16 GATINHOS ADOTADOS (Sucessos do Café Jiji) ===
                    new Gato { Nome = "Mingau", Status = StatusGato.Adotado, DataChegada = new DateOnly(2025, 10, 12), FotoUrl = "https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?q=80&w=500" },
                    new Gato { Nome = "Frajola", Status = StatusGato.Adotado, DataChegada = new DateOnly(2025, 11, 05), FotoUrl = "https://images.unsplash.com/photo-1573865526739-10659fec78a5?q=80&w=500" },
                    new Gato { Nome = "Luna", Status = StatusGato.Adotado, DataChegada = new DateOnly(2025, 11, 20), FotoUrl = "https://images.unsplash.com/photo-1495360010541-f48722b34f7d?q=80&w=500" },
                    new Gato { Nome = "Simba", Status = StatusGato.Adotado, DataChegada = new DateOnly(2025, 12, 01), FotoUrl = "https://images.unsplash.com/photo-1519052537078-e6302a4968d4?q=80&w=500" },
                    new Gato { Nome = "Mel", Status = StatusGato.Adotado, DataChegada = new DateOnly(2025, 12, 15), FotoUrl = "https://images.unsplash.com/photo-1543466835-00a7907e9de1?q=80&w=500" },
                    new Gato { Nome = "Oliver", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 01, 10), FotoUrl = "https://images.unsplash.com/photo-1536590158209-e9d615d525e4?q=80&w=500" },
                    new Gato { Nome = "Mia", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 01, 22), FotoUrl = "https://images.unsplash.com/photo-1561948955-570b270e7c36?q=80&w=500" },
                    new Gato { Nome = "Tom", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 02, 02), FotoUrl = "https://images.unsplash.com/photo-1526336028067-849a161d56a1?q=80&w=500" },
                    new Gato { Nome = "Garfield", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 02, 15), FotoUrl = "https://images.unsplash.com/photo-1513360356757-782b606f5847?q=80&w=500" },
                    new Gato { Nome = "Nala", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 03, 01), FotoUrl = "https://images.unsplash.com/photo-1592194996308-7b43878e84a6?q=80&w=500" },
                    new Gato { Nome = "Chico", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 03, 12), FotoUrl = "https://images.unsplash.com/photo-1574158622643-69d34d72650a?q=80&w=500" },
                    new Gato { Nome = "Bolinha", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 03, 28), FotoUrl = "https://images.unsplash.com/photo-1533743983669-94fa5c4338ec?q=80&w=500" },
                    new Gato { Nome = "Cacau", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 04, 05), FotoUrl = "https://images.unsplash.com/photo-1511044568932-338cba0ad80d?q=80&w=500" },
                    new Gato { Nome = "Pipoca", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 04, 18), FotoUrl = "https://images.unsplash.com/photo-1548247416-ec66f4900b2e?q=80&w=500" },
                    new Gato { Nome = "Frederico", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 05, 02), FotoUrl = "https://images.unsplash.com/photo-1555685812-4b943f1cb0eb?q=80&w=500" },
                    new Gato { Nome = "Amora", Status = StatusGato.Adotado, DataChegada = new DateOnly(2026, 05, 10), FotoUrl = "https://images.unsplash.com/photo-1583511655857-d19b40a7a54e?q=80&w=500" },

                    // === 4 GATINHOS ATUAIS NO GATIL (Disponíveis e Reservados) ===
                    new Gato { Nome = "Paçoca", Status = StatusGato.Reservado, DataChegada = new DateOnly(2026, 05, 15), FotoUrl = "https://images.unsplash.com/photo-1533738363-b7f9aef128ce?q=80&w=500" },
                    new Gato { Nome = "Pretinho", Status = StatusGato.Reservado, DataChegada = new DateOnly(2026, 05, 18), FotoUrl = "https://images.unsplash.com/photo-1520315342629-6ea920342047?q=80&w=500" },
                    new Gato { Nome = "Cookie", Status = StatusGato.Disponivel, DataChegada = new DateOnly(2026, 05, 20), FotoUrl = "https://images.unsplash.com/photo-1517849845537-4d257902454a?q=80&w=500" },
                    new Gato { Nome = "Fumaça", Status = StatusGato.Disponivel, DataChegada = new DateOnly(2026, 05, 24), FotoUrl = "https://images.unsplash.com/photo-1472491235688-bdc81a63246e?q=80&w=500" }
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