using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilCriancaView : ContentPage
    {
        public PerfilCriancaView()
        {
            InitializeComponent();

            BindingContext = new PerfilCriancaViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = new PerfilCriancaViewModel(Navigation);
        }
    }
}