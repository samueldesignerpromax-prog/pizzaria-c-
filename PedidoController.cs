using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzariaWeb.Data;
using PizzariaWeb.Models;
using PizzariaWeb.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PizzariaWeb.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            var carrinhoJson = HttpContext.Session.GetString("Carrinho");
            if (string.IsNullOrEmpty(carrinhoJson))
            {
                return RedirectToAction("Index", "Carrinho");
            }

            var model = new CheckoutViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var carrinhoJson = HttpContext.Session.GetString("Carrinho");
            if (string.IsNullOrEmpty(carrinhoJson))
            {
                return RedirectToAction("Index", "Produtos");
            }

            var carrinho = System.Text.Json.JsonSerializer.Deserialize<CarrinhoViewModel>(carrinhoJson);
            
            if (carrinho.Itens.Count == 0)
            {
                return RedirectToAction("Index", "Produtos");
            }

            var pedido = new Pedido
            {
                ClienteNome = model.Nome,
                ClienteEmail = model.Email,
                ClienteTelefone = model.Telefone,
                EnderecoEntrega = $"{model.Endereco} {model.Complemento}",
                Observacoes = model.Observacoes,
                Subtotal = carrinho.Subtotal,
                TaxaEntrega = carrinho.TaxaEntrega,
                Total = carrinho.Total,
                FormaPagamento = model.FormaPagamento,
                DataPedido = DateTime.Now,
                Status = "Recebido"
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            foreach (var item in carrinho.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                var itemPedido = new ItemPedido
                {
                    PedidoId = pedido.Id,
                    ProdutoId = item.ProdutoId,
                    ProdutoNome = item.ProdutoNome,
                    Tamanho = item.Tamanho,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.Preco
                };
                _context.ItensPedido.Add(itemPedido);
            }

            await _context.SaveChangesAsync();

            // Limpar carrinho
            HttpContext.Session.Remove("Carrinho");

            TempData["PedidoId"] = pedido.Id;
            return RedirectToAction("Confirmacao");
        }

        public IActionResult Confirmacao()
        {
            if (TempData["PedidoId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var pedidoId = (int)TempData["PedidoId"];
            return View(pedidoId);
        }
    }
}
