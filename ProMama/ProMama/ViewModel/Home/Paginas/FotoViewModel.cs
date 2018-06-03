using ProMama.Model;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class FotoViewModel : ViewModelBase
    {
        private Foto Foto { get; set; }
        public string Titulo { get; set; }
        public ImageSource Caminho { get; set; }

        public FotoViewModel(Foto _foto)
        {
            Foto = _foto;
            Titulo = Foto.titulo;
            Caminho = Foto.source;
        }
    }
}
