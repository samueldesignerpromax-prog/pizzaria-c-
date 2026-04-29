using Microsoft.EntityFrameworkCore;
using PizzariaWeb.Models;

namespace PizzariaWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed de pizzas
            modelBuilder.Entity<Produto>().HasData(
                new Produto
                {
                    Id = 1,
                    Nome = "Margherita",
                    Descricao = "Pizza tradicional com molho de tomate, mussarela, manjericão fresco e azeite",
                    PrecoPequena = 29.90m,
                    PrecoMedia = 39.90m,
                    PrecoGrande = 49.90m,
                    Categoria = "Tradicionais",
                    Ingredientes = "Molho de tomate, mussarela, manjericão, azeite",
                    Destaque = true,
                    Disponivel = true
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Pepperoni",
                    Descricao = "Mussarela, pepperoni fatiado e orégano",
                    PrecoPequena = 34.90m,
                    PrecoMedia = 44.90m,
                    PrecoGrande = 54.90m,
                    Categoria = "Tradicionais",
                    Ingredientes = "Mussarela, pepperoni, orégano",
                    Destaque = true,
                    Disponivel = true
                },
                new Produto
                {
                    Id = 3,
                    Nome = "Portuguesa",
                    Descricao = "Presunto, ovos, cebola, azeitona, ervilha e mussarela",
                    PrecoPequena = 32.90m,
                    PrecoMedia = 42.90m,
                    PrecoGrande = 52.90m,
                    Categoria = "Tradicionais",
                    Ingredientes = "Presunto, ovos, cebola, azeitona, ervilha, mussarela",
                    Destaque = false,
                    Disponivel = true
                },
                new Produto
                {
                    Id = 4,
                    Nome = "Calabresa",
                    Descricao = "Calabresa fatiada, cebola, mussarela e orégano",
                    PrecoPequena = 31.90m,
                    PrecoMedia = 41.90m,
                    PrecoGrande = 51.90m,
                    Categoria = "Tradicionais",
                    Ingredientes = "Calabresa, cebola, mussarela, orégano",
                    Destaque = true,
                    Disponivel = true
                },
                new Produto
                {
                    Id = 5,
                    Nome = "Frango com Catupiry",
                    Descricao = "Frango desfiado, catupiry, milho e mussarela",
                    PrecoPequena = 35.90m,
                    PrecoMedia = 45.90m,
                    PrecoGrande = 55.90m,
                    Categoria = "Especiais",
                    Ingredientes = "Frango, catupiry, milho, mussarela",
                    Destaque = true,
                    Disponivel = true
                },
                new Produto
                {
                    Id = 6,
                    Nome = "Quatro Queijos",
                    Descricao = "Mussarela, provolone, parmesão, catupiry e orégano",
                    PrecoPequena = 36.90m,
                    PrecoMedia = 46.90m,
                    PrecoGrande = 56.90m,
                    Categoria = "Especiais",
                    Ingredientes = "Mussarela, provolone, parmesão, catupiry, orégano",
                    Destaque = false,
                    Disponivel = true
                }
            );
        }
    }
}
