using Microsoft.AspNetCore.Mvc;
using PizzariaWeb.Models;
using PizzariaWeb.ViewModels;
using System.Text.Json;

namespace PizzariaWeb.Controllers
{
    public class CarrinhoController : Controller
    {
        public IActionResult Index()
        {
            var carrinho = GetCarrinho();
            return View(carrinho);
        }

        [HttpPost]
        public IActionResult Adicionar(int id, string nome, string tamanho, decimal preco, int quantidade = 1, string observacao = "")
        {
            var carrinho = GetCarrinho();
            
            var itemExistente = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id && i.Tamanho == tamanho);
            
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                carrinho.Itens.Add(new ItemCarrinho
                {
                    ProdutoId = id,
                    ProdutoNome = nome,
                    Tamanho = tamanho,
                    Preco = preco,
                    Quantidade = quantidade,
                    Observacao = observacao
                });
            }

            SaveCarrinho(carrinho);
            TempData["Mensagem"] = $"{nome} ({tamanho}) adicionado ao carrinho!";
            
            return RedirectToAction("Index", "Produtos");
        }

        public IActionResult Remover(int id, string tamanho)
        {
            var carrinho = GetCarrinho();
            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id && i.Tamanho == tamanho);
            
            if (item != null)
            {
                carrinho.Itens.Remove(item);
                SaveCarrinho(carrinho);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AtualizarQuantidade(int id, string tamanho, int quantidade)
        {
            var carrinho = GetCarrinho();
            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == id && i.Tamanho == tamanho);
            
            if (item != null)
            {
                if (quantidade <= 0)
                {
                    carrinho.Itens.Remove(item);
                }
                else
                {
                    item.Quantidade = quantidade;
                }
                SaveCarrinho(carrinho);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Limpar()
        {
            SaveCarrinho(new CarrinhoViewModel());
            return RedirectToAction("Index");
        }

        private CarrinhoViewModel GetCarrinho()
        {
            var carrinhoJson = HttpContext.Session.GetString("Carrinho");
            return string.IsNullOrEmpty(carrinhoJson) 
                ? new CarrinhoViewModel() 
                : JsonSerializer.Deserialize<CarrinhoViewModel>(carrinhoJson);
        }

        private void SaveCarrinho(CarrinhoViewModel carrinho)
        {
            var carrinhoJson = JsonSerializer.Serialize(carrinho);
            HttpContext.Session.SetString("Carrinho", carrinhoJson);
        }
    }
}
