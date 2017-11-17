using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GaleriaView : ContentPage
    {
        public GaleriaView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.GaleriaViewModel();
        }
    }
}