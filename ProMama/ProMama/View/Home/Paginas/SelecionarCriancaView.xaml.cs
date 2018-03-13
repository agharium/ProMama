using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelecionarCriancaView : ContentPage
    {
        public SelecionarCriancaView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.SelecionarCriancaViewModel(Navigation);
        }
    }
}