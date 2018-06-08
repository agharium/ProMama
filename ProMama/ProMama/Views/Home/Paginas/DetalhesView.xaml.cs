using ProMama.Models;
using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesView : ContentPage
    {
        public DetalhesView(Informacao informacao)
        {
            InitializeComponent();
            BindingContext = new DetalhesViewModel(informacao);
        }

        public DetalhesView(Duvida duvida)
        {
            InitializeComponent();
            BindingContext = new DetalhesViewModel(duvida);
        }
    }
}