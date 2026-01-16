namespace EcoTrails.Models
{
    public class Noticia
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string Resumen { get; set; } = "";
    }
}
