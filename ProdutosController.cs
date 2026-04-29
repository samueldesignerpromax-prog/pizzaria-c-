using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzariaWeb.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PizzariaWeb.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string categoria, string busca)
        {
            var produtos = _context.Produtos.Where(p => p.Disponivel).AsQueryable();

            if (!string.IsNullOrEmpty(categoria) && categoria != "Todos")
            {
                produtos = produtos.Where(p => p.Categoria == categoria);
            }

            if (!string.IsNullOrEmpty(busca))
            {
                produtos = produtos.Where(p => p.Nome.Contains(busca) || 
                                               p.Descricao.Contains(busca) ||
                                               p.Ingredientes.Contains(busca));
            }

            ViewBag.Categorias = await _context.Produtos
                .Where(p => p.Disponivel)
                .Select(p => p.Categoria)
                .Distinct()
                .ToListAsync();

            return View(await produtos.ToListAsync());
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            
            if (produto == null || !produto.Disponivel)
            {
                return NotFound();
            }

            return View(produto);
        }
    }
}
