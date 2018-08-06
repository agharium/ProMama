using Xamarin.Forms;

namespace ProMama.Models
{
    public class PaginaCarousel
    {
        public int Position { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public ImageSource Imagem { get; set; }

        public PaginaCarousel(int position, string titulo, string subtitulo, ImageSource imagem)
        {
            Position = position;
            Titulo = titulo;
            Subtitulo = subtitulo;
            Imagem = imagem;
        }
    }
}
