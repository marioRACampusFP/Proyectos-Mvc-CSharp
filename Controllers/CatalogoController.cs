using Microsoft.AspNetCore.Mvc;
using CatalogoProductosMvc.Models;

namespace CatalogoProductosMvc.Controllers;

// Usaremos rutas por atributo. El patrón por defecto del proyecto también funcionaría,
// pero así dejamos endpoints claros bajo /catalogo
[Route("catalogo")]
public class CatalogoController : Controller
{
    // “Base de datos” en memoria (solo para el ejercicio)
    private static readonly List<Producto> _productos = new()
    {
        new() { Id = 1, Nombre = "Mouse óptico",    Categoria = "Periféricos", Precio = 19.99m, Imagen = "mouse.jpg"   },
        new() { Id = 2, Nombre = "Teclado mecánico",Categoria = "Periféricos", Precio = 49.90m, Imagen = "teclado.jpg" },
        new() { Id = 3, Nombre = "Monitor 24\"",    Categoria = "Pantallas",   Precio = 139.00m, Imagen = "monitor.jpg"}
    };

    // 0) Texto de bienvenida
    // GET /catalogo
    [HttpGet]
    public IActionResult Bienvenida()
    {
        // Devolvemos texto plano sin vista Razor
        return Content("Bienvenido al Catálogo de Productos (MVC sin Razor) 👋");
    }

    // 1) Listado JSON con búsqueda, filtros, orden y paginación
    // GET /catalogo/productos?q=mouse&categoria=Periféricos&min=10&max=50&sort=precio_asc&skip=0&take=10
    [HttpGet("productos")]
    public IActionResult GetProductos(
        [FromQuery] string? q,
        [FromQuery] string? categoria,
        [FromQuery] decimal? min,
        [FromQuery] decimal? max,
        [FromQuery] string? sort,   // nombre_asc | nombre_desc | precio_asc | precio_desc
        [FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        if (take is < 1 or > 100) take = 50; // límites sanos

        IEnumerable<Producto> query = _productos;

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.Nombre.Contains(q, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(categoria))
            query = query.Where(p => p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));

        if (min is not null) query = query.Where(p => p.Precio >= min);
        if (max is not null) query = query.Where(p => p.Precio <= max);

        query = sort?.ToLowerInvariant() switch
        {
            "nombre_asc" => query.OrderBy(p => p.Nombre),
            "nombre_desc" => query.OrderByDescending(p => p.Nombre),
            "precio_asc" => query.OrderBy(p => p.Precio),
            "precio_desc" => query.OrderByDescending(p => p.Precio),
            _ => query.OrderBy(p => p.Id)
        };

        var total = query.Count();
        var items = query.Skip(skip).Take(take).ToList();

        // Devolvemos JSON: datos + metadatos + enlaces
        return Ok(new
        {
            total,
            skip,
            take,
            items,
            links = new
            {
                self = Url.ActionLink(nameof(GetProductos), "Catalogo",
                    new { q, categoria, min, max, sort, skip, take }),
                next = (skip + take < total)
                    ? Url.ActionLink(nameof(GetProductos), "Catalogo",
                        new { q, categoria, min, max, sort, skip = skip + take, take })
                    : null
            }
        });
    }

    // 2) Detalle por Id — GET /catalogo/productos/2
    [HttpGet("productos/{id:int}")]
    public IActionResult GetProductoById(int id)
    {
        var p = _productos.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound(new { mensaje = "Producto no encontrado" });

        return Ok(new
        {
            p.Id,
            p.Nombre,
            p.Categoria,
            p.Precio,
            p.Imagen,
            links = new
            {
                self = Url.ActionLink(nameof(GetProductoById), "Catalogo", new { id }),
                imagen = Url.ActionLink(nameof(GetImagenProducto), "Catalogo", new { id }),
                archivo = Url.ActionLink(nameof(GetImagenArchivo), "Catalogo", new { archivo = p.Imagen })
            }
        });
    }

    // 3) Imagen por nombre de archivo — GET /catalogo/imagenes/mouse.jpg
    [HttpGet("imagenes/{archivo}")]
    public IActionResult GetImagenArchivo(string archivo)
    {
        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", archivo);
        if (!System.IO.File.Exists(ruta)) return NotFound("Imagen no encontrada.");

        // Mime básico según extensión
        var contentType = archivo.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ? "image/png" : "image/jpeg";

        // Devolvemos el archivo físico (sin pasar por Razor)
        return PhysicalFile(ruta, contentType);
    }

    // 4) Imagen del producto por Id — GET /catalogo/productos/2/imagen
    [HttpGet("productos/{id:int}/imagen")]
    public IActionResult GetImagenProducto(int id)
    {
        var p = _productos.FirstOrDefault(x => x.Id == id);
        if (p is null) return NotFound("Producto no encontrado.");
        return GetImagenArchivo(p.Imagen);
    }

    // 5) (Opcional) Crear un producto — POST /catalogo/productos
    // Body JSON: { "nombre":"...", "categoria":"...", "precio":10.5, "imagen":"nuevo.jpg" }
    [HttpPost("productos")]
    public IActionResult CrearProducto([FromBody] Producto nuevo)
    {
        if (string.IsNullOrWhiteSpace(nuevo.Nombre))
            return BadRequest(new { mensaje = "El nombre es obligatorio." });

        nuevo.Id = _productos.Count == 0 ? 1 : _productos.Max(p => p.Id) + 1;
        _productos.Add(nuevo);

        // 201 + Location al recurso creado
        return CreatedAtAction(nameof(GetProductoById), new { id = nuevo.Id }, nuevo);
    }
}
