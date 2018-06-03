using ProMama.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FotoView : ContentPage
    {
        public FotoView(Foto foto)
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.FotoViewModel(foto);
        }
    }
}