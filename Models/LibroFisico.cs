namespace BookRazor.Models
{
    //Clase derivada de Libro que representa un libro físico
    //Aplica herencia y sobrescribe el método ObtenerDescripcion (polimorfismo)
    public class LibroFisico : Libro
    {
        //Propiedades adicionales exclusivas de los libros físicos
        public int NumeroPaginas { get; set; }
        public double PesoKg { get; set; }

        //Constructor vacío necesario para instanciación por defecto
        public LibroFisico() { }

        //Constructor con parámetros, llama al constructor base (de Libro)
        public LibroFisico(int id, string titulo, string autor, string genero, decimal precio, int numeroPaginas, double pesoKg, bool disponible = true)
            : base(id, titulo, autor, genero, precio, disponible)
        {
            NumeroPaginas = numeroPaginas;
            PesoKg = pesoKg;
        }

        //Sobrescribe el método base para añadir detalles específicos del libro físico
        public override string ObtenerDescripcion()
        {
            return base.ObtenerDescripcion() + $" (Físico: {NumeroPaginas} págs, {PesoKg} kg).";
        }
    }
}
