using ProMama.Model;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class ImagemViewModel : ViewModelBase
    {
        public string Titulo { get; set; }
        public ImageSource Caminho { get; set; }

        public ImagemViewModel(Imagem _imagem)
        {
            Titulo = _imagem.Titulo;
            Caminho = _imagem.Caminho;
        }
    }
}
