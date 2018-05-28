using ProMama.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesView : ContentPage
    {
        public DetalhesView(Informacao informacao)
        {
            InitializeComponent();
            BindingContext = new ViewModel.Home.Paginas.DetalhesViewModel(informacao);
        }

        public DetalhesView(Duvida duvida)
        {
            InitializeComponent();
            BindingContext = new ViewModel.Home.Paginas.DetalhesViewModel(duvida);
        }
    }
}