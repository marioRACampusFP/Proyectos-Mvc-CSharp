using System;

namespace BookRazor.Models
{
    public class Libro
    {
        //Campos privados (encapsulación)
        private int _id;
        private string _titulo;
        private string _autor;
        private string _genero;
        private decimal _precio;
        private bool _disponible;

        //Propiedades públicas con validaciones y control de acceso
        public int Id { get => _id; set => _id = value; }
        public string Titulo { get => _titulo; set => _titulo = value ?? throw new ArgumentNullException(nameof(Titulo)); }
        public string Autor { get => _autor; set => _autor = value ?? "Desconocido"; }
        public string Genero { get => _genero; set => _genero = value ?? "General"; }
        public decimal Precio { get => _precio; set => _precio = value < 0 ? 0 : value; }
        public bool Disponible { get => _disponible; set => _disponible = value; }

        //Constructor vacío (necesario para instanciación por defecto)
        public Libro() { }

        //Constructor con parámetros para inicializar los valores
        public Libro(int id, string titulo, string autor, string genero, decimal precio, bool disponible = true)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Genero = genero;
            Precio = precio;
            Disponible = disponible;
        }

        //Método virtual que devuelve la descripción general de un libro
        //Se sobrescribirá en las clases hijas (polimorfismo)
        public virtual string ObtenerDescripcion()
        {
            return $"[{Id}] \"{Titulo}\" por {Autor}. Género: {Genero}. Precio: {Precio:C}. {(Disponible ? "Disponible" : "Agotado")}.";
        }
    }
}
