using CafeJiji.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeJiji.Data
{
    public static class CafeJijiDbSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // 1. SEED DE USUÁRIOS
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Nome = "Kiki", Username = "jiji.senior", SenhaHash = "hash_senior_123", Perfil = "Gerente" },
                new Usuario { Id = 2, Nome = "Ursula", Username = "jiji.junior", SenhaHash = "hash_junior_123", Perfil = "Atendente" },
                new Usuario { Id = 3, Nome = "Osono", Username = "jiji.pleno", SenhaHash = "hash_pleno_123", Perfil = "Barista" }
            );

            // 2. SEED DE PRODUTOS
            modelBuilder.Entity<Produto>().HasData(
                // Categoria: Cafés (Requerem Preparo)
                new Produto { Id = 1, Nome = "Espresso Tradicional", Preco = 7.50m, Categoria = "Cafés", QuantidadeEstoque = 999, EstoqueMinimo = 0, RequerPreparo = true, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },
                new Produto { Id = 2, Nome = "Capuccino Gateiro", Preco = 12.00m, Categoria = "Cafés", QuantidadeEstoque = 999, EstoqueMinimo = 0, RequerPreparo = true, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },
                new Produto { Id = 3, Nome = "Latte Macchiato", Preco = 14.50m, Categoria = "Cafés", QuantidadeEstoque = 999, EstoqueMinimo = 0, RequerPreparo = true, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },

                // Categoria: Doces (Venda Direta / Estoque Limitado)
                new Produto { Id = 4, Nome = "Fatia de Torta Holandesa", Preco = 16.00m, Categoria = "Doces", QuantidadeEstoque = 12, EstoqueMinimo = 3, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },
                new Produto { Id = 5, Nome = "Brownie de Chocolate", Preco = 9.50m, Categoria = "Doces", QuantidadeEstoque = 20, EstoqueMinimo = 5, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },

                // Categoria: Salgados
                new Produto { Id = 6, Nome = "Pão de Queijo Recheado", Preco = 8.00m, Categoria = "Salgados", QuantidadeEstoque = 15, EstoqueMinimo = 4, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now },

                // Categoria: Serviços (A taxa do Gatil)
                new Produto { Id = 7, Nome = "Taxa de Entrada Gatil", Preco = 15.00m, Categoria = "Serviços", QuantidadeEstoque = 9999, EstoqueMinimo = 0, RequerPreparo = false, Ativo = true, CriadoEm = DateTime.Now, AtualizadoEm = DateTime.Now }
            );

            // 3. SEED DE GATOS
            modelBuilder.Entity<Gato>().HasData(
                new Gato { Id = 1, Nome = "Mingau", Status = StatusGato.Disponivel, DataChegada = new DateOnly(2026, 4, 10), FotoUrl = "https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?q=80&w=500" },
                new Gato { Id = 2, Nome = "Frajola", Status = StatusGato.Disponivel, DataChegada = new DateOnly(2026, 5, 01), FotoUrl = "https://images.unsplash.com/photo-1573865526739-10659fec78a5?q=80&w=500" },
                new Gato { Id = 3, Nome = "Paçoca", Status = StatusGato.Reservado, DataChegada = new DateOnly(2026, 3, 15), FotoUrl = "https://images.unsplash.com/photo-1533738363-b7f9aef128ce?q=80&w=500" }
            );

            // 4. SEED DE INSUMOS (Controle macro da cozinha)
            modelBuilder.Entity<Insumo>().HasData(
                new Insumo { Id = 1, Nome = "Fardo Leite Integral (12L)", QuantidadeAtual = 5, EstoqueMinimo = 2 },
                new Insumo { Id = 2, Nome = "Café em Grãos Blend Especial (1kg)", QuantidadeAtual = 8, EstoqueMinimo = 3 }
            );
        }
    }
}