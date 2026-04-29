using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzariaWeb.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PizzariaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var destaques = await _context.Produtos
                .Where(p => p.Destaque && p.Disponivel)
                .Take(4)
                .ToListAsync();

            return View(destaques);
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contato(string nome, string email, string mensagem)
        {
            // Aqui você pode implementar envio de email
            TempData["Mensagem"] = "Mensagem enviada com sucesso! Entraremos em contato em breve.";
            return RedirectToAction("Contato");
        }
    }
}
