namespace BookRazor.Models
{ 
    //Clase que representa la información general del sitio web
    //Sus valores se cargan desde el archivo appsettings.json
    public class SiteInfo
    {
        //Propiedades públicas para el título y el lema del sitio
        public string Title { get; set; }
        public string Tagline { get; set; }

        //Constructor vacío necesario para la configuración automática (binding)
        public SiteInfo() { }

        //Constructor con parámetros para inicializar manualmente los valores
        public SiteInfo(string title, string tagline)
        {
            Title = title;
            Tagline = tagline;
        }
    }
}
