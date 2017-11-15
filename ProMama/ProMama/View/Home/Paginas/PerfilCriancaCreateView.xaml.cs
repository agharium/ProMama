using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilCriancaCreateView : ContentPage
    {
        public PerfilCriancaCreateView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.PerfilCriancaCreateViewModel(this.Navigation);
        }
    }
}