using ProMama.Models;
using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelecionarCriancaView : ContentPage
    {
        public SelecionarCriancaView()
        {
            InitializeComponent();

            BindingContext = new SelecionarCriancaViewModel(Navigation);
        }

        protected override bool OnBackButtonPressed()
        {
            Aplicativo app = Aplicativo.Instance;
            app._home.Detail_Home();
            return true;
        }
    }
}