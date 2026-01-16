namespace BookRazor.Models
{
    //Clase derivada de Libro que representa un libro digital
    //Aplica herencia y sobrescribe el método ObtenerDescripcion (polimorfismo)
    public class LibroDigital : Libro
    {
        //Propiedades específicas de los libros digitales
        public int TamanoMB { get; set; }
        public string Formato { get; set; }

        //Constructor vacío necesario para instanciación por defecto
        public LibroDigital() { }

        //Constructor con parámetros, llama al constructor base (de Libro)
        public LibroDigital(int id, string titulo, string autor, string genero, decimal precio, int tamanoMB, string formato, bool disponible = true)
            : base(id, titulo, autor, genero, precio, disponible)
        {
            TamanoMB = tamanoMB;
            Formato = formato ?? "EPUB";
        }

        //Sobrescribe el método base para añadir información específica del libro digital
        public override string ObtenerDescripcion()
        {
            return base.ObtenerDescripcion() + $" (Digital: {Formato}, {TamanoMB}MB).";
        }
    }
}
