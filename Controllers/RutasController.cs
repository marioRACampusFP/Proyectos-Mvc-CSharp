using Microsoft.AspNetCore.Mvc;
using EcoTrails.Models;
using Bogus;
using System.Linq;

namespace EcoTrails.Controllers
{
    public class RutasController : Controller
    {
        // Genera rutas falsas
        private static List<Ruta> GenerarRutas(int n = 20)
        {
            var difs = new[] { "Fácil", "Media", "Difícil" };
            var parques = new[] { "Ordesa y Monte Perdido", "Picos de Europa", "Sierra Nevada", "Aigüestortes" };

            var faker = new Faker<Ruta>("es")
                .RuleFor(r => r.Id, f => f.IndexFaker + 1)
                .RuleFor(r => r.Nombre, f => $"Ruta {f.Address.City()} - {f.Random.Int(1, 10)}")
                .RuleFor(r => r.Dificultad, f => f.PickRandom(difs))
                .RuleFor(r => r.Kilometros, f => Math.Round(f.Random.Double(2, 28), 1))
                .RuleFor(r => r.Parque, f => f.PickRandom(parques));

            return faker.Generate(n);
        }

        public IActionResult Index(string? dificultad, double? minKm, double? maxKm)
        {
            var datos = GenerarRutas(30).AsQueryable();

            if (!string.IsNullOrWhiteSpace(dificultad))
                datos = datos.Where(r => r.Dificultad.Equals(dificultad, StringComparison.OrdinalIgnoreCase));
            if (minKm.HasValue)
                datos = datos.Where(r => r.Kilometros >= minKm.Value);
            if (maxKm.HasValue)
                datos = datos.Where(r => r.Kilometros <= maxKm.Value);

            ViewBag.Dificultad = dificultad;
            ViewBag.MinKm = minKm;
            ViewBag.MaxKm = maxKm;

            return View(datos.OrderBy(r => r.Kilometros).ToList());
        }

        public IActionResult Detalle(int id)
        {
            var ruta = GenerarRutas(30).FirstOrDefault(r => r.Id == id);
            if (ruta == null) return NotFound();
            return View(ruta);
        }
    }
}
