using Microsoft.AspNetCore.Mvc;
using EcoTrails.Models;
using Bogus;
using System.Linq;

namespace EcoTrails.Controllers
{
    public class EspeciesController : Controller
    {
        private static List<Especie> Generar(int n = 20)
        {
            var tipos = new[] { "Flora", "Fauna" };

            var faker = new Faker<Especie>("es")
                .RuleFor(e => e.Id, f => f.IndexFaker + 1)
                .RuleFor(e => e.NombreComun, f => f.Commerce.ProductMaterial()) // simple “nombre”
                .RuleFor(e => e.Tipo, f => f.PickRandom(tipos))
                .RuleFor(e => e.Protegida, f => f.Random.Bool(0.3f)); // 30% protegidas

            return faker.Generate(n);
        }

        public IActionResult Index(string? tipo, bool? protegida)
        {
            var datos = Generar(30).AsQueryable();

            if (!string.IsNullOrWhiteSpace(tipo))
                datos = datos.Where(e => e.Tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase));
            if (protegida.HasValue)
                datos = datos.Where(e => e.Protegida == protegida.Value);

            ViewBag.Tipo = tipo;
            ViewBag.Protegida = protegida;

            return View(datos.OrderBy(e => e.NombreComun).ToList());
        }
    }
}
