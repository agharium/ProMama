using Xamarin.Forms;

namespace ProMama.Model
{
    public class Imagem
    {
        public int Numero { get; set; }
        public string Titulo { get; set; }
        public ImageSource Caminho { get; set; }

        public Imagem(int numero, string titulo, ImageSource caminho)
        {
            Numero = numero;
            Titulo = titulo;
            Caminho = caminho;
        }

        public Imagem() { }
    }
}
