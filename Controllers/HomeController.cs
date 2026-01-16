using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using BookRazor.Models;

namespace BookRazor.Controllers
{
    public class HomeController : Controller
    {
        //Inyección de dependencias para acceder a la información del sitio desde appsettings.json
        private readonly IOptions<SiteInfo> _siteInfo;

        //Constructor que recibe los datos de configuración (título y lema del sitio)
        public HomeController(IOptions<SiteInfo> siteInfo)
        {
            _siteInfo = siteInfo;
        }

        //Acción principal que muestra la página de inicio
        //Usa los valores de configuración para mostrar el título y el lema del sitio en la vista
        public IActionResult Index()
        {
            ViewBag.Title = _siteInfo.Value.Title;
            ViewBag.Tagline = _siteInfo.Value.Tagline;
            return View();
        }

        //Acción que muestra la vista de error si ocurre algún problema
        public IActionResult Error()
        {
            return View();
        }
    }
}
