using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilMaeEditView : ContentPage
    {
        public PerfilMaeEditView()
        {
            InitializeComponent();

            BindingContext = new ViewModel.Home.Paginas.PerfilMaeEditViewModel(Navigation);
        }
    }
}