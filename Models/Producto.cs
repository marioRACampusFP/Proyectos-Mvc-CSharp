namespace CatalogoProductosMvc.Models;

public class Producto
{
    public int Id { get; set; }                      // Identificador
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = "General";
    public decimal Precio { get; set; }
    public string Imagen { get; set; } = string.Empty; // nombre de archivo en wwwroot/images
}
