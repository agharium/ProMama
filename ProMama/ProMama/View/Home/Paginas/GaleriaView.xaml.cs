using ProMama.ViewModel.Home.Paginas;
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

            BindingContext = new GaleriaViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new GaleriaViewModel(Navigation);
        }
    }
}