using ProMama.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagemView : ContentPage
    {
        public ImagemView(Imagem imagem)
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.ImagemViewModel(imagem);
        }
    }
}