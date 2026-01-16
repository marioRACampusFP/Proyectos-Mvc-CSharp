namespace EcoTrails.Models
{
    public class Ruta
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Dificultad { get; set; } = ""; // "Fácil", "Media", "Difícil"
        public double Kilometros { get; set; }
        public string Parque { get; set; } = "";
    }
}
