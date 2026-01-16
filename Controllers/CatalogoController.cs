using Microsoft.AspNetCore.Mvc;
using BookRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRazor.Controllers
{
    public class CatalogoController : Controller
    {
        //Método privado que genera una lista de libros (físicos y digitales)
        //Aquí se aplica herencia y polimorfismo, ya que LibroFisico y LibroDigital derivan de Libro
        private List<Libro> GenerarLibros()
        {
            return new List<Libro>
            {
                new LibroFisico(1, "Cien Años de Soledad", "Gabriel García Márquez", "Realismo mágico", 18.50m, 417, 0.55),
                new LibroDigital(2, "Clean Code", "Robert C. Martin", "Programación", 25.00m, 2, "PDF"),
                new LibroFisico(3, "El Principito", "Antoine de Saint-Exupéry", "Infantil", 9.99m, 96, 0.15),
                new LibroDigital(4, "The Pragmatic Programmer", "Andrew Hunt", "Programación", 22.00m, 3, "EPUB"),
                new LibroFisico(5, "Don Quijote", "Miguel de Cervantes", "Clásico", 24.00m, 863, 1.2),
                new LibroDigital(6, "El Aleph", "Jorge Luis Borges", "Narrativa", 7.50m, 1, "MOBI")
            };
        }

        //Acción principal que muestra el listado completo de libros
        //Envía la lista a la vista "Index"
        public IActionResult Index()
        {
            var libros = GenerarLibros();
            return View(libros);
        }

        //Acción que muestra los detalles de un libro concreto
        //Busca el libro por su ID y utiliza el método ObtenerDescripcion() (polimorfismo)
        public IActionResult Detalle(int id)
        {
            var libro = GenerarLibros().FirstOrDefault(l => l.Id == id);
            if (libro == null) return NotFound();
            ViewBag.Descripcion = libro.ObtenerDescripcion();
            return View(libro);
        }

        //Acción que genera recomendaciones aleatorias (3 libros)
        //qDemuestra cómo manejar listas y seleccionar elementos dinámicamente
        public IActionResult Recomendaciones()
        {
            var rnd = new Random();
            var libros = GenerarLibros();
            var recomendados = libros.OrderBy(x => rnd.Next()).Take(3).ToList();
            return View(recomendados);
        }
    }
}
