using System;

namespace ProMama.View.Home
{
    public class HomeMenuItem
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Icone { get; set; }
        public Type Pagina { get; set; }

        public HomeMenuItem(int id, string titulo, string icone, Type pagina)
        {
            Id = id;
            Titulo = titulo;
            Icone = icone;
            Pagina = pagina;
        }
    }
}