namespace EcoTrails.Models
{
    public class Especie
    {
        public int Id { get; set; }
        public string NombreComun { get; set; } = "";
        public string Tipo { get; set; } = "";   // "Flora" o "Fauna"
        public bool Protegida { get; set; }
    }
}
