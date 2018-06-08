using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilCriancaEditView : ContentPage
    {
        public PerfilCriancaEditView()
        {
            InitializeComponent();

            BindingContext = new PerfilCriancaEditViewModel(Navigation);

            for (int i = 20; i <= 42; i++)
            {
                idadeGestacionalPicker.Items.Add(i + " semanas");
            }
        }
    }
}