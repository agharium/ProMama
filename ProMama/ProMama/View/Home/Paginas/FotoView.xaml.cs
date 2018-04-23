using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FotoView : ContentPage
    {
        public FotoView(ImageSource foto)
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.FotoViewModel(foto);
        }
    }
}