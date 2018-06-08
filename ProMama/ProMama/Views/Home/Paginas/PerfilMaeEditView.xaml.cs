using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilMaeEditView : ContentPage
    {
        public PerfilMaeEditView()
        {
            InitializeComponent();

            BindingContext = new PerfilMaeEditViewModel(Navigation);
        }
    }
}