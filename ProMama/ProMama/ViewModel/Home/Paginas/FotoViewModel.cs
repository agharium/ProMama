using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class FotoViewModel : ViewModelBase
    {
        public ImageSource Foto { get; set; }

        public FotoViewModel(ImageSource foto)
        {
            Foto = foto;
        }
    }
}
