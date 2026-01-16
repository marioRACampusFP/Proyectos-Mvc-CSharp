using Microsoft.AspNetCore.Mvc;
using EcoTrails.Models;
using Bogus;
using Humanizer;

namespace EcoTrails.Controllers
{
    public class NoticiasController : Controller
    {
        private static List<Noticia> Generar(int n = 12)
        {
            var faker = new Faker<Noticia>("es")
                .RuleFor(n => n.Id, f => f.IndexFaker + 1)
                .RuleFor(n => n.Titulo, f => f.Lorem.Sentence(5, 2))
                .RuleFor(n => n.Fecha, f => f.Date.Recent(40))
                .RuleFor(n => n.Resumen, f => f.Lorem.Paragraph());

            return faker.Generate(n)
                        .OrderByDescending(x => x.Fecha)
                        .ToList();
        }

        public IActionResult Index(string? q)
        {
            var datos = Generar().AsEnumerable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                datos = datos.Where(n => n.Titulo.Contains(q, StringComparison.OrdinalIgnoreCase)
                                       || n.Resumen.Contains(q, StringComparison.OrdinalIgnoreCase));
            }

            ViewBag.Q = q;
            return View(datos.ToList());
        }
    }
}
