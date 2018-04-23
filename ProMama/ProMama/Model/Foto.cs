using Xamarin.Forms;

namespace ProMama.Model
{
    class Foto
    {
        public int Mes { get; private set; }
        public ImageSource Imagem { get; set; }

        public Foto(int mes, ImageSource imagem)
        {
            Mes = mes;
            Imagem = imagem;
        }

        public Foto() { }
    }
}
